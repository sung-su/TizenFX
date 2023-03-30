/// @file UITaskManager.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version>10.10.0</version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2023 Samsung Electronics Co., Ltd All Rights Reserved
/// PROPRIETARY/CONFIDENTIAL 
/// This software is the confidential and proprietary
/// information of SAMSUNG ELECTRONICS ("Confidential Information"). You shall
/// not disclose such Confidential Information and shall use it only in
/// accordance with the terms of the license agreement you entered into with
/// SAMSUNG ELECTRONICS. SAMSUNG make no representations or warranties about the
/// suitability of the software, either express or implied, including but not
/// limited to the implied warranties of merchantability, fitness for a
/// particular purpose, or non-infringement. SAMSUNG shall not be liable for any
/// damages suffered by licensee as a result of using, modifying or distributing
/// this software or its derivatives.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Singleton Class
    /// Schedules the UI task registered by user on Main thread based on Time Slicing and Priority scheduling approach.
    /// UITaskManager Manager object is accessible to worker thread also.
    /// Means Add/Remove public APIs can be called from worker thread also.
    /// </summary>
    /// <code>
    /// UITaskManager.GetInstance().Add(Callback, data as object, UITaskManager.Priority.High);
    /// </code>
    internal class UITaskManager
    {
        /// <summary>
        /// Callback that user will register to be called in main thread
        /// </summary>
        /// <typeparam name="T"> generic type</typeparam>
        /// <param name="obj"> generic type object</param>
        public delegate void FunctionCallback<in T>(T obj);

        /// <summary>
        /// Task Priority
        /// High Priority task will be processed first, then Medium and then Low
        /// </summary>
        public enum Priority : uint
        {
            /// <summary> Lowest priority /// </summary>
            Low = 0,
            /// <summary> Medium priority /// </summary>
            Medium,
            /// <summary> Highest priority /// </summary>
            High,
            /// <summary> End of enum /// </summary>
            Last
        }

        /// <summary>
        /// Gets the singleton of the UITaskManager instance
        /// </summary>
        /// <returns> UITaskManager singleton instance</returns>
        public static UITaskManager Instance => instance;

        /// <summary>
        /// Add task to be processed in main thread
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"> Function to be called from main thread. It will get removed once it is called from main thread.</param>
        /// <param name="obj"> User data</param>
        /// <param name="priority"> Priority of task</param>
        /// <returns> Value greater than 0 if task successfully added, otherwise 0</returns>
        public uint Add<T>(FunctionCallback<T> callback, T obj, Priority priority)
        {
            uint id = GetNextFreeId();
            if (id == 0)
            {
                CLog.Debug("Failed to add task");
                return 0;
            }
            CLog.Debug("Next free id is %d1", d1: id);
            {
                lock (taskData[readIndex].dataLock)
                {
                    if (isAppTerminated == true)
                    {
                        CLog.Debug("isAppTerminated is true");
                        return 0;
                    }
                    // Add new task in client side task dictionary.
                    // These task will get copied to ecoreIdler task dictionary in idler callback

                    taskData[readIndex].taskDictionary[(uint)(priority)].Add(id, () => { callback(obj); });

                    if (ecoreIdlerAdded == false)
                    {
                        ecoreIdlerAdded = true;

                        //calling AddEcoreIdler API in main thread
                        Interop.Ecore.EcoreMainLoopThreadSafeCallAsync((value) => { UITaskManager.Instance.AddEcoreIdler(); }, IntPtr.Zero);
                    }
                }
            }

            bool result = taskIdPriorityDictionary.TryAdd(id, priority);


            if (result == false)
            {
                CLog.Debug("Failed to add id & priority in taskIdPriorityDictionary");
            }
            return id;
        }

        /// <summary>
        /// Removes previously added task, if that task is not already processed
        /// </summary>
        /// <param name="id">id of task to be removed</param>
        public void Remove(uint id)
        {
            bool result = taskIdPriorityDictionary.TryRemove(id, out Priority priority);
            if (result == false)
            {
                CLog.Error("RemoveTasks id [%d1] not found", d1: id);
                return;
            }

            freeTaskIdQueue.Enqueue(id);
            result = false;
            {
                lock (taskData[readIndex].dataLock)
                {
                    if (isAppTerminated == true)
                    {
                        CLog.Debug("isAppTerminated is true");
                        return;
                    }
                    result = taskData[readIndex].taskDictionary[(uint)(priority)].Remove(id);
                }
            }
            {
                lock (taskData[writeIndex].dataLock)
                {
                    result = result || taskData[writeIndex].taskDictionary[(uint)(priority)].Remove(id);
                }
            }
            if (result == false)
            {
                CLog.Debug("RemoveTasks id [%d1] not found", d1: id);
            }
        }

        /// <summary>
        /// Checks if the task with the given id is currently in the queue
        /// </summary>
        /// <param name="id">id of task to be checked</param>
        /// <returns>Returns true if the task is currently in the queue, false if already processed or not found</returns>
        public bool IsTaskCurrentlyInQueue(uint id)
        {
            Priority priority;
            try
            {
                priority = taskIdPriorityDictionary[id];
            }
            catch (KeyNotFoundException)
            {
                CLog.Error("Task id [%d1] not found", d1: id);
                return false;
            }

            if (taskData[readIndex].taskDictionary[(uint)priority].ContainsKey(id) == true
                || taskData[writeIndex].taskDictionary[(uint)priority].ContainsKey(id) == true)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Forcefully invokes the task with the given id, if the task is currently in the queue
        /// </summary>
        /// <param name="id">id of task to be invoked</param>
        public void ForceInvokeTask(uint id)
        {
            Priority priority;
            try
            {
                priority = taskIdPriorityDictionary[id];
            }
            catch (KeyNotFoundException)
            {
                CLog.Error("ForceInvokeTask id [%d1] not found", d1: id);
                return;
            }

            if (IsTaskCurrentlyInQueue(id) == false)
            {
                return;
            }

            uint index = (taskData[readIndex].taskDictionary[(uint)priority].ContainsKey(id) == true) ? readIndex : writeIndex;
            taskData[index].taskDictionary[(uint)priority][id]?.Invoke();
            taskData[index].taskDictionary[(uint)priority].Remove(id);
            if (taskIdPriorityDictionary.TryRemove(id, out Priority p) == false)
            {
                CLog.Debug("ForceInvokeTask id [%d1] not found", d1: id);
            }
            freeTaskIdQueue.Enqueue(id);
        }

        internal static void OnAppPaused(object sender, EventArgs e)
        {
            UITaskManager.Instance.ProcessPendingTasks();
        }

        internal static void OnAppTerminate(object sender, EventArgs e)
        {
            UITaskManager.Instance.OnAppTerminate();
        }

        /// <summary>
        /// Connect App pause event, process all pending tasks when app Pause event is received.
        /// Connect App Terminate event, remove ecore idler callback when event reveived.
        /// </summary>
        internal static void ConnectAppLifeCycleSignals()
        {
            FluxApplication fluxApplication = NUIApplication.Current as FluxApplication;
            if (fluxApplication != null)
            {
                fluxApplication.Paused += OnAppPaused;
                fluxApplication.Terminated += OnAppTerminate;
            }
        }

        private UITaskManager()
        {
            for (int i = 0; i < taskData.Length; ++i)
            {
                taskData[i] = new TaskData();
            }

            //ConnectAppPauseSignal function is calling DALi API, so making sure it gets called from Main thread only
            Interop.Ecore.EcoreMainLoopThreadSafeCallAsync((value) => { ConnectAppLifeCycleSignals(); }, IntPtr.Zero);
        }

        private void OnAppTerminate()
        {
            lock (taskData[readIndex].dataLock)
            {
                isAppTerminated = true;
                Interop.Ecore.EcoreIdlerDelete(ecoreIdlerPtr);
                ecoreIdlerPtr = IntPtr.Zero;
                ecoreIdlerAdded = false;
            }
        }

        /// <summary>
        /// Update priority of Pending Mid and Low priority task, to prevent process starvation issue due to Priority scheduling.
        /// </summary>
        private void UpdateEcoreIdlerTaskPriority()
        {
            lock (taskData[writeIndex].dataLock)
            {
                for (uint i = (uint)Priority.High; i > (uint)Priority.Low; --i)
                {
                    foreach (KeyValuePair<uint, Action> item in taskData[writeIndex].taskDictionary[i - 1])
                    {
                        taskData[writeIndex].taskDictionary[i].Add(item.Key, item.Value);
                    }
                    taskData[writeIndex].taskDictionary[i - 1].Clear();
                }
            }
        }

        // copy registered tasks from index 0 dictionary to index 1 dictionary
        private void CopyClientDictionaryToEcoreIdlerDictionary()
        {
            // copying task from client dictionary to EcoreIdler dictionary
            lock (taskData[readIndex].dataLock)
            {
                lock (taskData[writeIndex].dataLock)
                {
                    for (uint i = 0; i < (uint)Priority.Last; ++i)
                    {
                        foreach (KeyValuePair<uint, Action> item in taskData[readIndex].taskDictionary[i])
                        {
                            taskData[writeIndex].taskDictionary[i].Add(item.Key, item.Value);
                        }
                        taskData[readIndex].taskDictionary[i].Clear();
                    }
                }
            }
        }

        /// <summary>
        /// ecore idler callback renew required or not
        /// </summary>
        /// <returns>true if renew required otherwise false</returns>
        private bool RenewEcoreIdlerCallback()
        {
            bool result = false;
            {
                lock (taskData[readIndex].dataLock)
                {
                    lock (taskData[writeIndex].dataLock)
                    {
                        for (uint i = 0; (i < (uint)Priority.Last) && (result == false); ++i)
                        {
                            result = result || (taskData[readIndex].taskDictionary[i].Count > 0);
                            result = result || (taskData[writeIndex].taskDictionary[i].Count > 0);
                        }
                        if (result == false)
                        {
                            Interop.Ecore.EcoreIdlerDelete(ecoreIdlerPtr);
                            ecoreIdlerPtr = IntPtr.Zero;
                            ecoreIdlerAdded = false;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Forcefully processes all pending tasks
        /// </summary>
        private void ProcessPendingTasks()
        {
            //Process Tasks
            {
                lock (taskData[writeIndex].dataLock)
                {
                    ProcessTasks(writeIndex);
                }
            }
            {
                lock (taskData[readIndex].dataLock)
                {
                    ProcessTasks(readIndex);
                }
            }
            taskIdPriorityDictionary.Clear();
        }

        private void ProcessTasks(uint type)
        {
            for (uint i = 0; (i < (uint)Priority.Last); ++i)
            {
                foreach (KeyValuePair<uint, Action> item in taskData[type].taskDictionary[i])
                {
                    uint id = item.Key;
                    item.Value?.Invoke();
                    taskData[type].taskDictionary[i].Remove(id);
                    freeTaskIdQueue.Enqueue(id);
                }
            }
        }

        /// <summary>
        /// Added as ecore idler callback. Called from main thread
        /// </summary>
        /// <returns> Returns true in case of pending task, if all task has been processed returns false</returns>
        private bool OnEcoreIdlerCallback()
        {
            UpdateEcoreIdlerTaskPriority();
            CopyClientDictionaryToEcoreIdlerDictionary();

            //Process Tasks
            Stopwatch stopwatch = new Stopwatch();
            lock (taskData[writeIndex].dataLock)
            {
                stopwatch.Start();
                bool processTasks = true;
                for (uint i = 0; (i < (uint)Priority.Last) && (processTasks == true); ++i)
                {
                    foreach (KeyValuePair<uint, Action> item in taskData[writeIndex].taskDictionary[i])
                    {
                        uint id = item.Key;
                        item.Value?.Invoke();
                        taskData[writeIndex].taskDictionary[i].Remove(id);
                        if (taskIdPriorityDictionary.TryRemove(id, out Priority priority) == false)
                        {
                            CLog.Debug("RemoveTasks id [%d1] not found", d1: id);
                        }
                        freeTaskIdQueue.Enqueue(id);
                        if (stopwatch.ElapsedMilliseconds > burstTime)
                        {
                            stopwatch.Stop();
                            processTasks = false;
                            break;
                        }
                    }
                }
            }
            bool result = RenewEcoreIdlerCallback();
            return result;
        }

        private void AddEcoreIdler()
        {
            ecoreIdlerPtr = Interop.Ecore.EcoreIdlerAdd((value) => { bool result = UITaskManager.Instance.OnEcoreIdlerCallback(); return result; }, IntPtr.Zero);
        }

        /// <summary>
        /// Returns unique id
        /// </summary>
        /// <returns></returns>
        private uint GetNextFreeId()
        {
            bool result = freeTaskIdQueue.TryDequeue(out uint id);
            if (result == false)
            {
                if (freeTaskIdQueue.IsEmpty == true)
                {
                    currentMaxId++;
                    id = currentMaxId;
                }
            }
            return id;
        }

        private class TaskData
        {
            public TaskData()
            {
                for (int i = 0; i < taskDictionary.Length; ++i)
                {
                    taskDictionary[i] = new Dictionary<uint, Action>();
                }
            }
            public Dictionary<uint, Action>[] taskDictionary = new Dictionary<uint, Action>[3]; // taskId is key & Action is user task
            public object dataLock = new object();
        }

        // index 0 taskData used when client adds/removes task, will call it client side dictionary
        // index 1 used in ecore idler callback, will call it ecore idler dictionary.
        // data from index 0 to index 1 is copied in ecoreidler callback
        private readonly TaskData[] taskData = new TaskData[2]; // double buffer dictionary

        // used to identify the priority of a task using its unique id. useful in Remove API
        private readonly ConcurrentDictionary<uint, Priority> taskIdPriorityDictionary = new ConcurrentDictionary<uint, Priority>();

        // Contains free unique id that can be used for new task
        private readonly ConcurrentQueue<uint> freeTaskIdQueue = new ConcurrentQueue<uint>();
        private uint currentMaxId = 0;

        private IntPtr ecoreIdlerPtr = IntPtr.Zero;
        private bool ecoreIdlerAdded = false;
        private bool isAppTerminated = false;

        private const long burstTime = 10;  // 10 milliseconds
        private const uint readIndex = 0;
        private const uint writeIndex = 1;

        private static readonly UITaskManager instance = new UITaskManager();
    }
}

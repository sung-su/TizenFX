using System;
using System.Collections.Generic;
using System.Threading;
using Tizen.NUI;
using Tizen.TV.Broadcast.TVService.Types;
using Tizen.TV.UI.SharedAPI;

namespace Tizen.NUI.FLUX
{
    internal sealed class TVWindowControlAdaptorBehavior : IVideoCanvasAdaptorBehavior
    {
        private enum UpdateType
        {
            NONE,
            UPDATE_GEOMETRY,
            UPDATE_GEOMETRY_EXTENSION,
            UPDATE_ATTRIBUTE
        };

        private static bool isFirstSetTVTypeCall = true;
        private int windowResourceID = 0;
        private readonly (int width, int height) baseResolution = ResolutionUtil.GetCurrentBaseResolution();

        private Thread asyncUpdateThread;
        private AutoResetEvent waitHandle = new AutoResetEvent(initialState: false);
        private readonly object syncObject = new object();
        private readonly object attributeSyncObject = new object();

        private List<AttributeSet> attributeList = new List<AttributeSet>();
        private List<UpdateType> taskList = new List<UpdateType>();
        private HashSet<UpdateType> taskSet = new HashSet<UpdateType>();
        private AttributeSet globalAttribute = new AttributeSet();

        private bool exitUpdateThread = false;
        private bool isAsyncMode = false;

        private struct AttributeSet
        {
            public UpdateType UpdateType { get; set; }
            public VideoAttribute VideoAttribute { set; get; }
            public WindowAttribute WindowAttribute { set; get; }
            public Rectangle DisplayArea { set; get; }
            public TVAttribute TVAttribute { set; get; }
            public int WindowOrientation { set; get; }
        }

        public ScalerTypes ScalerType { get; } = ScalerTypes.Default;

        public Profile Profile { get; } = Profile.Main;

        private ITVWindowControl TVWindowControl { get; }

        private IVideoWindowControlExtension VideoWindowControlExtension { get; set; }

        private VideoCanvasView VideoCanvasView { get; set; }

        private Window VideoCanvasViewWindow { get; set; }

        public TVWindowControlAdaptorBehavior(ITVWindowControl tvWindowControl, VideoCanvasView videoCanvasView, bool asynchronousUpdateMode = false, ScalerTypes scalerType = ScalerTypes.Main)
        {
            CLog.Info("TVWindowVideoCanvasAdaptor ScalerType:(%s1) Profile:(%s2)"
                , s1: CLog.EnumToString(scalerType)
                , s2: CLog.EnumToString(Profile)
                );
            TVWindowControl = tvWindowControl;

            if (tvWindowControl is IVideoWindowControlExtension videoWindowControlExtension)
            {
                VideoWindowControlExtension = videoWindowControlExtension;
            }

            VideoCanvasView = videoCanvasView;
            ScalerType = scalerType;

            if (asynchronousUpdateMode == true)
            {
                lock (syncObject)
                {
                    isAsyncMode = true;
                }
                CreateUpdateThread();
            }

            if (VideoCanvasView.IsOnWindow)
            {
                UpdateAttribute();
            }
        }

        private void CreateUpdateThread()
        {
            exitUpdateThread = false;
            CLog.Info("Create AsynchronousUpdateThread.");
            asyncUpdateThread = new Thread(RunUpdateThread);
            CLog.Info("AsynchronousUpdate Thread Start.");
            asyncUpdateThread.Start();
            CLog.Info("Create AsynchronousUpdateThread done.");
        }

        private void DestroyUpdateThread()
        {
            if (asyncUpdateThread != null)
            {
                CLog.Info("CleanUp AsynchronousUpdateThread.");

                lock (syncObject)
                {
                    exitUpdateThread = true;
                }

                waitHandle.Set();

                asyncUpdateThread.Join();
                asyncUpdateThread = null;

                attributeList.Clear();
                taskList.Clear();
                taskSet.Clear();

                CLog.Info("CleanUp AsynchronousUpdateThread done.");
            }
        }

        private void RunUpdateThread(object obj)
        {
            CLog.Info("Run UpdateThread");
            while (true)
            {
                waitHandle.WaitOne();

                lock (syncObject)
                {
                    if (exitUpdateThread == true)
                    {
                        CLog.Info("Exit VideoCanvasView AsynchronousUpdate Thread.");
                        return;
                    }
                }

                lock (attributeSyncObject)
                {
                    foreach (UpdateType updateType in taskList)
                    {
                        AttributeSet attribute = new AttributeSet
                        {
                            UpdateType = updateType
                        };
                        if (updateType == UpdateType.UPDATE_GEOMETRY)
                        {
                            attribute.DisplayArea = new Rectangle(globalAttribute.DisplayArea.X, globalAttribute.DisplayArea.Y, globalAttribute.DisplayArea.Width, globalAttribute.DisplayArea.Height);
                            attribute.WindowOrientation = globalAttribute.WindowOrientation;
                        }
                        else if (updateType == UpdateType.UPDATE_GEOMETRY_EXTENSION)
                        {
                            attribute.VideoAttribute = new VideoAttribute(globalAttribute.VideoAttribute.X, globalAttribute.VideoAttribute.Y, globalAttribute.VideoAttribute.Width, globalAttribute.VideoAttribute.Height);
                            attribute.WindowAttribute = new WindowAttribute(globalAttribute.WindowAttribute.Position, globalAttribute.WindowAttribute.Size, globalAttribute.WindowAttribute.Degree, globalAttribute.WindowAttribute.BaseScreenResolution);
                        }
                        else if (updateType == UpdateType.UPDATE_ATTRIBUTE)
                        {
                            attribute.TVAttribute = new TVAttribute(globalAttribute.TVAttribute.WaylandSurfaceID, globalAttribute.TVAttribute.GroupID, globalAttribute.TVAttribute.Profile, globalAttribute.TVAttribute.BaseScreenResolution);
                        }

                        attributeList.Add(attribute);
                    }

                    taskList.Clear();
                    taskSet.Clear();
                }

                foreach (AttributeSet attribute in attributeList)
                {
                    CLog.Info("UpdateType: %s1", s1: CLog.EnumToString(attribute.UpdateType));
                    if (attribute.UpdateType == UpdateType.UPDATE_GEOMETRY)
                    {
                        TVWindowControl?.UpdateTVGeometry(attribute.DisplayArea.X, attribute.DisplayArea.Y, attribute.DisplayArea.Width, attribute.DisplayArea.Height, attribute.WindowOrientation);
                    }
                    else if (attribute.UpdateType == UpdateType.UPDATE_GEOMETRY_EXTENSION)
                    {
                        VideoWindowControlExtension?.SetDisplayArea(attribute.VideoAttribute, attribute.WindowAttribute);
                    }
                    else if (attribute.UpdateType == UpdateType.UPDATE_ATTRIBUTE)
                    {
                        TVWindowControl?.SetTVAttribute(attribute.TVAttribute);
                    }
                }
                attributeList.Clear();
            }
        }

        public void CleanUp()
        {
            DestroyUpdateThread();
        }

        public void UpdateGeometry(Rectangle displayArea)
        {
            if (isAsyncMode == false)
            {
                TVWindowControl.UpdateTVGeometry(displayArea.X, displayArea.Y, displayArea.Width, displayArea.Height, globalAttribute.WindowOrientation);
            }
            else
            {
                lock (attributeSyncObject)
                {
                    UpdateTaskList(UpdateType.UPDATE_GEOMETRY);
                    globalAttribute.DisplayArea = displayArea;
                }

                waitHandle.Set();
            }
        }

        public void UpdateGeometry(VideoAttribute videoAttribute, WindowAttribute windowAttribute)
        {
            if (isAsyncMode == false)
            {
                VideoWindowControlExtension?.SetDisplayArea(videoAttribute, windowAttribute);
            }
            else
            {
                lock (attributeSyncObject)
                {
                    UpdateTaskList(UpdateType.UPDATE_GEOMETRY_EXTENSION);
                    globalAttribute.VideoAttribute = videoAttribute;
                    globalAttribute.WindowAttribute = windowAttribute;
                }

                waitHandle.Set();
            }
        }

        public void UpdateAttribute()
        {
            VideoCanvasViewWindow = Window.Get(VideoCanvasView);
            if (VideoCanvasViewWindow != null)
            {
                SetTVType(forceUpdate: true);
            }
        }

        public void SetAsyncUpdateMode(bool isEnabled)
        {
            CLog.Info("SetAsynchrounousUpdateMode : %d1", d1: Convert.ToInt32(isEnabled));
            lock (syncObject)
            {
                isAsyncMode = isEnabled;
            }
            if (asyncUpdateThread == null && isEnabled == true)
            {
                CreateUpdateThread();
            }
        }

        private void SetTVType(bool forceUpdate = false)
        {
            if (!CanExecute())
            {
                return;
            }

            windowResourceID = VideoCanvasViewWindow.GetResourceID();
            CLog.Info("GetResourceID of VideoCanvasViewWindow");
            TVAttribute attr = new TVAttribute((uint)windowResourceID, (int)ScalerType, Profile, baseResolution);

            if (isAsyncMode == false)
            {
                globalAttribute.WindowOrientation = DisplayMetrics.Instance.GetTVAngle(VideoCanvasViewWindow.GetCurrentOrientation());
                TVWindowControl.SetTVAttribute(attr);
            }
            else
            {
                lock (attributeSyncObject)
                {
                    UpdateTaskList(UpdateType.UPDATE_ATTRIBUTE);
                    globalAttribute.TVAttribute = attr;
                    globalAttribute.WindowOrientation = DisplayMetrics.Instance.GetTVAngle(VideoCanvasViewWindow.GetCurrentOrientation());
                }

                waitHandle.Set();
            }

            CLog.Info("SetTVType WIID:(%d1) ScalerType:(%s1) Profile:(%s2) BaseResolution:(%d2 x %d3)"
                , d1: windowResourceID
                , s1: CLog.EnumToString(ScalerType)
                , s2: CLog.EnumToString(Profile)
                , d2: attr.BaseScreenResolution.width
                , d3: attr.BaseScreenResolution.height
                );

            bool CanExecute()
            {
                if (isFirstSetTVTypeCall)
                {
                    isFirstSetTVTypeCall = false;
                    return true;
                }

                if (forceUpdate)
                {
                    return true;
                }

                if (VideoCanvasView.IsPaused())
                {
                    CLog.Warn("SetTVType call is invalid for current state");
                    return false;
                }

                return true;
            }
        }

        private void UpdateTaskList(UpdateType updateType)
        {
            if (taskSet.Contains(updateType))
            {
                CLog.Info("Task Queue already contains %s1", s1: CLog.EnumToString(updateType));
                taskList.Remove(updateType);
                taskList.Add(updateType);
            }
            else
            {
                CLog.Info("New type %s1 added to Task Queue", s1: CLog.EnumToString(updateType));
                taskList.Add(updateType);
                taskSet.Add(updateType);
            }
        }
    }
}
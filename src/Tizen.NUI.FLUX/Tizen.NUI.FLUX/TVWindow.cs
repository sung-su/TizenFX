/// @file TVWindow.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.Collections.Generic;
using System.Globalization;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is delegate of keycombination handler 
    /// </summary>
    /// <param name="combination"> Add user handler about Keycombination</param>
    public delegate void KeyCombinationDelegate(KeyCombinations combination);

    /// <summary>
    /// This is Window include extension method in Window class
    /// </summary>
    /// <code>
    /// Window.Instance.GrabKey("1", KeyGrabModes.Registered);
    /// Window.Instance.UnGrabKey("1", KeyGrabModes.Registered);
    /// Window.Instance.GrabAllKey(KeyGrabModes.Registered);
    /// </code>
    public static class TVWindow
    {
        private static readonly Dictionary<IntPtr, Cursor> cursorMap = new Dictionary<IntPtr, Cursor>();
        private static readonly Dictionary<IntPtr, KeyRouter> keyRouterMap = new Dictionary<IntPtr, KeyRouter>();
        private static readonly Dictionary<IntPtr, int> resourceIDMap = new Dictionary<IntPtr, int>();
        private const int InvalidWindowResourceID = 0;
        //TODO: Need to check this vconf key is available or not across all products.
        //private static readonly string serverReceivedLastKeyEvent = "memory/window_system/input/early_key_events";

        /// <summary>
        /// It is called automatically before the first instance is created or any static members are referenced.
        /// </summary>
        static TVWindow()
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// User can get cursor object for using cursor
        /// </summary>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <returns> Cursor object </returns>
        public static Cursor GetCursor(this Window window)
        {
            if (cursorMap.ContainsKey(window.GetNativeWindowHandler()) == false)
            {
                Cursor cursor = new Cursor(window.GetNativeWindowHandler());
                cursorMap.Add(window.GetNativeWindowHandler(), cursor);
                return cursor;
            }
            else
            {
                return cursorMap[window.GetNativeWindowHandler()];
            }
        }

        /// <summary>
        /// This function is used to set the window to ignore all the keys in focus state.
        /// </summary>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to set IgnoreKeyevent
        /// </exception>
        public static void IgnoreKeyEvent(this Window window)
        {
            GetKeyRouter(window).IgnoreKeyEvent();
        }

        /// <summary>
        /// This function is used to re-set the window to receive all the keys in focus state.
        /// </summary>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to set ReceiveKeyEvent
        /// </exception>
        public static void ReceiveKeyEvent(this Window window)
        {
            GetKeyRouter(window).ReceiveKeyEvent();
        }

        /// <summary>
        /// Grab key which you want to mode in this window
        /// If you can use TVkeyGrabModes except Topmost mode, you should add privileges. 
        /// </summary>
        /// <privlevel> Platform </privlevel>
        /// <privilege> http://tizen.org/privilege/keygrab </privilege>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <param name="keyName"> keyname of the key to be grabbed or registered </param>
        /// <param name="mode"> mode in which the key is to be grabbed or registered </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to set GrabKey
        /// </exception>
        public static void GrabKey(this Window window, string keyName, TVKeyGrabModes mode)
        {
            GetKeyRouter(window).GrabKey(keyName, mode);
        }

        /// <summary>
        /// Ungrab key which you want to mode about grab key in this window
        /// If you can use TVkeyGrabModes except Topmost mode, you should add privileges. 
        /// </summary>
        /// <privlevel> Platform </privlevel>
        /// <privilege> http://tizen.org/privilege/keygrab </privilege>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <param name="keyName"> keyname of the key to be ungrabbed or unregistered </param>
        /// <param name="mode"> mode in which the key is to be ungrabbed or unregistered </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to UnGrabKey
        /// </exception>
        public static void UnGrabKey(this Window window, string keyName, TVKeyGrabModes mode)
        {
            GetKeyRouter(window).UnGrabKey(keyName, mode);
        }

        /// <summary>
        /// Grab all key which you want to mode in this window
        /// If you can use TVkeyGrabModes except Topmost mode, you should add privileges. 
        /// </summary>
        /// <privlevel> Platform </privlevel>
        /// <privilege> http://tizen.org/privilege/keygrab </privilege>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <param name="mode"> mode in which the key is to be grabbed or registered </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to GrabAllKey
        /// </exception>
        public static void GrabAllKey(this Window window, TVKeyGrabModes mode)
        {
            GetKeyRouter(window).GrabAllKey(mode);
        }

        /// <summary>
        /// UnGrab all key which you want to mode in this window
        /// If you can use TVkeyGrabModes except Topmost mode, you should add privileges. 
        /// </summary>
        /// <version> 6.6.0 </version>
        /// <privlevel> Platform </privlevel>
        /// <privilege> http://tizen.org/privilege/keygrab </privilege>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <param name="mode"> mode in which the key is to be grabbed or registered </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to GrabAllKey
        /// </exception>
        public static void UnGrabAllKey(this Window window, TVKeyGrabModes mode)
        {
            GetKeyRouter(window).UnGrabAllKey(mode);
        }

#pragma warning disable CS1570
        // XML comment has badly formed XML
        /// <summary>
        /// Grab list of key which you want to mode at once in this window
        /// If you can use TVkeyGrabModes except Topmost mode, you should add privileges. 
        /// </summary>
        /// <privlevel> Platform </privlevel>
        /// <privilege> http://tizen.org/privilege/keygrab </privilege>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <param name="keyGrabList"> User enter the list of key which you want to grab mode </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to set GrabKey
        /// </exception>
        /// <code>
        /// keygrabList = new List<KeyValuePair<string, TVKeyGrabModes>>();
        /// keygrabList.Add(new KeyValuePair<string, TVKeyGrabModes>("XF86Back", TVKeyGrabModes.Registered));
        /// Window.Instance.GrabKeyList(keygrabList);
        /// </code>
#pragma warning restore CS1570 // XML comment has badly formed XML
        public static void GrabKeyList(this Window window, List<KeyValuePair<string, TVKeyGrabModes>> keyGrabList)
        {
            GetKeyRouter(window).GrabKeyList(keyGrabList);
        }

#pragma warning disable CS1570
        /// <summary>
        /// Ungrab list of key which you want to mode about grab key in this window
        /// If you can use TVkeyGrabModes except Topmost mode, you should add privileges. 
        /// </summary>
        /// <privlevel> Platform </privlevel>
        /// <privilege> http://tizen.org/privilege/keygrab </privilege>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <param name="keyUnGrabList"> User enter the list of key which you want to ungrab mode </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to UnGrabKey
        /// </exception>
        /// <code>
        /// keygrabList = new List<KeyValuePair<string, TVKeyGrabModes>>();
        /// keygrabList.Add(new KeyValuePair<string, TVKeyGrabModes>("XF86Back", TVKeyGrabModes.Registered));
        /// Window.Instance.UnGrabKeyList(keygrabList);
        /// </code>
#pragma warning restore CS1570 // XML comment has badly formed XML
        public static void UnGrabKeyList(this Window window, List<KeyValuePair<string, TVKeyGrabModes>> keyUnGrabList)
        {
            GetKeyRouter(window).UnGrabKeyList(keyUnGrabList);
        }

        /// <summary>
        /// This function will add handler to be called for specific key combination.
        /// </summary>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <param name="keys"> Keycombination which application wants to listen </param>
        /// <param name="handler"> This hanlder to be called when specified key combination </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to AddKeyCombinationHandler
        /// </exception>
        public static void AddKeyCombinationHandler(this Window window, KeyCombinations keys, KeyCombinationDelegate handler)
        {
            KeyCombination.Instance.AddKeyCombinationHandler(keys, handler);
        }

        /// <summary>
        /// This function will remove handler to be called for specific key combination.
        /// </summary>
        /// <param name="window"> It is indicated extension method of Window class </param>
        /// <param name="keys"> Keycombination which application wants to listen </param>
        /// <param name="handler"> This hanlder to be deleted </param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to RemoveKeyCombinationHandler
        /// </exception>
        public static void RemoveKeyCombinationHandler(this Window window, KeyCombinations keys, KeyCombinationDelegate handler)
        {
            KeyCombination.Instance.RemoveKeyCombinationHandler(keys, handler);
        }

        /// <summary>
        /// Extension method to retrive Native Window Handle
        /// </summary>
        /// <param name="window">NUI Window Instance</param>
        /// <returns>EcoreWindowHandle</returns>

        public static IntPtr GetWindowHandle(this Window window)
        {
            return window.GetNativeWindowHandler();
        }

        /// <summary>
        /// Extension method for resource Id of window
        /// </summary>
        /// <param name="window">NUI Window Instance</param>
        /// <returns>Resource Id of window </returns>
        public static int GetResourceID(this Window window)
        {
            if (!Window.IsInstalled())
            {
                CLog.Fatal("This has been called from worker thread. This API must be called from MainThread.");
                CLog.Fatal(Environment.StackTrace);
            }

            int resourceID = InvalidWindowResourceID;
            IntPtr handle = window.GetNativeWindowHandler();

            if (resourceIDMap.TryGetValue(handle, out resourceID))
            {
                return resourceID;
            }
            else
            {
                resourceID = Interop.WindowUtil.GetResourceId(handle);
                if (resourceID > InvalidWindowResourceID)
                {
                    resourceIDMap.Add(window.GetNativeWindowHandler(), resourceID);
                }
                else
                {
                    CLog.Fatal("GetResourceId failed !!");
                }
                return resourceID;
            }
        }

        /// <summary>
        /// Extension method for capturing a window surface as image
        /// </summary>
        /// <param name="window">NUI Window Instance</param>
        /// <param name="captureCallback">Window surface capture complete callback</param>
        /// <param name="path">Path where captured image needs to be dumped</param>
        /// <param name="filename">filename without extension, of the captured image. Extension is fixed as .png</param>
        public static void CaptureWindowSurfaceAsFile(this Window window, Action<WindowCaptureResult> captureCallback, string path, string filename)
        {
            WindowCaptureUtil.Instance.Captured = captureCallback;
            Interop.WindowUtil.CaptureWindowSurfaceAsFile(window.GetNativeWindowHandler(), WindowCaptureUtil.Instance.nativeCallback, path, filename);
        }

        /// <summary>
        /// GetKeyRouter - Windows extension method to get KeyRouter Object
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        internal static KeyRouter GetKeyRouter(Window window)
        {
            if (keyRouterMap.ContainsKey(window.GetNativeWindowHandler()) == false)
            {
                KeyRouter keyRouter = new KeyRouter(window.GetNativeWindowHandler());
                keyRouterMap.Add(window.GetNativeWindowHandler(), keyRouter);
                return keyRouter;
            }
            else
            {
                return keyRouterMap[window.GetNativeWindowHandler()];
            }
        }

        /// <summary>
        /// This is Window capture util internal class
        /// </summary>
        internal class WindowCaptureUtil
        {
            public Action<WindowCaptureResult> Captured;
            private static readonly WindowCaptureUtil instance = new WindowCaptureUtil();
            public Interop.WindowUtil.NativeCaptureWindowSurfaceCallback nativeCallback;

            private WindowCaptureUtil()
            {
                nativeCallback = NativeCaptureCallbackFunction;
            }

            public static WindowCaptureUtil Instance => instance;

            //userData is passed by vd-win-util in callback but it is not to be used
            public void NativeCaptureCallbackFunction(IntPtr userData, int result)
            {
                WindowCaptureResult res = (WindowCaptureResult)result;
                Captured?.Invoke(res);
            }
        }

        /// <summary>
        /// This is KeyRouter internal class 
        /// </summary>
        internal class KeyRouter
        {
            private bool initCheck = false;
            private Interop.WindowUtil.ErrorType errorType;
            private bool isGrabKey = false;

            private readonly IntPtr ecoreWindowNativeHandle;
            internal KeyRouter(IntPtr handle)
            {
                ecoreWindowNativeHandle = handle;
                InitKeyRouter();
            }

            private void InitKeyRouter()
            {
                if (initCheck == false)
                {
                    errorType = (Interop.WindowUtil.ErrorType)Interop.WindowUtil.InitializeWindowUtility();
                    if (errorType != Interop.WindowUtil.ErrorType.Success)
                    {
                        throw new InvalidOperationException("Failed to InitializeDaliWindow , Error type : " + errorType);
                    }

                    if (Interop.KeyRouter.Initialize() == false)
                    {
                        throw new InvalidOperationException("Failed to initialize KeyRouter");
                    }
                    initCheck = true;
                }
            }

            public void IgnoreKeyEvent()
            {
                CLog.Debug("IgnoreKeyEvent");
                if (Interop.KeyRouter.RegisterIgnoreAllKey(ecoreWindowNativeHandle) == false)
                {
                    CLog.Error("Failed to set IgnoreKeyEvent");
                    throw new InvalidOperationException("Failed to set IgnoreKeyEvent");
                }
            }

            public void ReceiveKeyEvent()
            {
                CLog.Debug("ReceiveKeyEvent");
                if (Interop.KeyRouter.UnRegisterIgnoreAllKey(ecoreWindowNativeHandle) == false)
                {
                    CLog.Error("Failed to set ReceiveKeyEvent");
                    throw new InvalidOperationException("Failed to set ReceiveKeyEvent");
                }
            }

            public void GrabKey(string keyName, TVKeyGrabModes mode)
            {
                CLog.Error("GrabKey keyname: %s1, KeyGrabMode: %s2", s1: keyName, s2: CLog.EnumToString(mode));
                Interop.KeyRouter.ResetKeyList();

                if (Interop.KeyRouter.AddKey(keyName, (int)mode) == false)
                {
                    CLog.Error("Failed to GrabKey addKey");
                    throw new InvalidOperationException("Failed to GrabKey addKey, Keyname - " + keyName + " TVKeyGrabMode - " + mode);
                }

                Interop.KeyRouter.SetKeyList(ecoreWindowNativeHandle);

                isGrabKey = true;
            }

            public void UnGrabKey(string keyName, TVKeyGrabModes mode)
            {
                CLog.Debug("UnGrabKey keyname: %s1, mode: %s2", s1: keyName, s2: CLog.EnumToString(mode));
                if (!isGrabKey)
                {
                    CLog.Error("Need to call GrabKey before calling UnGrabKey");
                    return;
                }

                Interop.KeyRouter.ResetKeyList();
                if (Interop.KeyRouter.AddKey(keyName, (int)mode) == false)
                {
                    CLog.Error("Failed to UnGrabKey addKey");
                    throw new InvalidOperationException("Failed to UnGragKey addkey, Keyname - " + keyName + " TVKeyGrabMode - " + mode);
                }
                Interop.KeyRouter.UnSetKeyList(ecoreWindowNativeHandle);
            }

            public void GrabAllKey(TVKeyGrabModes mode)
            {
                CLog.Debug("GrabAllKey mode: %s1", s1: CLog.EnumToString(mode));
                Interop.KeyRouter.ResetKeyList();
                if (Interop.KeyRouter.AddAllKey((int)mode) == false)
                {
                    CLog.Error("Failed to GrabAllKey AddAllKey");
                    throw new InvalidOperationException("Failed to GrabAllKey AddAllKey, TVKeyGrabMode - " + mode);
                }
                Interop.KeyRouter.SetKeyList(ecoreWindowNativeHandle);
                isGrabKey = true;
            }

            public void UnGrabAllKey(TVKeyGrabModes mode)
            {
                CLog.Debug("UnGrabAllKey mode: %s1", s1: CLog.EnumToString(mode));
                if (!isGrabKey)
                {
                    CLog.Error("Need to call GrabKey before calling UnGrabKey");
                    return;
                }

                Interop.KeyRouter.ResetKeyList();
                if (Interop.KeyRouter.AddAllKey((int)mode) == false)
                {
                    CLog.Error("Failed to UnGrabAllKey AddAllKey");
                    throw new InvalidOperationException("Failed to UnGrabAllKey AddAllKey, TVKeyGrabMode - " + mode);
                }
                Interop.KeyRouter.UnSetKeyList(ecoreWindowNativeHandle);
            }

            public void GrabKeyList(List<KeyValuePair<string, TVKeyGrabModes>> keyList)
            {
                List<KeyValuePair<string, TVKeyGrabModes>> addKeyList = keyList;
                AddKeyList(addKeyList);
                Interop.KeyRouter.SetKeyList(ecoreWindowNativeHandle);
                addKeyList.Clear();
                isGrabKey = true;
            }

            public void UnGrabKeyList(List<KeyValuePair<string, TVKeyGrabModes>> keyList)
            {
                if (!isGrabKey)
                {
                    CLog.Error("Need to call GrabKey before calling UnGrabKey");
                    return;
                }
                List<KeyValuePair<string, TVKeyGrabModes>> addKeyList = keyList;
                AddKeyList(addKeyList);
                Interop.KeyRouter.UnSetKeyList(ecoreWindowNativeHandle);
                addKeyList.Clear();
                CLog.Debug("UnGrabKeyList Finished");
            }

            private void AddKeyList(List<KeyValuePair<string, TVKeyGrabModes>> keyList)
            {
                Interop.KeyRouter.ResetKeyList();
                foreach (KeyValuePair<string, TVKeyGrabModes> data in keyList)
                {
                    CLog.Debug("Key name : %s1, KeyGrabMode: %s2", s1: data.Key, s2: CLog.EnumToString(data.Value));
                    if (Interop.KeyRouter.AddKey(data.Key, (int)data.Value) == false)
                    {
                        CLog.Error("Failed to GrabKey addKey");
                        throw new InvalidOperationException("Failed to GrabKey addKey, Keyname - " + data.Key + " TVKeyGrabMode - " + data.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Get unit size of window
        /// </summary>
        /// <param name="window">NUI Window Instance</param>
        /// <returns> unit size of window </returns>
        public static UnitSize GetWindowUnitSize(this Window window)
        {
            Size2D windowSize = window.WindowSize;
            return new UnitSize(DisplayMetrics.Instance.PixelToUnit(windowSize.Width), DisplayMetrics.Instance.PixelToUnit(windowSize.Height));
        }

        /// <summary>
        /// Get unit position of window
        /// </summary>
        /// <param name="window">NUI Window Instance</param>
        /// <returns> unit position of window </returns>
        public static UnitPosition GetWindowUnitPosition(this Window window)
        {
            Position2D windowPosition = window.WindowPosition;
            return new UnitPosition(DisplayMetrics.Instance.PixelToUnit(windowPosition.X), DisplayMetrics.Instance.PixelToUnit(windowPosition.Y));
        }

        /// <summary>
        /// Set unit position and unit size of window
        /// To remove flickering issue when user change window position and size, user should set both position and size at once.
        /// </summary>
        /// <param name="window">NUI Window Instance</param>
        /// <param name="unitPosition"> unit postion of window </param>
        /// <param name="unitSize"> unit size of window </param>

        public static void SetWindowUnitPositionSize(this Window window, UnitPosition unitPosition, UnitSize unitSize)
        {
            Rectangle windowPositionSize = new Rectangle(
                DisplayMetrics.Instance.UnitToPixel(unitPosition.X),
                DisplayMetrics.Instance.UnitToPixel(unitPosition.Y),
                DisplayMetrics.Instance.UnitToPixel(unitSize.Width),
                DisplayMetrics.Instance.UnitToPixel(unitSize.Height)
                );

            window.WindowPositionSize = windowPositionSize;
        }

        private static string previousServerReceivedLastKeyEventValue = null;

        internal static void CheckKeyInitialize()
        {
            Window.Instance.FocusChanged += WindowFocusChanged;
        }

        private static void WindowFocusChanged(object sender, Window.FocusChangedEventArgs e)
        {
            //TODO: Need to check Vconf
            //previousServerReceivedLastKeyEventValue = Vconf.GetString(serverReceivedLastKeyEvent);
            CLog.Debug("FocusChanged - Focused : %d1, key : %s1", d1: e.FocusGained == true ? 1 : 0, s1: previousServerReceivedLastKeyEventValue);
        }

        internal static void CheckKeyDestroy()
        {
            Window.Instance.FocusChanged -= WindowFocusChanged;
        }

        /// <summary>
        /// Extension method to check if there is a key that has not arrived in key event queue on system.
        /// </summary>
        /// <param name="window">NUI Window Instance</param>
        /// <param name="clientReceivedKeyTimeStamp">Time of occurrence of the current key being processed in client (NUI) side</param>
        /// <param name="clientReceivedKeyCode">Key code of the current key being processed in client (NUI) side</param>
        /// <param name="serverReceivedKeyTimeStamp">Time of occurrence of the last key stored in memory vconf</param>
        /// <param name="serverReceivedKeyCode">Key code of the last key stored in memory vconf</param>
        /// <returns> If there is a key that has not arrived in key event queue, then return true. </returns>
        /// <version>10.10.0</version>
        public static bool CheckNotArrivedKeyEvent(this Window window, ref uint clientReceivedKeyTimeStamp, ref int clientReceivedKeyCode, ref uint serverReceivedKeyTimeStamp, ref int serverReceivedKeyCode)
        {
            string serverReceivedLastKeyEventValue = null;
            Key clientReceivedLastKeyEvent = Window.Instance.GetLastKeyEvent();


            clientReceivedKeyCode = 0;
            clientReceivedKeyTimeStamp = 0;
            serverReceivedKeyCode = 0;
            serverReceivedKeyTimeStamp = 0;

            //TODO: Need to check Vconf
            //serverReceivedLastKeyEventValue = Vconf.GetString(serverReceivedLastKeyEvent);
            CLog.Debug("PreKey : %s1, ServerKey : %s2", s1: previousServerReceivedLastKeyEventValue, s2: serverReceivedLastKeyEventValue);

            if (string.IsNullOrEmpty(serverReceivedLastKeyEventValue))
            {
                CLog.Error("Server key is null --> false");
                return false;
            }

            ParseServerKeyInfo(serverReceivedLastKeyEventValue, ref serverReceivedKeyCode, ref serverReceivedKeyTimeStamp);
            if (previousServerReceivedLastKeyEventValue == serverReceivedLastKeyEventValue)
            {
                clientReceivedKeyCode = serverReceivedKeyCode;
                clientReceivedKeyTimeStamp = serverReceivedKeyTimeStamp;
                CLog.Info("server & previous key is same -> false, [%d1], [%d2]", d1: serverReceivedKeyCode, d2: serverReceivedKeyTimeStamp);
                return false;
            }

            previousServerReceivedLastKeyEventValue = serverReceivedLastKeyEventValue;
            if (clientReceivedLastKeyEvent == null)
            {
                CLog.Info("Client key is null, but server key is not null -> true , [%d1], [%d2]", d1: serverReceivedKeyCode, d2: serverReceivedKeyTimeStamp);
                return true;
            }

            clientReceivedKeyCode = clientReceivedLastKeyEvent.KeyCode;
            clientReceivedKeyTimeStamp = clientReceivedLastKeyEvent.Time;

            if (serverReceivedKeyTimeStamp == clientReceivedKeyTimeStamp)
            {
                CLog.Info("server [%d1], client[%d2] -> false", d1: serverReceivedKeyTimeStamp, clientReceivedKeyTimeStamp);
                return false;
            }
            else
            {
                CLog.Info("server [%d1], client[%d2] -> true", d1: serverReceivedKeyTimeStamp, clientReceivedKeyTimeStamp);
                return true;
            }
        }

        private static void ParseServerKeyInfo(string serverReceivedLastKeyEventValue, ref int serverReceivedKeyCode, ref uint serverReceivedKeyTimeStamp)
        {
            if (serverReceivedLastKeyEventValue == null)
            {
                return;
            }

            string[] valueList = serverReceivedLastKeyEventValue.Split(':');

            if (valueList.Length != 2)
            {
                CLog.Error("memory/window_system/input/early_key_events is [%s1] so can't split", s1: serverReceivedLastKeyEventValue);
            }
            if (int.TryParse(valueList[0], NumberStyles.Any, CultureInfo.InvariantCulture, out serverReceivedKeyCode) == false)
            {
                CLog.Error("memory/window_system/input/early_key_events is [%s1] so can't split", s1: serverReceivedLastKeyEventValue);
            }
            if (uint.TryParse(valueList[1], NumberStyles.Any, CultureInfo.InvariantCulture, out serverReceivedKeyTimeStamp) == false)
            {
                CLog.Error("memory/window_system/input/early_key_events is [%s1] so can't split", s1: serverReceivedLastKeyEventValue);
            }
        }

        /// <summary>
        /// This is KeyCombination internal class 
        /// </summary>
        internal class KeyCombination
        {
            private static readonly KeyCombination instance = new KeyCombination();
            private readonly List<KeyValuePair<KeyCombinations, KeyCombinationDelegate>> keyCombinationInfoList;

            private KeyCombination()
            {
                if (Interop.KeyCombination.Initialize() == false)
                {
                    throw new InvalidOperationException("Failed to initialize KeyCombination");
                }
                keyCombinationInfoList = new List<KeyValuePair<KeyCombinations, KeyCombinationDelegate>>();
            }

            public static KeyCombination Instance => instance;

            public void AddKeyCombinationHandler(KeyCombinations keys, KeyCombinationDelegate handler)
            {
                KeyValuePair<KeyCombinations, KeyCombinationDelegate> listValue = new KeyValuePair<KeyCombinations, KeyCombinationDelegate>(keys, handler);
                if (keyCombinationInfoList.Contains(listValue) != true)
                {
                    keyCombinationInfoList.Add(listValue);
                    CLog.Debug("The value is already added in list: Key[%s1]", s1: CLog.EnumToString(listValue.Key));
                }

                if (Interop.KeyCombination.AddHandler(keys, handler) == false)
                {
                    CLog.Error("Failed to AddKeyCombinationHandler");
                    throw new InvalidOperationException("Failed to AddKeyCombinationHandler : key - " + keys + " handler - " + handler);
                }
            }

            public void RemoveKeyCombinationHandler(KeyCombinations keys, KeyCombinationDelegate handler)
            {
                CLog.Debug("RemoveKeyCombinationHandler: Key[%s1]", s1: CLog.EnumToString(keys));
                if (Interop.KeyCombination.RemoveHandler(keys, handler) == false)
                {
                    CLog.Error("Failed to RemoveKeyCombinationHandler");
                    throw new InvalidOperationException("Failed to RemoveKeyCombinationHandler key - " + keys + " handler - " + handler);
                }
                KeyValuePair<KeyCombinations, KeyCombinationDelegate> listValue = new KeyValuePair<KeyCombinations, KeyCombinationDelegate>(keys, handler);
                if (keyCombinationInfoList.Contains(listValue) == true)
                {
                    CLog.Debug("The key and KeyCombination delegate exist in list: Key[%s1]", s1: CLog.EnumToString(listValue.Key));
                    keyCombinationInfoList.Remove(listValue);
                }
            }
        }

    }

}
/// @file Cursor.cs
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is Cursor which user want to use enable 
    /// </summary>
    /// <code>
    /// cursor = Window.Instance.GetCursor();
    /// cursor.Enable(true);
    /// cursor.Position = new Position2D(960, 540);
    /// cursor.Shape = Cursor.Shapes.Default;
    /// </code>
    public class Cursor
    {
        private Shapes cursorShape = Shapes.Default;
        private readonly Interop.WindowUtil.ErrorType errorType;
        private readonly IntPtr ecoreWindowNativeHandle;
        internal Cursor(IntPtr handle)
        {
            SecurityUtil.CheckPlatformPrivileges();
            errorType = (Interop.WindowUtil.ErrorType)Interop.WindowUtil.InitializeWindowUtility();
            if (errorType != Interop.WindowUtil.ErrorType.Success)
            {
                throw new InvalidOperationException("Failed to InitializeDaliWindow , Error type : " + errorType);
            }
            if (!Interop.Cursor.Initialize())
            {
                throw new InvalidOperationException("Failed to initialize Cursor");
            }
            ecoreWindowNativeHandle = handle;
        }

        /// <summary>
        /// Enumeration for shape of cursor.
        /// </summary>
        public enum Shapes
        {
            /// <summary> Default shape of cursor /// </summary>
            Default = 0,
            /// <summary> PressAndHold shape of cursor /// </summary>
            PressAndHold,
            /// <summary> Inputfield shape of cursor /// </summary>
            InputField,
            /// <summary> Transparent shape of cursor /// </summary>
            Transparent
        }

        /// <summary>
        /// Set or get shape of cursor (Default, PressAndHold, InputField, Transparent)
        /// </summary>
        public Shapes Shape
        {
            get => cursorShape;
            set
            {
                cursorShape = value;
                string cursorName = cursorShape switch
                {
                    Shapes.Default => "normal_default",
                    Shapes.PressAndHold => "normal_pnh",
                    Shapes.InputField => "normal_input_field",
                    Shapes.Transparent => "normal_transparent",
                    _ => string.Empty
                };

                if (cursorName != string.Empty)
                {
                    Interop.Cursor.SetType(cursorName);
                };
            }
        }

        /// <summary>
        /// Set or get position of Cursor
        /// </summary>
        public Position2D Position
        {
            get
            {
                Interop.Cursor.GetPosition(out int x, out int y);
                Position2D pos = new Position2D(x, y);
                CLog.Debug("GET Cursor Position X : %d1, Cursor Position Y : %d2", d1: pos.X, d2: pos.Y);
                return pos;
            }
            set
            {
                CLog.Debug("SET Cursor Position X : %d1, Cursor Position Y : %d2", value.X, value.Y);
                Interop.Cursor.SetPosition(ecoreWindowNativeHandle, value.X, value.Y);
            }
        }

        /// <summary>
        /// Get enabled state of Cursor
        /// </summary>
        public bool Enabled { private set; get; } = false;

        /// <summary>
        /// Get enabled always state of Cursor
        /// </summary>
        public bool StateAlwaysOn { private set; get; } = false;

        /// <summary>
        /// This function is used to load theme and shape for custom cursor.
        /// </summary>
        /// <param name="theme"> Set custom theme name of cursor </param>
        /// <param name="shape"> Set custom shape name of cursor from this theme</param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to set custom theme of cursor
        /// </exception>
        public void SetCustomType(string theme, string shape)
        {
            bool ret = Interop.Cursor.SetTheme(theme);
            if (!ret)
            {
                throw new InvalidOperationException("Failed to set custom theme of cursor");
            }
            Interop.Cursor.SetType(shape);
        }

        /// <summary>
        /// Set enable state of cursor.
        /// </summary>
        /// <param name="alwaysOn"> User want to show cursor always(true) or not(false = default)</param>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to enable cursor
        /// </exception>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to show cursor always
        /// </exception>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to hide cursor always
        /// </exception>
        public void Enable(bool alwaysOn = false)
        {
            bool ret = Interop.Cursor.EnableMousePointer(ecoreWindowNativeHandle);
            if (!ret)
            {
                throw new InvalidOperationException("Failed to EnableMousePointer");
            }
            if (alwaysOn)
            {
                ret = Interop.Cursor.EnableShowAlways(ecoreWindowNativeHandle);
                if (!ret)
                {
                    throw new InvalidOperationException("Failed to EnableShowAlways");
                }
            }
            else
            {
                ret = Interop.Cursor.DisableShowAlways(ecoreWindowNativeHandle);
                if (!ret)
                {
                    throw new InvalidOperationException("Failed to DisableShowAlways");
                }
            }
            Enabled = true;
            StateAlwaysOn = alwaysOn;
        }

        /// <summary>
        /// Set disable state of cursor.
        /// </summary>
        /// <exception cref='InvalidOperationException'>
        /// Throw failed to disable cursor
        /// </exception>
        public void Disable()
        {
            bool ret = Interop.Cursor.DisableMousePointer(ecoreWindowNativeHandle);
            if (!ret)
            {
                throw new InvalidOperationException("Failed to DisableMousePointer");
            }
            Enabled = false;
        }
    }
}

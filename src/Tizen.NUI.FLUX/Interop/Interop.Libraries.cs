/// @file Interop.Libraries.cs
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

using System.ComponentModel;

namespace Tizen.NUI.FLUX
{
    internal static partial class Interop
    {
        internal static class Libraries
        {
            public const string DaliExtension = Config.TizenNativeLibPath + "libdali-extension-flux.so.0";
            public const string DaliExtension_Create = Config.TizenNativeLibPath + "libdali-extension-flux-create.so.0";
            public const string DaliExtension_Photo = Config.TizenNativeLibPath + "libdali-extension-flux-photo.so.0";
            public const string DaliExtension_VectorView = Config.TizenNativeLibPath + "libdali-extension-flux-vector-view.so.0";
            public const string DaliExtension_VectorAnimation = Config.TizenNativeLibPath + "libdali-extension-flux-vector-animation.so.0";
            public const string DaliExtension_VideoCanvas = Config.TizenNativeLibPath + "libdali-extension-flux-video-canvas.so.0";

            public const string UIFW_Misc = Config.TizenNativeLibPath + "libuifw_misc.so.0";
            public const string Ecore = Config.TizenNativeLibPath + "libecore.so.1";
        }
    }

    /// <summary>
    /// This class provides TizenTV Internal API
    /// </summary>
    /// <remarks>
    /// A Tizen C# application is based on the specified API version information.
    /// </remarks>
    /// <code>
    /// using Tizen.TV.Internal;
    /// internal static partial class Interop
    /// {
    ///     internal static partial class Libraries
    ///     {
    ///         public const string GamepadClient = Config.TizenNativeLibPath + "libSefClientGamepad.so";
    ///     }
    /// }
    /// </code>
    public static class Config
    {
        /// <summary>
        /// Return of the library path according to the architecture
        /// </summary>
        /// <version> 10.10.0 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public const string TizenNativeLibPath = "";
    }
}


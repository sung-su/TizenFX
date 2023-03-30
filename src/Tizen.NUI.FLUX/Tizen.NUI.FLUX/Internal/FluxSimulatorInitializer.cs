/// @file FluxApplicationInitializer.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.Globalization;
using System.IO;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    internal class FluxSimulatorInitializer
    {
        private const string fluxSimulatorFilePath = "/opt/usr/apps/flux_resolution";

        public static void Initialize()
        {
            if (IsSimulatorModeEnabled() == false)
            {
                return;
            }

            try
            {
                if (TryGetValidWindowResolutionFromFile(out Size2D size))
                {
                    Window.Instance.SetWindowSize(size);
                    SetWindowHints();
                    CLog.Info("Application works at flux simulator mode");
                }
            }
            catch (IOException e)
            {
                CLog.Info("Failed to read file: %s1", s1: e.Message);
            }
            catch (ArgumentException e)
            {
                CLog.Info("Failed to read file: %s1", s1: e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                CLog.Info("Failed to read file: %s1", s1: e.Message);
            }
            catch (NotSupportedException e)
            {
                CLog.Info("Failed to read file: %s1", s1: e.Message);
            }
        }

        private static bool IsSimulatorModeEnabled()
        {
            bool isReleaseImage = File.Exists("/etc/release");
            bool isFileExist = File.Exists(fluxSimulatorFilePath);
            return !isReleaseImage && isFileExist;
        }

        private static void SetWindowHints()
        {
            Window.Instance.AddAuxiliaryHint("wm.policy.win.user.geometry", "1");
            Window.Instance.AddAuxiliaryHint("wm.policy.win.transform.mode", "ratiofit");
            Window.Instance.AddAuxiliaryHint("wm.comp.win.effect.enable", "0"); // window transition effect makes noise.
        }

        private static bool TryGetValidWindowResolutionFromFile(out Size2D size)
        {
            size = new Size2D();
            string resolution = File.ReadAllText(fluxSimulatorFilePath).Trim();
            if (!string.IsNullOrEmpty(resolution))
            {
                string[] values = resolution.Split(' ');
                if (values.Length == 2 && (values[0] != "" && values[1] != ""))
                {
                    int width = int.Parse(values[0], CultureInfo.InvariantCulture);
                    int height = int.Parse(values[1], CultureInfo.InvariantCulture);

                    if (IsValidResolutionRange(width, height))
                    {
                        size = new Size2D(width, height);
                        return true;
                    }
                }
            }
            CLog.Error("Failed to get window resolution for FluxSimulator");
            return false;
        }

        private static bool IsValidResolutionRange(int width, int height)
        {
            const int maxScreenSize = 4096;
            if ((width > 0 && width <= maxScreenSize) && (height > 0 && height <= maxScreenSize))
            {
                return true;
            }
            return false;
        }
    }
}
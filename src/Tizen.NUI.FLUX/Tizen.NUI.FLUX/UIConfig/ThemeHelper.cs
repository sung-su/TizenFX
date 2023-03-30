/// @file ThemeHelper.cs
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
using System.Reflection;
using System.Threading;
using Tizen.Applications;
using Tizen.Applications.ThemeManager;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is ThemeHelper class.
    /// You can map object, proprety, colorchip in ThemeHelper.
    /// </summary>
    /// <code>
    /// ThemeHelper.Instance.MapColorChip(view, "BackgroundColor", "CC_Basic1100");
    /// </code>
    public class ThemeHelper
    {
        private ThemeHelper()
        {
            Initialize();
        }
        #region public Property
        /// <summary>
        /// ThemeHelper instance (read-only) <br></br>
        /// Gets the current ThemeHelper object.
        /// </summary>
        public static ThemeHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ThemeHelper();
                }
                return instance;
            }
        }

        /// <summary>
        /// If you want to change theme, you can call it.(Default - DARK_THEME)
        /// </summary>
        public string CurrentTheme
        {
            get => currentTheme;
            set
            {
                if (currentTheme == value)
                {
                    ConfigLogger.DebugP("You set same themeName [%s1]", s1: value);
                }
                else
                {
                    currentTheme = value;
                    ConfigLogger.DebugP("currentTheme [%s1], highcontrast:[%s2],[%d1]", s1: value, s2: highcontrastTheme, d1: Convert.ToInt32(highcontrast));
                    UpdateTheme();
                }
            }
        }

        /// <summary>
        /// When the theme is changed, it is called
        /// </summary>
        public event EventHandler<string> ThemeChanged
        {
            add
            {
                themeChangedEventHandler += value;
            }
            remove
            {
                themeChangedEventHandler -= value;
            }
        }
        #endregion public Property

        #region public Method
        /// <summary>
        /// You map View, Property of View, ColorChip
        /// </summary>
        /// <param name="view">Object to apply colorChip</param>
        /// <param name="property">Property of object to apply colorchip</param>
        /// <param name="colorChip">Colorchip Name by UX</param>
        public void MapColorChip(View view, string property, string colorChip)
        {
            if (view == null || property == null || colorChip == null)
            {
                Log.Debug("UIFW.UIConfig", $"Map - View :{view}, property :{property}, ColorchipName :{colorChip}");
                return;
            }

            if (viewDic.ContainsKey(view.ID) == false)
            {
                viewDic.Add(view.ID, view);
            }

            if (objectDic.ContainsKey(view.ID, property) == false)
            {
                objectDic.Add(view.ID, property, colorChip);
            }
            else
            {
                objectDic[view.ID][property] = colorChip;
            }
            //ConfigLogger.DebugP("Map : [%s1][%d1] - proeprty :[%s2]", s1: view.GetTypeName(), d1: (int)view.ID, s2: property); ;
            ApplyColorChip(view, property, colorChip);
        }

        /// <summary>
        /// You unmap View, Property of View, ColorChip
        /// </summary>
        /// <param name="view">Object to apply colorChip</param>
        /// <param name="property">Property of object to apply colorchip</param>
        public void UnMapColorChip(View view, string property)
        {
            if (view == null || property == null)
            {
                Log.Debug("UIFW.UIConfig", $"UnMap - View :{view}, property :{property}");
                return;
            }

            if (objectDic.ContainsKey(view.ID, property) == true)
            {
                objectDic.Remove(view.ID, property);

                //ConfigLogger.DebugP("UnMap : [%s1][%d1] - proeprty :[%s2]", s1: view.GetTypeName(), d1: (int)view.ID, s2: property); ;

                if (objectDic[view.ID]?.Count == 0)
                {
                    ClearColorChip(view);
                }
            }
            else
            {
                //ConfigLogger.DebugP("UnMap failed [%d1] - proeprty :[%s1]", d1: (int)view.ID, s1: property);
            }
        }

        /// <summary>
        /// Clear All property of View, Property of View, ColorChip
        /// </summary>
        /// <param name="view">Object to apply colorChip</param>
        /// <version> 8.8.0 </version>
        public void ClearColorChip(View view)
        {
            if (view == null)
            {
                ConfigLogger.DebugP("ClearColorChip:View is null");
                return;
            }

            if (objectDic.ContainsKey(view.ID) == true)
            {
                objectDic[view.ID]?.Clear();
                objectDic[view.ID] = null;
                objectDic.Remove(view.ID);
            }

            if (viewDic.ContainsKey(view.ID) == true)
            {
                viewDic[view.ID] = null;
                viewDic.Remove(view.ID);
            }
        }

        /// <summary>
        /// You change ColorChip in property of View
        /// </summary>
        /// <param name="view">Object to apply colorChip</param>
        /// <param name="property">Property of object to apply colorchip</param>
        /// <param name="colorChip">Colorchip Name by UX</param>
        public void ChangeColorchip(View view, string property, string colorChip)
        {
            if (view == null || property == null || colorChip == null)
            {
                Log.Debug("UIFW.UIConfig", $"Change - View :{view}, property :{property}, ColorchipName :{colorChip}");
                return;
            }

            ConfigLogger.DebugP("Change : [%s1]-[%d1] - proeprty :[%s2], chip:[%s3]", s1: view.GetTypeName(), d1: (int)view.ID, s2: property, s3: colorChip);
            if (objectDic.ContainsKey(view.ID, property) == true)
            {
                objectDic[view.ID][property] = colorChip;
            }

            ApplyColorChip(view, property, colorChip);
        }
        #endregion public Method
        #region internal Method
        private void ApplyColorChip(View view, string property, string colorChip)
        {
            if (view == null || property == null || colorChip == null)
            {
                Log.Debug("UIFW.UIConfig", $"Apply - View :{view}, property :{property}, ColorchipName :{colorChip}");
                return;
            }

            Tizen.NUI.Color color = ColorChipTable.Instance.GetColor(colorChip);

            if (property == "BackgroundColor")
            {
                view.BackgroundColor = color;
                ConfigLogger.DebugP("SetValue direct :[%s1], chip:[%s2], color[%d1],[%d2],[%d3],[%d4]", s1: property, s2: colorChip,
                        d1: ConfigLogger.GetColor(color.R), d2: ConfigLogger.GetColor(color.G), d3: ConfigLogger.GetColor(color.B), d4: ConfigLogger.GetColor(color.A));

                return;
            }

            if (view is FluxView fluxView)
            {
                if (fluxView.SetColorByPropertyInternal(property, color) == true)
                {
                    ConfigLogger.DebugP("SetValue direct :[%s1], chip:[%s2], color[%d1],[%d2],[%d3],[%d4]", s1: property, s2: colorChip,
                        d1: ConfigLogger.GetColor(color.R), d2: ConfigLogger.GetColor(color.G), d3: ConfigLogger.GetColor(color.B), d4: ConfigLogger.GetColor(color.A));
                    return;
                }
            }

            Type mytype = view.GetType();
            PropertyInfo propertyInfo = mytype.GetProperty(property, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            if (propertyInfo != null)
            {
                propertyInfo.SetValue(view, color);
                if (color == null)
                {
                    ConfigLogger.DebugP("SetValue [%s1][%d1] - proeprty :[%s2], chip:[%s3], Color is null", s1: mytype.Name, d1: (int)view.ID, s2: property, s3: colorChip);
                }
                else
                {
                    ConfigLogger.DebugP("SetValue [%s1][%d1] - reflection :[%s2], chip:[%s3], color[%d2],[%d3],[%d4],[%d5]", s1: mytype.Name, d1: (int)view.ID, s2: property, s3: colorChip,
                        d2: ConfigLogger.GetColor(color.R), d3: ConfigLogger.GetColor(color.G), d4: ConfigLogger.GetColor(color.B), d5: ConfigLogger.GetColor(color.A));
                }
            }
            else
            {
                ConfigLogger.DebugP("Propery Info is null, [%s1][%d1] - reflection :[%s2], chip:[%s3], color[%d2],[%d3],[%d4],[%d5]", s1: mytype.Name, d1: (int)view.ID, s2: property, s3: colorChip,
                    d2: ConfigLogger.GetColor(color.R), d3: ConfigLogger.GetColor(color.G), d4: ConfigLogger.GetColor(color.B), d5: ConfigLogger.GetColor(color.A));
            }
        }
        #endregion internal Method

        #region private Method

        private void Initialize()
        {
            if (UIConfig.MainThreadID != Thread.CurrentThread.ManagedThreadId)
            {
                ConfigLogger.DebugP("UIConfig.MainThreadID :[%d1], Thread.CurrentThread.ManagedThreadId:[%d2]", d1: UIConfig.MainThreadID, d2: Thread.CurrentThread.ManagedThreadId);
                throw new Exception($"ColorTable is called in user thread (MainThreadID {UIConfig.MainThreadID}, ManagedThreadId {Thread.CurrentThread.ManagedThreadId})");
            }

            // get current theme
            themeLoader = new ThemeLoader();
            currentTheme = GetThemeName();
            ConfigLogger.DebugP("currentTheme [%s1]", s1: currentTheme);

            ResourceUtility.RootPath = GetRootPath();
            themeLoader.ThemeChanged += ThemeChangedCB;

            // TODO: Temporary remove VConf dependency
            //themeChangedCallback = ThemeChangedCB;
            //Vconf.NotifyKeyChanged("db/theme/theme-path", themeChangedCallback, IntPtr.Zero);

            //// assign root path for downloadable theme
            //string rootPath = Vconf.GetString("db/theme/theme-path");
            //if (rootPath == null)
            //{
            //    ConfigLogger.DebugP("Theme path from Vconf.GetString is null");
            //}
            //ResourceUtility.RootPath = rootPath;

            //highContrastChangedCallback = HighContrastChangedCB;
            //Vconf.NotifyKeyChanged("db/menu/system/accessibility/highcontrast", highContrastChangedCallback, IntPtr.Zero);
            //Vconf.GetInt("db/menu/system/accessibility/highcontrast", out int value);
            //highcontrast = value;
        }

        private void HighContrastChangedCB(IntPtr node, IntPtr userData)
        {
            // TODO: Temporary remove VConf dependency
            //Vconf.GetInt("db/menu/system/accessibility/highcontrast", out int value);
            //highcontrast = value;

#if Support_FLUXCore_Separated_UIThread
            if (FluxApplication.UIThreadSeparated == false)
            {
                UpdateTheme();
            }
            else
            {
                FluxSynchronizationContext.ToUIThread.Post(UIThreadHighContrastChanged, this);
            }
#else
            UpdateTheme();
#endif
        }

        private void UIThreadHighContrastChanged(object obj)
        {
            if (obj is ThemeHelper themeHelper)
            {
                UpdateTheme();
            }
        }

        private void ThemeChangedCB(object sender, ThemeEventArgs e)
        {
            // TODO: Temporary remove VConf dependency
            //string rootPath = Vconf.GetString("db/theme/theme-path");
            //ResourceUtility.RootPath = rootPath;            
            currentTheme = GetThemeName();
            ResourceUtility.RootPath = GetRootPath();
            ColorChipTable.Instance.UpdateColorChipTable(ResourceUtility.CommonPath + "COLOR_TABLE/principle_colortable.json");

#if Support_FLUXCore_Separated_UIThread
            if (FluxApplication.UIThreadSeparated == false)
            {
                themeChangedEventHandler?.Invoke(this, currentTheme);
                UpdateTheme();
            }
            else
            {
                FluxSynchronizationContext.ToUIThread.Post(UIThreadThemeChanged, this);
            }
#else
            themeChangedEventHandler?.Invoke(this, currentTheme);
            UpdateTheme();
#endif
        }

        private void UIThreadThemeChanged(object obj)
        {
            if (obj is ThemeHelper themeHelper)
            {
                themeChangedEventHandler?.Invoke(this, currentTheme);
                UpdateTheme();
            }
        }

        private string GetThemeName()
        {
            // TODO: Temporary remove VConf dependency
            //string retName = Vconf.GetString("db/theme/theme-type");

            //if (retName == "default")
            //{
            //    string temp = system_info.GetCustomString("com.samsung/featureconf/theme.default");

            //    if (temp == null)
            //    {
            //        ConfigLogger.DebugP("theme.default theme is null");
            //        retName = "DARK_THEME";
            //    }
            //    else
            //    {
            //        retName = temp;
            //    }
            //}

            //return retName;
            var themeName = themeLoader.CurrentTheme.Title;
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "Dark Theme";
            }
            return themeName;
        }

        private string GetRootPath()
        {
            var pkgInfo = ApplicationManager.GetInstalledApplication(themeLoader.CurrentTheme.Id);
            return pkgInfo.SharedResourcePath;
        }

        private void UpdateTheme()
        {
            if (objectDic.Count != 0)
            {
                foreach (KeyValuePair<uint, Dictionary<string, string>> pair in objectDic)
                {
                    if (pair.Value != null)
                    {
                        foreach (KeyValuePair<string, string> value in pair.Value)
                        {
                            if (value.Value != null)
                            {
                                if (viewDic.ContainsKey(pair.Key))
                                {
                                    View view = viewDic[pair.Key];
                                    ApplyColorChip(view, value.Key, value.Value);
                                }
                                else
                                {
                                    ConfigLogger.DebugP("can't find view");
                                }

                            }
                        }
                    }
                }
            }
        }

        #endregion private Method
        private EventHandler<string> themeChangedEventHandler;
        private ThemeLoader themeLoader;
        private string currentTheme = null;
        private readonly string highcontrastTheme = "HIGHCONTRAST_THEME";
        private int highcontrast = 0;
        private static ThemeHelper instance = null;
        private static readonly MultiKeyDictionary<uint, string, string> objectDic = new MultiKeyDictionary<uint, string, string>();
        private static readonly Dictionary<uint, View> viewDic = new Dictionary<uint, View>();
        // TODO: Temporary remove VConf dependency
        //private Vconf.VconfCallBack themeChangedCallback;
        //private Vconf.VconfCallBack highContrastChangedCallback;
    }
}
/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file PresetManager.cs 
/// <published> N </published> 
/// <privlevel> Non-privilege </privlevel> 
/// <privilege> None </privilege>  
/// <privacy> N </privacy> 
/// <product> TV </product> 
/// <version> 6.6.0 </version> 
/// <SDK_Support> Y </SDK_Support>

using System;
using System.Collections.Generic;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    ///  Management of all Preset
    /// </summary>
    public class PresetManager
    {
        /// <summary>
        ///  Register Custom Preset with class type
        /// </summary>
        /// <exception cref="InvalidOperationException">Throw when presetClassType is null or presetClassType is not a PresetBase </exception>
        /// <param name="name">Custom Type Name, Cannot register same name which already be registered</param>
        /// <param name="presetClassType">Class type of Custom Preset. Using "typeof()"</param>
        /// <code>
        /// public class C_TextItem_WhiteSingleline01_Custom001 : C_TextItem_WhiteSingleline01
        /// {
        ///     public C_TextItem_WhiteSingleline01_Custom001()
        ///     {
        ///         // Change Custom Property
        ///     }
        ///  }
        ///  
        /// // Register Custom Style with type of Class
        /// StyleSet.RegisterStyle("C_TextItem_WhiteSingleline01_Custom001", typeof( C_TextItem_WhiteSingleline01_Custom001 ) );
        /// 
        /// // Create component using registered custom type
        /// TextItem textItem = new TextItem("C_TextItem_WhiteSingleline01_Custom001");
        /// 
        /// </code>
        /// <returns>return false if name is duplicated</returns>
        /// <version> 6.0.0 </version>
        public static bool RegisterPreset(string name, Type presetClassType)
        {
            if (preset.ContainsKey(name))
            {
                FluxLogger.InfoP("[PresetManager.RegisterStyle] [%s1] already be used", s1: name);
                return false;
            }

            if (presetClassType == null)
            {
                throw new InvalidOperationException("[PresetManager.RegisterPreset]  presetClassTyle is null");
            }

            if ((presetClassType.IsSubclassOf(typeof(PresetBase)) || presetClassType == typeof(PresetBase)) == false)
            {
                throw new InvalidOperationException("[PresetManager.RegisterPreset]  presetClassTyle is not inherited by PresetBase");
            }

            preset.Add(name, presetClassType);

            return true;
        }

        internal static PresetBase GetPreset(string name)
        {

            PresetBase retPreset = null;

            if (preset.ContainsKey(name))
            {
                Type presetType = preset[name];

                if (presetType != null)
                {
                    retPreset = Activator.CreateInstance(presetType, true) as PresetBase;
                }
            }
            else
            {
                FluxLogger.ErrorP("preset doesn't have %s1 - key ", s1: name);
            }

            return retPreset;
        }


        #region private field


        private static readonly Dictionary<string, Type> preset = new Dictionary<string, Type>();


        #endregion

    }
}

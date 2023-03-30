using System;
using System.Collections.Generic;


namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    ///  Management of all Preset
    /// </summary>
    public class TemplateManager
    {
        /// <summary>
        /// </summary>
        public static bool RegisterPreset(string name, Type presetClassType)
        {
            if (preset.ContainsKey(name))
            {
                FluxLogger.InfoP("[TemplateManager.RegisterPreset] [%s1] already be used", s1: name);
                return false;
            }

            if (presetClassType == null)
            {
                throw new InvalidOperationException("[TemplateManager.RegisterPreset]  presetClassType is null");
            }

            if ((presetClassType.IsSubclassOf(typeof(TemplatePresetBase)) || presetClassType == typeof(TemplatePresetBase)) == false)
            {
                throw new InvalidOperationException("[TemplateManager.RegisterPreset]  presetClassType is not inherited by TemplateBase");
            }

            preset.Add(name, presetClassType);

            return true;
        }

        internal static TemplatePresetBase GetPreset(string name)
        {

            TemplatePresetBase retPreset = null;

            Type presetType = preset[name];

            if (presetType != null)
            {
                retPreset = Activator.CreateInstance(presetType) as TemplatePresetBase;
            }

            return retPreset;
        }


        #region private field


        private static readonly Dictionary<string, Type> preset = new Dictionary<string, Type>();


        #endregion

    }
}

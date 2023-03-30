using System.ComponentModel;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// type of using Scaler
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal enum ScalerTypes
    {
        /// <summary>
        /// Default (Main)
        /// </summary>
        Default = 0,
        /// <summary>
        /// Main scaler
        /// </summary>
        Main = Default,
        /// <summary>
        /// Sub scaler
        /// </summary>
        Sub = 1,
    }
}
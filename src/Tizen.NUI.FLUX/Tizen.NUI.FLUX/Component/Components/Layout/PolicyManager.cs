/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
using Tizen.NUI;

namespace Tizen.NUI.FLUX.Component
{
    internal class PolicyManager
    {
        #region public Property
        internal static PolicyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PolicyManager();
                }
                return instance;
            }
        }
        internal float Version
        {
            get;
            set;
        } = 1.0f;

        internal UnitSize UIAreaUnitSize
        {
             set
            {
                uiAreaUnitsize = value;
            }
            get
            {
                return uiAreaUnitsize;
            }
        }

        internal Vector2 Margin
        {
            set
            {
                margin = value;
            }
            get
            {
                if (margin == null)
                {
                    margin = new Vector2(0, 0);
                }
                return margin;
            }
        }
        // When angle changed, this value is set true. Default value is false. 
        // That value set false after rootlayout`s updatelayout. 
        internal bool IsChangingSize
        {
            get;
            set;
        } = false;

        internal int ColumnCount
        {
            set
            {
                columnCount = value;
            }
            get
            {
                Size2D windowSize = Window.Instance.Size;
                if (columnCount == 0 && windowSize != null)
                {
                    int GUTTERSIZE = 1;
                    int COLUMNSIZE = 4;
                    int MarginMinSize = 60; // Dependeny UX

                    int resizeWidth = ((COLUMNSIZE + GUTTERSIZE) * DisplayMetrics.Instance.PPU) * 2;
                    int drawingArea = (int)(windowSize.Width - ((MarginMinSize * 2) * DisplayMetrics.Instance.ScaleFactor) + GUTTERSIZE * DisplayMetrics.Instance.PPU);

                    columnCount = 2 * (drawingArea / resizeWidth);
                }

                return columnCount;
            }
        }

        internal void ApplyPolicy(Spec spec)
        {
            WrapNativePolicyManager.ApplyPolicy(spec.SpecPtr);
        }

        #endregion public Property
        #region private Method
        private PolicyManager()
        {

        }
        #endregion private Method


        #region private Method
        private static PolicyManager instance;
        private UnitSize uiAreaUnitsize = new UnitSize(0, 0);
        private Vector2 margin = new Vector2(0, 0);
        private int columnCount = 0;
        #endregion private Method
    }
}
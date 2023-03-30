/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
using System.Collections.Generic;
using Tizen.NUI;


namespace Tizen.NUI.FLUX.Component
{
    #region public Enum
    /// <summary>
    /// 
    /// </summary>
    public enum PrintColomnModes
    {
        /// <summary>
        /// 
        /// </summary>
        none,
        /// <summary>
        /// 
        /// </summary>
        print
    }
    #endregion public Enum
    #region public Delegate

    /// <summary>
    /// Delegate for Change Grid value
    /// </summary>
    public delegate void ChangeGrid(int culcnt, int paddingsize, int widthSize); //<<typle define
    #endregion public Delegate

    /// <summary>
    /// Gidsystem Class
    /// </summary>
    internal class Grid : ComponentBase
    {
        #region public Method
        /// <summary>
        /// 
        /// </summary>

        public Grid()
        {
            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public PrintColomnModes Mode
        {
            set
            {
                if (printMode == value)
                {
                    return;
                }
                printMode = value;
                if (printMode == PrintColomnModes.print)
                {
                    CreateGridView();
                }
            }
            get => printMode;
        }

        /// <summary>
        /// 
        /// </summary>

        public ChangeGrid ChangeDelegate
        {
            set => changeSize += value;
            get => changeSize;
        }


        /// <summary>
        /// 
        /// </summary>
        public override void UpdateLayout()
        {
            if (rightMargin != null && (rightMargin.SizeWidth < rightMargin.MinimumSize.Width))
            {
                rightMargin.SizeWidth = rightMargin.MinimumSize.Width;
            }
            if (leftMargin != null && (leftMargin.SizeWidth < leftMargin.MinimumSize.Width))
            {
                leftMargin.SizeWidth = leftMargin.MinimumSize.Width;
            }

            //(Screen Width – (40(pixel) X 2) +(1(unit) X 8(DisplayMetrics.Instance.PPU)) ) / (4 + 1 + 4 + 1)(unit) X 8(DisplayMetrics.Instance.PPU)
            // ->근은 Column 개수, (나머지 – 1(unit) * 8(DisplayMetrics.Instance.PPU))/ 2 + 40 = 좌우 마진 값

            resizeWidth = ((COLUMNSIZE + GUTTERSIZE) * DisplayMetrics.Instance.PPU) * 2;
            drawingArea = ((int)SizeWidth - (int)((MarginMinSize * 2) * DisplayMetrics.Instance.ScaleFactor) + GUTTERSIZE * DisplayMetrics.Instance.PPU);

            columnCount = 2 * (drawingArea / resizeWidth);

            marginSize = ((int)SizeWidth - ((COLUMNSIZE + GUTTERSIZE) * columnCount - GUTTERSIZE) * DisplayMetrics.Instance.PPU) / 2;
            //paddingSize = ( ( (drawingArea % resizeWidth) - (GUTTERSIZE * DisplayMetrics.Instance.PPU) ) / 2 ) + PADDINGMINSIZE;
            //paddingSize = (((drawingArea % resizeWidth)) / 2) + PADDINGMINSIZE;

            int widthSize = ((int)SizeWidth - (marginSize) * 2);

            //calcColumnLineCount(drawingArea, ref remainArea , ref columnCount);
            if (centerView != null)
            {
                centerView.SizeWidth = SizeWidth - (marginSize) * 2;
                centerView.SizeHeight = 0;
            }
            DrawColumn(columnCount);
            if (leftMargin != null)
            {
                leftMargin.SizeWidth = marginSize;
                leftMargin.SizeHeight = 0;
            }
            if (rightMargin != null)
            {
                rightMargin.SizeWidth = marginSize;
                rightMargin.SizeHeight = 0;
            }

            changeSize?.Invoke(columnCount, marginSize, widthSize);

            PolicyManager.Instance.ColumnCount = columnCount;
        }
        #endregion public Method

        #region protected Method
        /// <summary>
        /// Cleaning up managed and unmanaged resources
        /// </summary>
        /// <param name="type">
        /// Type of Dispose.
        /// Explicit - Called by user explicitly.
        /// Implicit - Called by gc implicitly.
        /// </param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.

                DestroyUtility.DestroyView(ref centerView);
                DestroyUtility.DestroyView(ref leftMargin);
                DestroyUtility.DestroyView(ref rightMargin);
            }

            base.Dispose(type);
        }
        #endregion protected Method

        #region private Method
        private void Initialize()
        {
        }

        private void CreateGridView()
        {
            if (centerView == null)
            {
                centerView = new FluxView
                {
                    PositionUsesPivotPoint = true,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter,
                    PivotPoint = Tizen.NUI.PivotPoint.TopCenter,
                    MinimumSize = new Size2D(MarginMinSize, 0)
                };
                Add(centerView);
            }

            if (leftMargin == null)
            {
                leftMargin = new FluxView
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    MinimumSize = new Size2D(MarginMinSize, 0)
                };
                centerView.Add(leftMargin);
            }

            if (rightMargin == null)
            {
                rightMargin = new FluxView
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    MinimumSize = new Size2D(MarginMinSize, 0)
                };
                centerView.Add(rightMargin);
            }
        }

        private void DrawColumn(int columnCnt)
        {
            if (printMode == PrintColomnModes.none)
            {
                return;
            }
            int px = 0;
            int py = 0;
            List<FluxView> columnList = new List<FluxView>();
            if (columnList.Count != 0)
            {
                for (int index = 0; index < columnList.Count; index++)
                {

                    centerView.Remove(columnList[index]);
                }
            }

            columnList.Clear();

            for (int index = 0; index < columnCnt; index++)
            {
                FluxView gridView = new FluxView
                {
                    BackgroundColor = new Color(0.8f, 0.1f, 0.4f, 0.3f),
                    SizeWidth = COLUMNSIZE * DisplayMetrics.Instance.PPU,
                    SizeHeight = 0,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
                columnList.Add(gridView);
            };

            for (int index = 0; index < columnCnt; index++)
            {
                columnList[index].PositionX = px;
                columnList[index].PositionY = py;
                if (Mode == PrintColomnModes.print)
                {
                    centerView.Add(columnList[index]);
                    leftMargin.BackgroundColor = rightMargin.BackgroundColor = new Color(0.0f, 0.4f, 0.0f, 0.5f);
                }
                else
                {
                    leftMargin.BackgroundColor = rightMargin.BackgroundColor = Color.Transparent;
                }
                px += (COLUMNSIZE + GUTTERSIZE) * DisplayMetrics.Instance.PPU;
            }
            columnList.Clear();
            columnList = null;
        }
        #endregion private Method
        #region private Field
        private PrintColomnModes printMode = PrintColomnModes.none;
        private int columnCount = 0;
        private int marginSize = 0;

        private FluxView centerView;
        private FluxView leftMargin;
        private FluxView rightMargin;
        private ChangeGrid changeSize;

        private readonly int GUTTERSIZE = 1;
        private readonly int COLUMNSIZE = 4;
        private int resizeWidth = 0;
        private int drawingArea = 0;
        #endregion private Field

        #region internal Field
        internal int MarginMinSize = 60; // Dependeny UX
        #endregion internal Field
    }
}
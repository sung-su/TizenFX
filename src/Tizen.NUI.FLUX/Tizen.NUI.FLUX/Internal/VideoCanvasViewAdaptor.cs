namespace Tizen.NUI.FLUX
{
    internal sealed class VideoCanvasViewAdaptor
    {
        public IVideoCanvasAdaptorBehavior AdaptorBehavior { get; set; }

        public VideoCanvasViewAdaptor(IVideoCanvasAdaptorBehavior adaptorBehavior)
        {
            AdaptorBehavior = adaptorBehavior;
        }

        public void CleanUp()
        {
            AdaptorBehavior.CleanUp();
        }

        public void UpdateGeometry(Tizen.NUI.Rectangle displayArea)
        {
            if (displayArea.Width > 0 && displayArea.Height > 0)
            {
                CLog.Info("DisplayArea: (%d1, %d2), (%d3, %d4)"
                    , d1: displayArea.X
                    , d2: displayArea.Y
                    , d3: displayArea.Width
                    , d4: displayArea.Height
                    );
                AdaptorBehavior.UpdateGeometry(displayArea);
            }
        }

        public void UpdateGeometry(VideoAttribute videoAttribute, WindowAttribute windowAttribute)
        {
            if (videoAttribute.Width > 0 && videoAttribute.Height > 0)
            {
                CLog.Info("VideoAttribute: %d1, %d2, %d3, %d4"
                , d1: videoAttribute.X
                , d2: videoAttribute.Y
                , d3: videoAttribute.Width
                , d4: videoAttribute.Height
                );
                AdaptorBehavior.UpdateGeometry(videoAttribute, windowAttribute);
                CLog.Info("WindowAttribute: %d1, %d2, %d3, %d4, %d5"
                , d1: windowAttribute.Position.X
                , d2: windowAttribute.Position.Y
                , d3: windowAttribute.Size.Width
                , d4: windowAttribute.Size.Height
                , d5: windowAttribute.Degree
                );
            }
        }

        public void UpdateAttribute()
        {
            AdaptorBehavior.UpdateAttribute();
        }

        public void SetAsyncUpdateMode(bool isEnabled)
        {
            AdaptorBehavior.SetAsyncUpdateMode(isEnabled);
        }
    }
}
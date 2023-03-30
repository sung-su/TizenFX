namespace Tizen.NUI.FLUX
{
    internal sealed class VideoWindowControlAdaptorBehavior : IVideoCanvasAdaptorBehavior
    {
        private IVideoWindowControl VideoWindowControl { get; set; }
        private IVideoWindowControlExtension VideoWindowControlExtension { get; set; }

        public VideoWindowControlAdaptorBehavior(IVideoWindowControl videoWindowControl)
        {
            VideoWindowControl = videoWindowControl;

            if (videoWindowControl is IVideoWindowControlExtension videoWindowControlExtension)
            {
                VideoWindowControlExtension = videoWindowControlExtension;
            }
        }

        public void UpdateGeometry(Rectangle displayArea)
        {
            VideoWindowControl?.SetDisplayArea(displayArea.X, displayArea.Y, displayArea.Width, displayArea.Height);
        }

        public void UpdateGeometry(VideoAttribute videoAttribute, WindowAttribute windowAttribute)
        {
            VideoWindowControlExtension?.SetDisplayArea(videoAttribute, windowAttribute);
        }

        public void UpdateAttribute()
        {
        }

        public void SetAsyncUpdateMode(bool isEnabled)
        {
        }

        public void CleanUp()
        {
        }
    }
}
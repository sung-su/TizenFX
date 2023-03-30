namespace Tizen.NUI.FLUX
{
    internal interface IVideoCanvasAdaptorBehavior
    {
        public void UpdateGeometry(Rectangle displayArea);
        public void UpdateGeometry(VideoAttribute videoAttribute, WindowAttribute windowAttribute);
        public void UpdateAttribute();
        public void SetAsyncUpdateMode(bool isEnabled);
        public void CleanUp();
    }
}

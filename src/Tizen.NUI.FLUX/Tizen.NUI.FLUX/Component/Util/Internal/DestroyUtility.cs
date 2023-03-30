using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX.Component
{
    internal class DestroyUtility
    {
        public static void DestroyView<T>(ref T view) where T : View
        {
            if (view != null)
            {
                view.Unparent();
                view.Dispose();
                view = null;
            }
        }

        public static void DestroyViewLinkedList<T>(ref LinkedList<T> list) where T : View
        {
            if (list != null)
            {
                foreach (View view in list)
                {
                    view.Unparent();
                    view.Dispose();
                }
                list.Clear();
                list = null;
            }
        }

        public static void DestroyViewList<T>(ref List<T> list) where T : View
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    list[i].Unparent();
                    list[i].Dispose();
                    list[i] = null;
                }
                list.Clear();
                list = null;
            }
        }

        public static void DestroyLayer<T>(ref T layer) where T : Layer
        {
            if (layer != null)
            {
                Window.Instance.RemoveLayer(layer);
                layer.Dispose();
                layer = null;
            }
        }


        public static void DestroyViewArray<T>(ref T[] view) where T : View
        {
            if (view != null)
            {
                for (int i = 0; i < view.Length; i++)
                {
                    DestroyView(ref view[i]);
                }
                view = null;
            }
        }

        public static void DestroyAnimation<T>(ref T ani) where T : Animation
        {
            if (ani != null)
            {
                ani.Stop();
                ani.Clear();
                ani.Dispose();
                ani = null;
            }
        }

        public static void DestroyAnimationArray<T>(ref T[] ani) where T : Animation
        {
            if (ani != null)
            {
                for (int i = 0; i < ani.Length; i++)
                {
                    DestroyAnimation(ref ani[i]);
                }
                ani = null;
            }
        }

        public static void DestroyFrameAnimation<T>(ref T ani) where T : FrameAnimation
        {
            if (ani != null)
            {
                ani.Stop();
                ani.Detach();
                ani = null;
            }
        }

        public static void DestroyFluxAnimationPlayer<T>(ref T ani) where T : FluxAnimationPlayer
        {
            if (ani != null)
            {
                ani.Stop();
                ani.Clear();
                ani.Dispose();
                ani = null;
            }
        }


        public static void DestroyTimer<T>(ref T timer) where T : Timer
        {
            if (timer != null)
            {
                //TODO: need to check how to remove Tick here
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }
    }
}

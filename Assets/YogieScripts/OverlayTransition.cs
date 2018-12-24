using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace Yogie
{
    public class OverlayTransition : MonoBehaviour
    {
        public Image overlay;

        System.Action fadeComplete;

        public static OverlayTransition core;

        void Awake()
        {
            core = this;

            DontDestroyOnLoad(gameObject);
        }

        public void FadeIn(System.Action action)
        {
            fadeComplete = action;

            overlay.color = new Color(0, 0, 0, 0);
            overlay.gameObject.SetActive(true);

            overlay.DOFade(1, 0.5f).OnComplete(FadeInComplete);
        }

        public void FadeOut(System.Action action)
        {
            fadeComplete = action;

            overlay.color = new Color(0, 0, 0, 1);

            overlay.DOFade(0, 0.5f).OnComplete(FadeOutComplete);
        }

        void FadeInComplete()
        {
            if (fadeComplete != null)
            {
                fadeComplete();

                fadeComplete = null;
            }
        }

        void FadeOutComplete()
        {
            overlay.gameObject.SetActive(false);

            if (fadeComplete != null)
            {
                fadeComplete();

                fadeComplete = null;
            }
        }
    }
}

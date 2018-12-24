using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Yogie
{
    public class SplashScreen : MonoBehaviour
    {
        public Image[] splashScreen;

        private int currentIndex;

        public string nextScene;

        void Start()
        {
            int ln = splashScreen.Length;

            for (int i = 0; i < ln; i++)
            {
                splashScreen[i].color = new Color(1, 1, 1, 0);
            }

            Invoke("Animate", 1.0f);
        }

        void Animate()
        {
            splashScreen[currentIndex].DOFade(1, 1.0f).OnComplete(FadeInComplete);
        }

        void FadeInComplete()
        {
            splashScreen[currentIndex].DOFade(0, 1.0f).SetDelay(1.5f).OnComplete(FadeOutComplete);
        }

        void FadeOutComplete()
        {
            if (currentIndex == splashScreen.Length - 1)
            {
                LoadNextScene();
            }
            else
            {
                currentIndex++;

                Invoke("Animate", 0.5f);
            }
        }

        void LoadNextScene()
        {
            OverlayTransition.core.FadeIn(ExecuteLoad);
        }

        void ExecuteLoad()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

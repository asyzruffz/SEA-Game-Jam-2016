using UnityEngine;
using System.Collections;

namespace Yogie
{
    public class TitleScreenController : MonoBehaviour
    {
        bool IsReady = false;

        void Start()
        {
            if (OverlayTransition.core != null)
                OverlayTransition.core.FadeOut(Ready);
            else
                Invoke("Ready", 2.0f);
        }

        void Ready()
        {
            IsReady = true;
        }

        void Update()
        {
            if (IsReady)
            {
                if (Input.anyKeyDown)
                {
                    IsReady = false;

                    LoadNextScene();
                }
            }
        }

        void LoadNextScene()
        {
            OverlayTransition.core.FadeIn(ExecuteLoad);
        }

        void ExecuteLoad()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
    }
}

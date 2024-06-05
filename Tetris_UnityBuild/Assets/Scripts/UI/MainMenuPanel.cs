using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace TetrisPuzzle.UI
{
    public class MainMenuPanel : MonoBehaviour
    {
        // Variables

        private CanvasGroup canvasGroup;


        // Methods

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public Coroutine FadeOut()
        {
            return StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeOutCoroutine()
        {
            canvasGroup.DOFade(0f, 1f);

            yield return new WaitForSeconds(1f);
        }
    }
}

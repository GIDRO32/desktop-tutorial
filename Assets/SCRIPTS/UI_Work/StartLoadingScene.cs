using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI_Work
{
    public class StartLoadingScene : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _imageFillAnim;

        private bool _startLoad = false;

        private void Start()
        {
            StartCoroutine((TimerCheckLoad()));
        }

        public void StartLoadSceneAnim(float Time)
        {
            _startLoad = true;

            _imageFillAnim.fillAmount = 0f;

            _imageFillAnim.DOFillAmount(1f, Time);
        }


        private IEnumerator TimerCheckLoad()
        {
            yield return new WaitForSeconds(1f);
            if (!_startLoad)
            {
                _imageFillAnim.DOFillAmount(1f, 1f)
                    .OnComplete(() => { gameObject.SetActive(false); });
            }
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
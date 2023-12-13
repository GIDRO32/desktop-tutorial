using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject LoadingPic;
    public Image Filling;

    void Start()
    {
        LoadingPic.SetActive(false);
    }
    public void SceneLoad(int Scene)
    {
        LoadingPic.SetActive(true);
        StartCoroutine(LoadSceneAsync(Scene));
    }
    IEnumerator LoadSceneAsync(int Scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Scene);

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Filling.fillAmount = progressValue;
            yield return null;
        }
    }
}

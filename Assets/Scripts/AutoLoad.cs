using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoLoad : MonoBehaviour
{

    public Slider loadingSlider;
    public GameObject loadingBar;
    public Text progText;
    AsyncOperation AO;

    void Start()
    {
        StartCoroutine(LoadingBar(1));
    }

    IEnumerator LoadingBar(int sceneIndex)
    {
        loadingBar.SetActive(true);
        AO = SceneManager.LoadSceneAsync(sceneIndex);
        AO.allowSceneActivation = false;

        while (AO.isDone == false)
        {
            loadingSlider.value = AO.progress;
            progText.text = Mathf.RoundToInt(AO.progress * 100).ToString() + " %";// * 100f + "%";

            if (AO.progress == 0.9f)
            {
                loadingSlider.value = 1f;
                AO.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

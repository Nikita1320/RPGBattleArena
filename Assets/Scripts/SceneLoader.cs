using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator animatorLoadPanel;
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private Image progressBarImage;
    [SerializeField] private Text progressText;
    private static SceneLoader instance;
    public static SceneLoader Instance => instance;
    private void Start()
    {
        instance = this;
        animatorLoadPanel.SetTrigger("Close");
    }
    public void LoadScene(int scene)
    {
        StartCoroutine(SceneLoadCoroutine(scene));
    }
    private IEnumerator SceneLoadCoroutine(int loadingScene)
    {
        AsyncOperation asyncOperationLoad = SceneManager.LoadSceneAsync(loadingScene);
        loadPanel.SetActive(true);
        animatorLoadPanel.SetTrigger("Loading");

        while (!asyncOperationLoad.isDone)
        {
            progressBarImage.fillAmount = asyncOperationLoad.progress * 100;
            progressText.text = $"{Mathf.RoundToInt(asyncOperationLoad.progress * 100)}%";
            Debug.Log(asyncOperationLoad.progress);
            yield return null;
        }
        Debug.Log("EndLoad");
    }
}

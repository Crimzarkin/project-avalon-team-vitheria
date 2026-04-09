using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections;
using System.ComponentModel;

public class MainMenuHandler : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand;
    private InputDevice controller;
    private GameObject menuCanvas = null;
    private Canvas loadingCanvas = null;
    public Text loadingText;
    public float loadingSpeed = 0.5f;
    private string baseText = "Loading";
    private int dotCount = 0;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
        menuCanvas = GameObject.Find("Menu");
        loadingCanvas = GameObject.Find("Loading").GetComponent<Canvas>();
        loadingCanvas.enabled = false;
    }
        public void StartGame(string sceneName)
{
    if (sceneName == "Exit")
    {
        Application.Quit();
        return;
    }

    menuCanvas.SetActive(false);
    loadingCanvas.enabled = true;

    StartCoroutine(LoadSceneRoutine(sceneName));
}

IEnumerator LoadSceneRoutine(string sceneName)
{
    StartCoroutine(AnimateLoading());
    yield return Resources.UnloadUnusedAssets();
    System.GC.Collect();

    yield return null;

    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
    while (!asyncLoad.isDone)
    {
        yield return null;
    }
}
}
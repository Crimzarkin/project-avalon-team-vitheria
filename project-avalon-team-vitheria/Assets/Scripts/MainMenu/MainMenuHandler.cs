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
        if(sceneName == "Exit")
        {
            Application.Quit();
            return;
        }
       
        
        menuCanvas.SetActive(false);
        loadingCanvas.enabled = true;
        StartCoroutine(AnimateLoading());
        SceneManager.LoadSceneAsync(sceneName);
    }
    IEnumerator AnimateLoading()
    {
        while (true)
        {
            loadingText.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4;
            yield return new WaitForSeconds(loadingSpeed);
        }
    }

}
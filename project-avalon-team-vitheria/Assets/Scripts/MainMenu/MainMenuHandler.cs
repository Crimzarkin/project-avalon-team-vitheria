using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.ComponentModel;

public class MainMenuHandler : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand;
    private InputDevice controller;
    private Canvas menuCanvas = null;
    private Canvas loadingCanvas = null;


    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
        menuCanvas = GameObject.Find("Menu").GetComponent<Canvas>();
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
        menuCanvas.enabled = false;
        loadingCanvas.enabled = true;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
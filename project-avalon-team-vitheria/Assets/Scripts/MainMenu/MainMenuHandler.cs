using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuHandler : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand;
    private InputDevice controller;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
    }
        
    public void StartGame(string sceneName)
    {
        if(sceneName == "Exit")
        {
            Application.Quit();
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
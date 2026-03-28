using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour


{
    // This is the function the button will trigger
    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
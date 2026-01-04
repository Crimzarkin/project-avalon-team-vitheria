using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    // Load Scene on button click via scene name
    public void LoadOnClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Load Scene on button click via scene index
    public void LoadOnClick(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Load default scene "LucisGarden"
    public void LoadOnClick()
    {
        SceneManager.LoadScene("LucisGarden");
    }
}

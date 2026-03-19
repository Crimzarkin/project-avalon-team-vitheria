using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitStargazing : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}

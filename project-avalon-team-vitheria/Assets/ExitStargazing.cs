using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitStargazing : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("MainMenu");
    }
}

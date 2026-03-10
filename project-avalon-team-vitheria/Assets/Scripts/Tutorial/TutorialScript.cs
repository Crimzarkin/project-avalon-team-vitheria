using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialPopUpHandler : MonoBehaviour
{
    public Transform player;
    public XRNode controllerNode = XRNode.RightHand;
    public Button toggleButton;
    public GameObject tutorialScreen;
    private float TuiDistance = 4.0f;
    private float TuiScale = 0.01f;
    private float TuiHeight = 1.5f;

    void Start()
    {
        tutorialScreen.SetActive(true);
    }
    void Update() {
        Vector3 newPos = player.position + player.forward * TuiDistance + Vector3.up * TuiHeight;
                tutorialScreen.transform.position = newPos;
                tutorialScreen.transform.localScale = new Vector3(TuiScale, TuiScale, TuiScale);
                tutorialScreen.transform.LookAt(player);
      
    }
    
}
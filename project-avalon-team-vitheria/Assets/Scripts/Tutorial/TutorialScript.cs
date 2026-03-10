using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialScript : MonoBehaviour
{
    public Transform player;
    public XRNode controllerNode = XRNode.RightHand;
    public Button toggleButton;
    public GameObject tutorialScreen;
    private float TuiDistance = 4.0f;
    private float TuiScale = 0.005f;
    private float TuiHeight = 1.0f;

    void Start()
    {
        tutorialScreen.SetActive(true);
    }
    void Update() {

        Vector3 newPos = player.position + (player.forward * TuiDistance) + (Vector3.up * TuiHeight) + (player.right * -1.25f);
      
                tutorialScreen.transform.position = newPos;
                tutorialScreen.transform.localScale = new Vector3(TuiScale, TuiScale, TuiScale);
                tutorialScreen.transform.rotation = player.rotation;
                tutorialScreen.transform.Rotate(0, 180, 0); 
      
    }
    
}
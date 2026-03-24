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
    public GameObject ButtonObject;
    private float TuiDistance = 3.0f; //Distance from the screen
    private float TuiScale = 0.0065f;
    private float TuiHeight = 0.0f; // 0 for center, -1 for top

    void Start()
    {

        tutorialScreen.SetActive(true);
        ButtonObject.SetActive(true);
        
    }
    void Update() {

        Vector3 newPos = player.position + (player.forward * TuiDistance) + (Vector3.up * TuiHeight) + (player.right * 0); // -0.75f TOP RIGHT,
      
                tutorialScreen.transform.position = newPos;
                tutorialScreen.transform.localScale = new Vector3(TuiScale * 2, TuiScale * 2, TuiScale);
                tutorialScreen.transform.rotation = player.rotation;
                //tutorialScreen.transform.Rotate(20, 160, 0); //Top right
                tutorialScreen.transform.Rotate(0, 180, 0); // Center
      
    }
    
}
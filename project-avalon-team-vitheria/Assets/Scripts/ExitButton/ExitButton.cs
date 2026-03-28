using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitButton : MonoBehaviour
{
    public Transform player;
    public XRNode controllerNode = XRNode.RightHand;
    public Button toggleButton;
    public GameObject ExitScreen;
    public GameObject Confirmation;
    public GameObject ButtonObject;
    private float TuiDistance = 3.0f; //Distance from the screen
    private float TuiScale = 0.0065f;
    private float TuiHeight = 2f; // 0 for center, -1 for top

    void Start()
    {

        ExitScreen.SetActive(true);
        
    }
    void Update() {

        Vector3 newPos = player.position + (player.forward * TuiDistance) + (Vector3.up * TuiHeight) + (player.right * 1f); // -0.75f TOP RIGHT,
      
                ExitScreen.transform.position = newPos;
                ExitScreen.transform.localScale = new Vector3(TuiScale * 2, TuiScale  * 2, TuiScale);
                ExitScreen.transform.rotation = player.rotation;
                ExitScreen.transform.Rotate(0, 180, 0); //Top right
                //tutorialScreen.transform.Rotate(0, 180, 0); // Center


        Vector3 ConfirmationnewPos = player.position + (player.forward * TuiDistance) + (Vector3.up * 0) + (player.right * 0);
                Confirmation.transform.position = ConfirmationnewPos;
                Confirmation.transform.localScale = new Vector3(TuiScale * 2, TuiScale * 2, TuiScale);
                Confirmation.transform.rotation = player.rotation;
                //Confirmation.transform.Rotate(0, 180, 0); //Top right
                Confirmation.transform.Rotate(0, 180, 0); // Center
      
    }
    
}
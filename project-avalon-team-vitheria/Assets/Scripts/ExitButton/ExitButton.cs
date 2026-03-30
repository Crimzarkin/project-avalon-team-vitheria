using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitButton : MonoBehaviour
{
    public Transform player;
    public XRNode controllerNode = XRNode.RightHand;

    public GameObject ExitScreen;
    public GameObject Confirmation;
    public GameObject ButtonObject;
    public GameObject tutorialGameObject; 

    private float TuiDistance = 3.0f;
    private float TuiScale = 0.0065f;
    private float TuiHeight = 2f;

    void Start()
    {
        ExitScreen.SetActive(true);
 
        Confirmation.SetActive(false); 
    }

    void Update() 
    {
        Vector3 newPos = player.position + (player.forward * TuiDistance) + (Vector3.up * TuiHeight) + (player.right * .50f); 
        ExitScreen.transform.position = newPos;
        ExitScreen.transform.localScale = new Vector3(TuiScale * 3, TuiScale  * 3, TuiScale);
        ExitScreen.transform.rotation = player.rotation;
        ExitScreen.transform.Rotate(20, 180, 0); 

        Vector3 ConfirmationnewPos = player.position + (player.forward * TuiDistance) + (Vector3.up * 0) + (player.right * 0);
        Confirmation.transform.position = ConfirmationnewPos;
        Confirmation.transform.localScale = new Vector3(TuiScale * 2, TuiScale * 2, TuiScale);
        Confirmation.transform.rotation = player.rotation;
        Confirmation.transform.Rotate(0, 180, 0); 
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLever : MonoBehaviour
{
    public GameObject Lever;
    float velocityZ = 0.2f;
    float posZ;
    bool forward = true;
    float upperBound = 0.2f;
    float lowerBound = -0.2f;
    // Start is called before the first frame update
    void Start()
    {
        upperBound += Lever.transform.position.z;
        lowerBound += Lever.transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        leverDisplacement();
    }
    void leverDisplacement()
    {
        if( (forward && Lever.transform.position.z >= upperBound) || (!forward && Lever.transform.position.z <= lowerBound))
        {
            forward = !forward;
            Debug.Log("Hit");
        }

        if (forward)
        {
            Lever.transform.Translate(Vector3.forward * Time.deltaTime * velocityZ);
        }
        else
        {
            Lever.transform.Translate(Vector3.forward * Time.deltaTime * -velocityZ);
        }
        
    }
}

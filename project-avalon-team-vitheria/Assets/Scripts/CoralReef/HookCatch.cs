using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCatch : MonoBehaviour
{
    private void onTriggerEnter(Collider other)
    {
        Debug.Log("Hooked");
        Destroy(other.gameObject);
    }
}

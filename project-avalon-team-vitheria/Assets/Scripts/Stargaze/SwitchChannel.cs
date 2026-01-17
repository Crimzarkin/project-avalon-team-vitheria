using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchChannel : MonoBehaviour
{
    public GameObject channel1;
    public GameObject channel2;

    private bool isChannel1Active = true;

    void Start()
    {
        ActivateChannel1();
    }

    void ToggleChannels()
    {
        if (isChannel1Active)
        {
            ActivateChannel2();
        }
        else
        {
            ActivateChannel1();
        }
    }

    void ActivateChannel1()
    {
        channel1.SetActive(true);
        channel2.SetActive(false);
        isChannel1Active = true;
    }

    void ActivateChannel2()
    {
        channel1.SetActive(false);
        channel2.SetActive(true);
        isChannel1Active = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEducationalScreen : MonoBehaviour
{
    private int channel = 0;
    public List<GameObject> channels = new List<GameObject>();

    public void Start()
    {
        foreach (GameObject channel in channels)
        {
            channel.SetActive(false);
        }
        channels[0].SetActive(true);
    }
    public void ToggleEduChannel()
    {
        TurnOffAllChannels();
        channel++;
        if (channel >= channels.Count)
        {
            channel = 0;
        }
        channels[channel].SetActive(true);
    }

    public void TogglePreviousChannel()
    {
        TurnOffAllChannels();
        channel--;
        if (channel < 0)
        {
            channel = channels.Count - 1;
        }
        channels[channel].SetActive(true);
    }
    private void TurnOffAllChannels()
    {
        foreach (GameObject channel in channels)
        {
            channel.SetActive(false);
        }
    }

}

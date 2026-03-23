using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchChannel : MonoBehaviour
{
    public List<Sprite> channels = new List<Sprite>();
    public GameObject map;
    private int channel = 1;

    public void ToggleChannels()
    {
        if (channel < channels.Count)
        {
            map.GetComponent<SpriteRenderer>().sprite = channels[channel];
            channel++;
        }
        else
        {
            map.GetComponent<SpriteRenderer>().sprite = channels[0];
            channel = 1;
        }
    }
    public void TogglePreviousChannels()
    {
        if (channel > 1)
        {
            channel--;
            map.GetComponent<SpriteRenderer>().sprite = channels[channel - 1];
        }
        else
        {
            channel = channels.Count;
            map.GetComponent<SpriteRenderer>().sprite = channels[channel - 1];
        }
    }
}

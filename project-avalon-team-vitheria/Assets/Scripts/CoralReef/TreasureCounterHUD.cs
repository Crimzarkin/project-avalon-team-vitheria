using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreasureCounterHUD : MonoBehaviour
{
    private int count;
    [SerializeField] private TextMeshProUGUI counterDisplay;
    public void decreaseCounter()
    {
        if (count > 0)
        {
            count -= 1;
            counterDisplay.text = "Remaining treasure: " + count.ToString();            
        }
        else
        {
            counterDisplay.text = "All treasure has been found! Return to main menu or reset the game.";
        }
    }

    public void setcounterText(int number)
    {
        count = number;
        counterDisplay.text = "Remaining treasure: " + number.ToString();
    }
}

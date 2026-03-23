using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureCounterHUD : MonoBehaviour
{
    private GameObject[] treasure;
    private int count;
    [SerializeField] private GameObject hudText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    public void decreaseCounter()
    {
        count -= 1;
        Text counterText = hudText.GetComponent<Text>();
        counterText.text = "Remaining treasure: " + count.ToString();
    }

    public void setcounterText(int number)
    {
        count = number;
        Text counterText = hudText.GetComponent<Text>();
        counterText.text = "Remaining treasure: " + number.ToString();
    }
}

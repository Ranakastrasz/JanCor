using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {
    
    public Text timeText; // df label that is updated as current time


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //DateTime currentTime = DateTime.Now;

        System.TimeSpan currentTime = System.DateTime.Now.TimeOfDay;

        System.DateTime d1 = System.DateTime.Today + currentTime;               // any date will do

        timeText.text = d1.ToString("hh:mm:ss tt"); // update the current time with df label








        

    }
}

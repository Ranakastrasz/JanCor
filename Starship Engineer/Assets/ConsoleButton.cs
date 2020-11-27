using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleButton : MonoBehaviour {


    public KeyCode key;
    public int digit;

    // Use this for initialization
    void Start ()
    {

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonPress);
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(key))
        {
            OnButtonPress();
            // If key is pressed, button is pressed.
        }
    }

    void OnButtonPress()
    {
        Console.Active.ButtonPress(this);
    }
}

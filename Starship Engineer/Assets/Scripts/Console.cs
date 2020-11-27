using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Console : MonoBehaviour
{
    // This is the Input module.
    // Holds the X and Y points
    // And uses horizontal/Vertical input.

    // Ok, screw the arrows keys. Use buttons on the screen and hotkeys if available.
    // Make a numberpad, tap for android, number keys if computer. Way faster that way. (and no easy adjust thinggy)

    public static Console Active;
    
    public ConsoleButton[] buttons;

    

    public Text valueText;
    public int value = 0;


    void Start()
    {
        Active = this;
        ResetValue();
        for (int x = 0; x < 10; x++)
        {
            buttons[x] = transform.Find(x.ToString()).GetComponent<ConsoleButton>();
            buttons[x].key = KeyCode.Keypad0 + x;
            buttons[x].digit = x;
        }
        buttons[10] = transform.Find("Del").GetComponent<ConsoleButton>();
        buttons[10].key = KeyCode.KeypadPeriod;
        buttons[10].digit = -1;
    }
    

    public void ResetValue()
    {
        value = 0;
        Draw();
    }

    void Backspace()
    {
        // remove last digit
        value /= 10;
        SoundManager.Active.PlaySound(SoundManager.Active.beep, 0.5f);
    }

    void AddDigit(int digit)
    {
        // Add digit to value;

        if (value < 100) // at 3 digits, can't add more digits.
        {
            value *= 10;
            value += digit;
            SoundManager.Active.PlaySound(SoundManager.Active.beep,0.75f + (digit*0.05f));
        }
        else
        {
            SoundManager.Active.PlaySound(SoundManager.Active.beep, 0.3f);
        }
    }

    public void ButtonPress(ConsoleButton button)
    {
        if (button.digit == -1)
        {
            // -1 is the backspace/Delete key
            Backspace();
        }
        else
        {
            // otherwise, digit is the digit value;
            AddDigit(button.digit);
        }
        Draw();
    }
    

    public int GetValue()
    {
        return value;

    }

    void Draw()
    {
        // Draw UI Represenation. Move cursor to correct position.
        valueText.text = "= "+ GetValue().ToString();
    }


}

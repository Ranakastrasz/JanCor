using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryPanel : MonoBehaviour
{

    /*
        An EntryPanel represents a single panel in the grid, linking to a BSRecord
        
        it can display/Redraw itself
        Be enabled for editing and saved,
        and Delete it's entry.
        
        It does NOT need an entry.

    */


    BSRecord _record;

    Text TimeText;
    Text DateText;
    InputField BSField;
    Button SaveButton;
    Button UndoButton;
    Button DeleteButton;



    private void Start()
    {
        TimeText = transform.Find("Time").GetComponent<Text>();
        DateText = transform.Find("Date").GetComponent<Text>();
        BSField = transform.Find("BSField").GetComponent<InputField>();
        SaveButton = transform.Find("Save").GetComponent<Button>();
        UndoButton = transform.Find("Undo").GetComponent<Button>();
        DeleteButton = transform.Find("Delete").GetComponent<Button>();

        //NewRecord();
        Redraw();
    }

    void NewRecord()
    {
        ShowToast.Active.MyShowToastMethod();
        System.TimeSpan currentTime = System.DateTime.Now.TimeOfDay;
        System.DateTime d1 = System.DateTime.Today + currentTime;

        _record = new BSRecord(0, d1.ToString("MM/dd/yyyy"), d1.ToString("hh:mm:ss tt"));

        ShowToast.Toast("New Record: " + _record.ToString());

        Redraw();
    }

    void SetRecord(BSRecord iRecord)
    {
        _record = iRecord;
        Redraw();
    }

    void Redraw()
    {
        if (_record != null)
        {
            TimeText.text = _record._time;
            DateText.text = _record._date;
            if (_record._BSLevel != 0)
            {
                BSField.text = _record._BSLevel.ToString();
            }
            BSField.interactable = true;
            SaveButton.interactable = true;
            UndoButton.interactable = true;
            DeleteButton.interactable = true;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }


}

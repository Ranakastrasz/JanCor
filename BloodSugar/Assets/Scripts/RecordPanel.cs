using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPanel : MonoBehaviour
{/*
        An EntryPanel represents a single panel in the grid, linking to a BSRecord
        
        it can display/Redraw itself
        Be enabled for editing and saved,
        and Delete it's entry.
        
        It does NOT need an entry.

    */


    BSRecord _record;

    Text _timeText;
    Text _dateText;
    Text _BSText;
    Button _editButton;



    private void Start()
    {
        _timeText = transform.Find("Time").GetComponent<Text>();
        _dateText = transform.Find("Date").GetComponent<Text>();
        _BSText = transform.Find("BSPanel/BSText").GetComponent<Text>();
        _editButton = transform.Find("Edit").GetComponent<Button>();

        _editButton.onClick.AddListener(OnEditButton);

        Redraw();
    }

    public void OnEditButton()
    {
        ShowToast.Toast("EditButton");
        RecordManager.Active.EditRecord(_record);
    }

    public void SetRecord(BSRecord iRecord)
    {
        _record = iRecord;
        Redraw();
    }

    void Redraw()
    {
        if (_record != null)
        {
            this.gameObject.SetActive(true);
            _timeText.text = _record._time;
            _dateText.text = _record._date;
            _BSText.text = _record._BSLevel.ToString();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }


}

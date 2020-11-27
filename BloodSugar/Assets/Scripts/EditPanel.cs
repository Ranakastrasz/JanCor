using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EditPanel : MonoBehaviour
{
    /*
        An EntryPanel represents a single panel in the grid, linking to a BSRecord
        
        it can display/Redraw itself
        Be selected for editing
        
        It does NOT need an entry. In such a case it hides itself until It recieves one.

    */
    
    
    BSRecord _record;

    Text _timeText;
    Text _dateText;
    InputField _BSField;
    Button _saveButton;
    Button _undoButton;
    Button _deleteButton;

    BSRecord _deletedRecord = null;

    bool _blankRecord;

    private void Start()
    {
        _timeText    = transform.Find("Time")      .GetComponent<Text>();
        _dateText    = transform.Find("Date")      .GetComponent<Text>();
        _BSField     = transform.Find("BSField").GetComponent<InputField>();
        _saveButton  = transform.Find("Save")      .GetComponent<Button>();
        _undoButton  = transform.Find("Undo")      .GetComponent<Button>();
        _deleteButton= transform.Find("Delete")    .GetComponent<Button>();

        _saveButton.onClick.AddListener(OnSaveButton);
        _undoButton.onClick.AddListener(OnUndoButton);
        _deleteButton.onClick.AddListener(OnDeleteButton);

        BlankRecord();

        Redraw();
    }

    void BlankRecord()
    {
        System.TimeSpan currentTime = System.DateTime.Now.TimeOfDay;
        System.DateTime d1 = System.DateTime.Today + currentTime;
        
        _record = new BSRecord(0, d1.ToString("MM/dd/yyyy"), d1.ToString("hh:mm:ss tt"));

        _blankRecord = true;

        Redraw();
    }

    public void EditRecord(BSRecord iRecord)
    {
        // Set the selected record for editing
        _record = iRecord;
        // You loaded a record, so the blank record is gone
        _blankRecord = false;
        // As is the deleted record.
        _deletedRecord = null;
        // Draw the selected record.
        Redraw();
    }

    void OnUndoButton()
    {
        // If you deleted a record
        if (_deletedRecord != null)
        {
            // Restore that record
            _record = _deletedRecord;
            // Put it back into the Manager.
            RecordManager.Active.NewRecord(_deletedRecord);
            // And it isn't exactly deleted anymore, is it?
            _deletedRecord = null;
        }
        // Nothing changes until you save, so redrawing reverts the values.
        Redraw();
    }

    void OnSaveButton()
    {
        _deletedRecord = null;// If you delete and then save the new one, you lose the old data.

        ShowToast.Toast("Record Saved");
        try
        {
            _record.SetBSLevel(int.Parse(_BSField.text));
            if (_blankRecord)
            {
                RecordManager.Active.NewRecord(_record);
            }
            else
            {
                RecordManager.Active.UpdateRecord(_record);
            }
            BlankRecord();
        }
        catch (Exception except)
        {
            if (except is FormatException)
            {
                ShowToast.Toast("Bloodsugar value Wrongly formatted. Enter an integer");
            }
            else
            {
                ShowToast.Toast(except.Message);
            }
        }

    }
    void OnDeleteButton()
    {
        if (!_blankRecord)
        {
            _deletedRecord = _record; // Save the record for undo-ing
            RecordManager.Active.DeleteRecord(_record); // Remove it from the RecordManager
        }
        BlankRecord(); // Make a new blank record.
        Redraw(); // Redraw
    }

    void Redraw()
    {
            _timeText.text = _record._time;
            _dateText.text = _record._date;
            if (_record._BSLevel != 0)
            {
                _BSField.text = _record._BSLevel.ToString();
            }
            else
            {
                _BSField.text = "";
            }
    }


}

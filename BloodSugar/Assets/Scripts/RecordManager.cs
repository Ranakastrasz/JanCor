using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordManager : MonoBehaviour {
    /*
     
        RecordManager
        Holds all records that exist currently, chooses how to display them, saves/Loads them, etc

        Has container with all saved records.
        Can save from and load to file (possibly by name?)
        Can Setup Panels to draw.

        Can recieve Records for saving - Redraw Panels
        Can recieve Records for Editing - Send to EditPanel
     */

    // needs a container.....
    public static RecordManager Active;

    List<BSRecord> _records = new List<BSRecord>();
    List<RecordPanel> _panels = new List<RecordPanel>();

    public GameObject _recordPanelPrefab;

    public GameObject _editPanel;
    public GameObject _panelGrid;

    public Button _nextPageButton;
    public Button _lastPageButton;

    private int _recordsPerPage = 10;
    private int _pageCount = 1;
    private int _currentPage = 0;

    private void Start()
    {
        Active = this;
        LoadFromFile();
        // Intstead of 10, use the correct number.
        for (int x = 0; x < _recordsPerPage; x++)
        {
            GameObject panel = Instantiate(_recordPanelPrefab);
            panel.transform.SetParent(_panelGrid.transform);
            _panels.Add(panel.GetComponent<RecordPanel>());
        }
        Invoke("Redraw", 0);
    }
    private void OnDestroy()
    {
        SaveToFile();
    }

    public void NewRecord(BSRecord iRecord)
    { // Insert record into List
        _records.Add(iRecord);
        Redraw();
    }

    public void EditRecord(BSRecord iRecord)
    {// Select record for modification
        _editPanel.GetComponent<EditPanel>().EditRecord(iRecord);
        Redraw();
    }

    public void UpdateRecord(BSRecord iRecord)
    {// Apply changes from record.
        Redraw();
    }

    public void DeleteRecord(BSRecord iRecord)
    {
        _records.Remove(iRecord);
        Redraw();
    }

    private void LoadFromFile()
    {
        List<string> iStrings = FileIO.ReadFile("records");

        foreach (string iString in iStrings)
        {
            BSRecord record = new BSRecord(iString);
            _records.Add(record);
        }

        //BSRecord record = new BSRecord(100, "3/8/2018", "12:25 PM");
        //_records.Add(record);

        // Read from file, For each line
        // Create a record as NewRecord for each one
        // Draw all
    }
    private void SaveToFile()
    {
        FileIO.CreateFile("records");
        List<string> oStrings = new List<string>();
        foreach (BSRecord record in _records)
        {
            oStrings.Add(record.ToString());
        }
        FileIO.WriteToFile("records", oStrings);
        // Foreach, toString and write to file
    }
    private void Redraw()
    {
        int pageOffset = _currentPage * 10; // Page 0, offset 0. Page 1, Offset 10, Page 2, offset 20.
        int x;
        for (x = 0; x < _recordsPerPage; x++)
        {
            if (_records.Count > (x+pageOffset))
            {
                _panels[x].SetRecord(_records[x + pageOffset]);
            }
            else
            {
                _panels[x].SetRecord(null);
            }
        }

        _pageCount = (int) Mathf.Max(1,(_records.Count+9)/10); // 0 - 10: 1 page. 11 - 20, 2 pages.

        _nextPageButton.interactable = (_currentPage < (_pageCount-1));
        _lastPageButton.interactable = (_currentPage >= 1         );

        if (_currentPage >= _pageCount)
        {
            // If you are on a ghost-page, go back one. Should be recursive enough for sanity.
            LastPage();
        }
    }

    public void NextPage()
    {
        _currentPage++;
        Redraw();
    }
    public void LastPage()
    {
        _currentPage--;
        Redraw();
    }


}



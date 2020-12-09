using System;
using UnityEngine;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
	private BSRecord _record;

	private InputField _BSField;

	public Button _saveButton;

	public Button _exitButton;

	private bool _blankRecord;

	private void Start()
	{
		_BSField = base.transform.Find("BSField").GetComponent<InputField>();
		_saveButton.onClick.AddListener(OnSaveButton);
		_exitButton.onClick.AddListener(OnExitButton);
		BlankRecord();
		Redraw();
	}

	private void BlankRecord()
	{
		TimeSpan timeOfDay = DateTime.Now.TimeOfDay;
		DateTime dateTime = DateTime.Today + timeOfDay;
		_record = new BSRecord(0, dateTime.ToString("MM/dd/yyyy"), dateTime.ToString("hh:mm:ss tt"));
		_blankRecord = true;
		Redraw();
	}

	private void OnSaveButton()
	{
		try
		{
			_record.SetBSLevel(int.Parse(_BSField.text));
			if (_blankRecord)
			{
				RecordManager.Active.NewRecord(_record);
				_blankRecord = false;
			}
			else
			{
				RecordManager.Active.UpdateRecord(_record);
			}
			_BSField.text = "";
			RecordManager.Active.Save();
			DisplayPanel.RedrawAll();
		}
		catch (Exception ex)
		{
			if (ex is FormatException)
			{
				ShowToast.Toast("Bloodsugar value Wrongly formatted. Enter an integer");
			}
			else
			{
				ShowToast.Toast(ex.Message);
			}
		}
	}

	private void OnExitButton()
	{
		UtilityFunctions.Quit();
	}

	private void Redraw()
	{
	}
}

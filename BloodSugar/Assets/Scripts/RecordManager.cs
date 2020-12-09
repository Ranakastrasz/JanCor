using System;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    private const string MethodName = "Redraw";
    public static RecordManager Active;

	private List<BSRecord> _records = new List<BSRecord>();

	private double _ratio;

	private void Start()
	{
		Active = this;
		LoadFromFile();
		Invoke(nameof(Redraw), 0f);
	}

	private void OnDestroy()
	{
	}

	public void Save()
	{
		SaveRecords();
		SaveRatio();

		System.TimeSpan currentTime = System.DateTime.Now.TimeOfDay;

		System.DateTime d1 = System.DateTime.Today + currentTime;
		FileIO.CreateDirectory("Backup");
		//string text = d1.ToString("YYYY_MM_dd_hh_mm_ss_tt"); // update the current time with df label
		string text = d1.ToString("yyyy-MM-dd"); // update the current time with df label
		string weekday = d1.ToString("ddd");
		//if (weekday == "fri")
		{
			if (FileIO.FileExists("Backup/" + text))
			{
				// Do Nothing
			}
			else
			{
				SaveRecords("Backup/" + text, false);
			}
		}
		//else
		{ 
			// Do Nothing
		}
	}

	public void NewRecord(BSRecord iRecord)
	{
		_records.Add(iRecord);
		Redraw();
	}

	public void UpdateRecord(BSRecord iRecord)
	{
		Redraw();
	}

	public void DeleteRecord(BSRecord iRecord)
	{
		_records.Remove(iRecord);
		Redraw();
	}

	private void LoadFromFile(string path = "records")
	{
		List<string> list = FileIO.ReadFile(path);
		foreach (string item2 in list)
		{
			BSRecord item = new BSRecord(item2);
			_records.Add(item);
		}

		List<string> list2 = FileIO.ReadFile("ratio");
		if (list2.Count > 0)
		{
			_ratio = double.Parse(list2[0]);
		}
		else
		{
			_ratio = 1.0;
		}
	}


	public void SaveRatio()
	{

		FileIO.CreateFile("ratio");
		List<string> list = new List<string>();
		list.Add(_ratio.ToString());
		FileIO.WriteToFile("ratio", list);
	}

	public void SaveRecords(string path = "records", bool overwrite = true)
	{
		if (!overwrite)
		{
			if (FileIO.FileExists(path))
				return;
		}
		else
		{ 
			// Continue and overwrite as normal.
		}

		FileIO.CreateFile(path);
		List<string> list = new List<string>();
		foreach (BSRecord record in _records)
		{
			list.Add(record.ToString());
		}
		FileIO.WriteToFile(path, list);



		/*
		 If Day.is.Saturday Then
			If Not FileNameExists(Today'sDateFormatted) Then
				DoNothing
			Else
				Save (Records.txt as Today'sDateFormatted)
			End
		Else
			DoNothing
		 
		 
		 
		 */
		/*
		 * Business requirement…   Make weekly backups so there is a fallback position when things go bad.
		 *
		 *  When a data point is recorded
		 *  Create the a filename based on date with this format.  YYYY-MM-DD.txt
		 *  Look in a backup directory under the place where the data is stored (creating it if needed)
		 *  If YYYY-MM-DD.txt does not exist,
		 *  Copy the current data file into the backup directory under that name.
		 *
		 *
         *      If the day is Saturday, create this file name pattern using the date “YYYY-MM-DD.txt”
		 *
         *      If that file exists in the records directory, then stop.  Otherwise…
		 *
         *      Copy records.txt to the file pattern above,  then stop.
		 *
 		 *
         *      There is no need to do maintenance on those files, just let them accumulate in the folder.
		 */
	}

	public double GetBSAverage(int iPeriodDays)
	{
		int num = 0;
		int num2 = 0;
		double result = 0.0;
		foreach (BSRecord record in _records)
		{
			if (DateTime.Parse(record._date).AddDays(iPeriodDays) >= DateTime.Now)
			{
				num++;
				num2 += record._BSLevel;
			}
		}
		if (num != 0)
		{
			result = num2 / num;
		}
		return result;
	}

	public double GetA1C(int iPeriodDays)
	{
		double bSAverage = GetBSAverage(iPeriodDays);
		bSAverage *= _ratio;
		return (46.7 + bSAverage) / 28.7;
	}

	private void Redraw()
	{
	}
}

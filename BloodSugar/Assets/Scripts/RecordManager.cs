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
		SaveToFile();
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

	private void LoadFromFile()
	{
		List<string> list = FileIO.ReadFile("records");
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

	public void SaveToFile()
	{
		FileIO.CreateFile("records");
		List<string> list = new List<string>();
		foreach (BSRecord record in _records)
		{
			list.Add(record.ToString());
		}
		FileIO.WriteToFile("records", list);
		FileIO.CreateFile("ratio");
		list = new List<string>();
		list.Add(_ratio.ToString());
		FileIO.WriteToFile("ratio", list);
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

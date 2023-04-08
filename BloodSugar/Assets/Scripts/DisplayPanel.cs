using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPanel : MonoBehaviour
{
	public static List<DisplayPanel> _panels = new List<DisplayPanel>();

	public string _periodName;

	public int _periodDays;

	public Text TextLabel;

	public Text TextBS;

	public Text TextA1C;

	public Text TextSTD;

	private void Start()
	{
		_panels.Add(this);
		TextLabel = base.transform.Find("Label").GetComponent<Text>();
		TextBS = base.transform.Find("BS").GetComponent<Text>();
		TextA1C = base.transform.Find("A1C").GetComponent<Text>();
		TextSTD = base.transform.Find("STD").GetComponent<Text>();
		Redraw();
	}

	public static void RedrawAll()
	{
		foreach (DisplayPanel panel in _panels)
		{
			panel.Redraw();
		}
	}

	private void Redraw()
	{
		TextLabel.text = _periodName;
		TextBS.text = Math.Round(RecordManager.Active.GetBSAverage(_periodDays)).ToString();
		TextA1C.text = RecordManager.Active.GetA1C(_periodDays).ToString("#.##");
		TextSTD.text = RecordManager.Active.GetStandardDeviation(_periodDays).ToString("#.##");
	}
}

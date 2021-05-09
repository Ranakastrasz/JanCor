using System;

public class BSRecord
{
	public static int _minBSLevel = 50;

	public static int _maxBSLevel = 400;

	public int _BSLevel
	{
		get;
		private set;
	}

	public string _date
	{
		get;
		private set;
	}

	public string _time
	{
		get;
		private set;
	}

	public BSRecord(string iString)
	{
		FromString(iString);
	}

	public BSRecord(int iBSLevel, string iDate, string iTime)
	{
		_BSLevel = iBSLevel;
		_date = iDate;
		_time = iTime;
	}

	~BSRecord()
	{
	}

	public void SetBSLevel(int iBSLevel)
	{
		if (iBSLevel >= _minBSLevel && iBSLevel <= _maxBSLevel)
		{
			_BSLevel = iBSLevel;
			return;
		}
		throw new Exception("Bloodsugar out of Range " + _minBSLevel + " <= " + iBSLevel + "<= " + _maxBSLevel);
	}

	public override string ToString()
	{
		return _date + "," + _time + "," + _BSLevel.ToString();
	}

	private void FromString(string iString)
	{
		string[] array = iString.Split(',');
		if (array.Length == 3)
		{
			_date = array[0];
			_time = array[1];
			_BSLevel = int.Parse(array[2]);
		}
	}
}

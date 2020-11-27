using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSRecord
{

    /*
        A BS Record represents a specific DateTime and blood sugar reading.
        It has no physical presence.

    */

    public int _BSLevel { get; private set; }

    public string _date { get; private set; }
    public string _time { get; private set; }


    public BSRecord(string iString)
    {
        // Tokenize and decode.
        // For loading

        FromString(iString);
        //_BSLevel = 0;
        //_date = "";
        //_time = "";
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
        if (iBSLevel >= 50 && iBSLevel <= 200)
        {
            _BSLevel = iBSLevel;
        }
        else
        {
            throw new System.Exception("Bloodsugar out of Range 50 <= " + iBSLevel + "<= 200");
        }
    }

    public override string ToString()
    {
        return _date+","+_time+","+_BSLevel.ToString();
    }
    void FromString(string iString)
    {
        string[] words = iString.Split(',');
        if (words.Length == 3)
        {
            _date = words[0];
            _time = words[1];
            _BSLevel = int.Parse(words[2]);
        }
        else
        {
            //throw new Exception("FromString:Wrong Argument count");
        }
    }


}

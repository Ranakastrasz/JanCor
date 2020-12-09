using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileIO : MonoBehaviour
{
	private static string basePath = Application.persistentDataPath + "/Records/";

	private static string fileType = ".txt";

	public static void CreateFile(string iFileName)
	{
		string path = basePath + iFileName + fileType;
		CreateDirectory();
		FileStream fileStream = File.Create(path);
		fileStream.Close();
	}
	public static void CreateDirectory(string Path = "")
	{
		Directory.CreateDirectory(basePath+Path);
	}

	public static void WriteToFile(string iFileName, List<string> iStrings)
	{
		string path = basePath + iFileName + fileType;
		using (StreamWriter streamWriter = new StreamWriter(path))
		{
			foreach (string iString in iStrings)
			{
				streamWriter.WriteLine(iString);
			}
		}
	}

	public static List<string> ReadFile(string iFileName)
	{
		string path = basePath + iFileName + fileType;
		List<string> list = new List<string>();
		if (File.Exists(path))
		{
			using (StreamReader streamReader = new StreamReader(path))
			{
				string item;
				while ((item = streamReader.ReadLine()) != null)
				{
					list.Add(item);
				}
			}
		}
		return list;
	}

	public static bool FileExists(string iFileName)
	{
		string path = basePath + iFileName + fileType;
		if (File.Exists(path))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

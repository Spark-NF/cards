using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager
{
	public static int mustLoad = -1;
	public static List<GameSave> savedGames = new List<GameSave>();

	public static void Init()
	{
		LoadAll();
	}

	public static void SaveNew()
	{
		savedGames.Add(Game.current.Save());
		SaveAll();
	}

	public static void Save(int i)
	{
		while (i >= savedGames.Count)
		{
			savedGames.Add(null);
		}
		savedGames[i] = Game.current.Save();
		SaveAll();
	}

	public static void Delete(int i)
	{
		if (i < savedGames.Count)
		{
			savedGames[i] = null;
		}
		SaveAll();
	}

	public static bool HasSave(int i)
	{
		return (i < savedGames.Count && savedGames[i] != null);
	}

	public static void SaveAll()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
		bf.Serialize(file, savedGames);
		file.Close();
	}

	public static bool Load(int i)
	{
		LoadAll();
		if (i < savedGames.Count)
		{
			Game.current.Load(savedGames[i]);
			return true;
		}
		return false;
	}

	public static void LoadAll()
	{
		if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			savedGames = (List<GameSave>)bf.Deserialize(file);
			file.Close();
		}
	}
}


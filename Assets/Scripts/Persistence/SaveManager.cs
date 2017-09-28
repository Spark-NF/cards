using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager
{
	public static int MustLoad = -1;
	public static List<GameSave> SavedGames = new List<GameSave>();

	public static void Init()
	{
		LoadAll();
	}

	public static void SaveNew()
	{
		SavedGames.Add(Game.Current.Save());
		SaveAll();
	}

	public static void Save(int i)
	{
		while (i >= SavedGames.Count)
		{
			SavedGames.Add(null);
		}
		SavedGames[i] = Game.Current.Save();
		SaveAll();
	}

	public static void Delete(int i)
	{
		if (i < SavedGames.Count)
		{
			SavedGames[i] = null;
		}
		SaveAll();
	}

	public static bool HasSave(int i)
	{
		return (i < SavedGames.Count && SavedGames[i] != null);
	}

	public static void SaveAll()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/SavedGames.gd");
		bf.Serialize(file, SavedGames);
		file.Close();
	}

	public static bool Load(int i)
	{
		LoadAll();
		if (i < SavedGames.Count)
		{
			Game.Current.Load(SavedGames[i]);
			return true;
		}
		return false;
	}

	public static void LoadAll()
	{
		if (File.Exists(Application.persistentDataPath + "/SavedGames.gd"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/SavedGames.gd", FileMode.Open);
			SavedGames = (List<GameSave>)bf.Deserialize(file);
			file.Close();
		}
	}
}

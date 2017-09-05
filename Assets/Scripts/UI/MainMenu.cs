using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject mainPanel;
	public GameObject loadPanel;
	public GameObject optionsPanel;
	public GameObject firstSelectedMain;
	public Slider optionsSfx;
	public Slider optionsMusic;
	private bool optionsChanged = false;

	#region Main

	public void Start()
	{
		EventSystem.current.SetSelectedGameObject(firstSelectedMain);
	}

	public void NewGame()
	{
		SceneManager.LoadScene("World");
	}

	public void Load()
	{
		mainPanel.SetActive(false);
		loadPanel.SetActive(true);
	}

	public void Options()
	{
		OptionsLoad();

		mainPanel.SetActive(false);
		optionsPanel.SetActive(true);
	}

	public void Exit()
	{
		Application.Quit();
	}

	#endregion

	#region Options

	public void OptionsLoad()
	{
		optionsSfx.value = PlayerPrefs.GetInt("Sfx", 8);
		optionsMusic.value = PlayerPrefs.GetInt("Music", 8);

		optionsChanged = false;
	}

	public void OptionsSave()
	{
		PlayerPrefs.SetInt("Sfx", (int)optionsSfx.value);
		PlayerPrefs.SetInt("Music", (int)optionsMusic.value);

		optionsChanged = false;
	}

	public void OptionsBack()
	{
		if (optionsChanged)
		{
			OptionsSave();
		}

		optionsPanel.SetActive(false);
		mainPanel.SetActive(true);
	}

	public void OptionsSfx()
	{
		Debug.Log("Update SFX volume");
		optionsChanged = true;
	}

	public void OptionsMusic()
	{
		Debug.Log("Update music volume");
		optionsChanged = true;
	}

	#endregion

	#region Load

	public void LoadBack()
	{
		loadPanel.SetActive(false);
		mainPanel.SetActive(true);
	}

	#endregion
}

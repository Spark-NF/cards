using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;
	public GameObject pausePanel;
	public GameObject savePanel;
	public GameObject loadPanel;
	public GameObject optionsPanel;
	public GameObject firstSelectedPause;
	public GameObject firstSelectedOptions;
	public Slider optionsSfx;
	public Slider optionsMusic;
	public bool stopTime = true;
	private bool optionsChanged = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Toggle();
		}
	}

	public void Toggle()
	{
		if (pauseMenu.activeSelf)
		{
			Hide();
		}
		else
		{
			Show();
		}
	}

	public void Show()
	{
		// Activate the correct panel
		pauseMenu.SetActive(true);
		pausePanel.SetActive(true);
		savePanel.SetActive(false);
		loadPanel.SetActive(false);
		optionsPanel.SetActive(false);

		// Select first menu element
		EventSystem es = EventSystem.current;
		es.SetSelectedGameObject(firstSelectedPause);

		// Stop time
		if (stopTime)
		{
			Time.timeScale = 0f;
		}
	}

	public void Hide()
	{
		// Activate the correct panel
		pauseMenu.SetActive(false);
		pausePanel.SetActive(true);
		savePanel.SetActive(false);
		loadPanel.SetActive(false);
		optionsPanel.SetActive(false);

		// Resume time
		if (stopTime)
		{
			Time.timeScale = 1f;
		}
	}

	public void Save()
	{
		pausePanel.SetActive(false);
		savePanel.SetActive(true);
	}

	public void Load()
	{
		pausePanel.SetActive(false);
		loadPanel.SetActive(true);
	}

	public void Options()
	{
		OptionsLoad();

		pausePanel.SetActive(false);
		optionsPanel.SetActive(true);
	}

	public void Title()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void Back()
	{
		Hide();
	}

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
		pausePanel.SetActive(true);
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

	public void SaveBack()
	{
		savePanel.SetActive(false);
		pausePanel.SetActive(true);
	}

	public void LoadBack()
	{
		loadPanel.SetActive(false);
		pausePanel.SetActive(true);
	}
}

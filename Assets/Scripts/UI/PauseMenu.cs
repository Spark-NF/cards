using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject MainCanvas;
	public GameObject PausePanel;
	public GameObject SavePanel;
	public GameObject LoadPanel;
	public GameObject OptionsPanel;
	public GameObject FirstSelectedPause;
	public GameObject FirstSelectedOptions;
	public Slider OptionsSfx;
	public Slider OptionsMusic;
	public bool StopTime = true;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Toggle();
		}
	}

	public void Toggle()
	{
		if (MainCanvas.activeSelf)
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
		MainCanvas.SetActive(true);
		PausePanel.SetActive(true);
		SavePanel.SetActive(false);
		LoadPanel.SetActive(false);
		OptionsPanel.SetActive(false);

		// Select first menu element
		EventSystem es = EventSystem.current;
		es.SetSelectedGameObject(FirstSelectedPause);

		// Stop time
		if (StopTime)
		{
			Time.timeScale = 0f;
		}
	}

	public void Hide()
	{
		// Activate the correct panel
		MainCanvas.SetActive(false);
		PausePanel.SetActive(true);
		SavePanel.SetActive(false);
		LoadPanel.SetActive(false);
		OptionsPanel.SetActive(false);

		// Resume time
		if (StopTime)
		{
			Time.timeScale = 1f;
		}
	}

	public void Save()
	{
		PausePanel.SetActive(false);
		SavePanel.SetActive(true);
	}

	public void Load()
	{
		PausePanel.SetActive(false);
		LoadPanel.SetActive(true);
	}

	public void Options()
	{
		OptionsLoad();

		PausePanel.SetActive(false);
		OptionsPanel.SetActive(true);
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
		OptionsSfx.value = PlayerPrefs.GetInt("Sfx", 8);
		OptionsMusic.value = PlayerPrefs.GetInt("Music", 8);
	}

	public void OptionsSave()
	{
		PlayerPrefs.SetInt("Sfx", (int)OptionsSfx.value);
		PlayerPrefs.SetInt("Music", (int)OptionsMusic.value);
	}

	public void OptionsBack()
	{
		OptionsSave();

		OptionsPanel.SetActive(false);
		PausePanel.SetActive(true);
	}

	public void SaveBack()
	{
		SavePanel.SetActive(false);
		PausePanel.SetActive(true);
	}

	public void LoadBack()
	{
		LoadPanel.SetActive(false);
		PausePanel.SetActive(true);
	}
}

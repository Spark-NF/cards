using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject MainPanel;
	public GameObject LoadPanel;
	public GameObject OptionsPanel;
	public GameObject FirstSelectedMain;
	public Slider OptionsSfx;
	public Slider OptionsMusic;

	#region Main

	public void Start()
	{
		EventSystem.current.SetSelectedGameObject(FirstSelectedMain);
	}

	public void NewGame()
	{
		SceneManager.LoadScene("World");
	}

	public void Load()
	{
		MainPanel.SetActive(false);
		LoadPanel.SetActive(true);
	}

	public void Options()
	{
		OptionsLoad();

		MainPanel.SetActive(false);
		OptionsPanel.SetActive(true);
	}

	public void Exit()
	{
		Application.Quit();
	}

	#endregion

	#region Options

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
		MainPanel.SetActive(true);
	}

	#endregion

	#region Load

	public void LoadBack()
	{
		LoadPanel.SetActive(false);
		MainPanel.SetActive(true);
	}

	#endregion
}

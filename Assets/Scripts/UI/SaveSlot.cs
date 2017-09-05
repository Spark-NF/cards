using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
	public int slot;
	public GameObject saveButton = null;
	public GameObject loadButton = null;
	public GameObject deleteButton = null;

	void OnEnable()
	{
		UpdateStatus();
	}

	void Start()
	{
		if (saveButton != null)
		{
			saveButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				SaveManager.Save(slot);
				UpdateStatus();
			});
		}

		if (loadButton != null)
		{
			loadButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				if (SceneManager.GetActiveScene().name != "World")
				{
					SaveManager.mustLoad = slot;
					SceneManager.LoadScene("World");
				}
				else
				{
					SaveManager.Load(slot);
					UpdateStatus();
				}
			});
		}

		if (deleteButton != null)
		{
			deleteButton.GetComponent<Button>().onClick.AddListener(() =>
			{
				SaveManager.Delete(slot);
				UpdateStatus();
			});
		}
	}

	public void UpdateStatus()
	{
		bool enabled = SaveManager.HasSave(slot);

		if (loadButton != null)
		{
			loadButton.GetComponent<Button>().interactable = enabled;
		}
		if (deleteButton)
		{
			deleteButton.GetComponent<Button>().interactable = enabled;
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
	public int slot;
	public Button saveButton = null;
	public Button loadButton = null;
	public Button deleteButton = null;

	void OnEnable()
	{
		UpdateStatus();
	}

	void Start()
	{
		if (saveButton != null)
		{
			saveButton.onClick.AddListener(() =>
			{
				SaveManager.Save(slot);
				UpdateStatus();
			});
		}

		if (loadButton != null)
		{
			loadButton.onClick.AddListener(() =>
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
			deleteButton.onClick.AddListener(() =>
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
			loadButton.interactable = enabled;

		if (deleteButton != null)
			loadButton.interactable = enabled;
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
	public int Slot;
	public Button SaveButton = null;
	public Button LoadButton = null;
	public Button DeleteButton = null;

	void OnEnable()
	{
		UpdateStatus();
	}

	void Start()
	{
		if (SaveButton != null)
		{
			SaveButton.onClick.AddListener(() =>
			{
				SaveManager.Save(Slot);
				UpdateStatus();
			});
		}

		if (LoadButton != null)
		{
			LoadButton.onClick.AddListener(() =>
			{
				if (SceneManager.GetActiveScene().name != "World")
				{
					SaveManager.MustLoad = Slot;
					SceneManager.LoadScene("World");
				}
				else
				{
					SaveManager.Load(Slot);
					UpdateStatus();
				}
			});
		}

		if (DeleteButton != null)
		{
			DeleteButton.onClick.AddListener(() =>
			{
				SaveManager.Delete(Slot);
				UpdateStatus();
			});
		}
	}

	public void UpdateStatus()
	{
		bool enabled = SaveManager.HasSave(Slot);

		if (LoadButton != null)
			LoadButton.interactable = enabled;

		if (DeleteButton != null)
			LoadButton.interactable = enabled;
	}
}

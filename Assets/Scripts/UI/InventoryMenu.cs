using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryMenu : MonoBehaviour
{
	public GameObject MainCanvas;
	public GameObject BagPanel;
	public GameObject CardsPanel;
	public GameObject ExitInventoryButton;
	public bool StopTime = true;

	void Start()
	{
		ShowBag();
	}

	public void Show()
	{
		// Activate the panel
		MainCanvas.SetActive(true);

		// Select first menu element
		EventSystem es = EventSystem.current;
		es.SetSelectedGameObject(ExitInventoryButton);

		// Stop time
		if (StopTime)
		{
			Time.timeScale = 0f;
		}
	}

	public void ShowBag()
	{
		// Switch the panels
		BagPanel.SetActive(true);
		CardsPanel.SetActive(false);
	}

	public void ShowCards()
	{
		// Switch the panels
		BagPanel.SetActive(false);
		CardsPanel.SetActive(true);
	}

	public void Hide()
	{
		// Deactivate the panel
		MainCanvas.SetActive(false);

		// Resume time
		if (StopTime)
		{
			Time.timeScale = 1f;
		}
	}
	
	void Update()
	{
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Notifier : MonoBehaviour
{
	public GameObject canvas;
	public Text textBox;
	private Animator anim;

	void Start()
	{
		canvas.SetActive(false);
		anim = canvas.GetComponent<Animator>();
	}

	public IEnumerator Notify(string txt, float duration = 3f)
	{
		if (txt == null)
		{
			Clear();
			yield break;
		}

		textBox.text = txt.Replace("\\n", "\n");
		canvas.SetActive(true);
		anim.Play("Slide In");

		yield return new WaitForSeconds(duration);

		anim.Play("Slide Out");
	}

	public void Clear()
	{
		canvas.SetActive(false);
	}

	public void Close()
	{
		anim.Play("Slide Out");
	}
}

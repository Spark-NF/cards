using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Notifier : MonoBehaviour
{
	public GameObject Canvas;
	public Text TextBox;

	private Animator _animator;

	private void Start()
	{
		Canvas.SetActive(false);
		_animator = Canvas.GetComponent<Animator>();
	}

	public IEnumerator Notify(string txt, float duration = 3f)
	{
		if (txt == null)
		{
			Clear();
			yield break;
		}

		TextBox.text = txt.Replace("\\n", "\n");
        Canvas.SetActive(true);
		_animator.Play("Slide In");

		yield return new WaitForSeconds(duration);

		_animator.Play("Slide Out");
	}

	public void Clear()
	{
		Canvas.SetActive(false);
	}

	public void Close()
	{
		_animator.Play("Slide Out");
	}
}

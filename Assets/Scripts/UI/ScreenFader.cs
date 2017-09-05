using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
	private Animator Animator;

	private bool _isFading = false;

	void Start()
	{
		Animator = GetComponent<Animator>();
	}

	public IEnumerator FadeToClear()
	{
		_isFading = true;
		Animator.SetTrigger("Fade In");

		while (_isFading)
		{
			yield return null;
		}
	}

	public IEnumerator FadeToBlack()
	{
		_isFading = true;
		Animator.SetTrigger("Fade Out");

		while (_isFading)
		{
			yield return null;
		}
	}

	public void AnimationComplete()
	{
		_isFading = false;
	}
}

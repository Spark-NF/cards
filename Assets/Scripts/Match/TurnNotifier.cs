using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnNotifier : MonoBehaviour
{
	public GameObject AnimationObject;
	public Text Text;

	private Animator _animator;

	private void Awake()
	{
		_animator = AnimationObject.GetComponent<Animator>();
		AnimationObject.SetActive(false);
	}

	public IEnumerator Notify(string message, float duration = 1f)
	{
		Text.text = message;
		AnimationObject.SetActive(true);

		_animator.Play("Slide In");
		yield return new WaitForSeconds(duration + GetAnimatorDuration(_animator));
		_animator.Play("Slide Out");
		yield return new WaitForSeconds(GetAnimatorDuration(_animator));

		AnimationObject.SetActive(false);
	}

	private float GetAnimatorDuration(Animator animator)
	{
		var info = animator.GetCurrentAnimatorStateInfo(0);
		return info.length / _animator.speed;
	}
}

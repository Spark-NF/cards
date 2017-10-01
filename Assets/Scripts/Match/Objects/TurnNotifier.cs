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
		_animator.speed = 0.5f;

		AnimationObject.SetActive(false);
	}

	public IEnumerator Notify(string message)
	{
		Text.text = message;
		AnimationObject.SetActive(true);

		_animator.Play("New Turn");
		var info = _animator.GetCurrentAnimatorStateInfo(0);
		yield return new WaitForSeconds(info.normalizedTime + info.length / _animator.speed);

		AnimationObject.SetActive(false);
	}
}

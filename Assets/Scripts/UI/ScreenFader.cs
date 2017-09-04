using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    private Animator animator;
    private bool isFading = false;

	void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator FadeToClear()
    {
        isFading = true;
        animator.SetTrigger("Fade In");

        while (isFading)
        {
            yield return null;
        }
    }

    public IEnumerator FadeToBlack()
    {
        isFading = true;
        animator.SetTrigger("Fade Out");

        while (isFading)
        {
            yield return null;
        }
    }

    public void AnimationComplete()
    {
        isFading = false;
	}
}

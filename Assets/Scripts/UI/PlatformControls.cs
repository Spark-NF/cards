using UnityEngine;
using UnityEngine.UI;

public class PlatformControls : MonoBehaviour
{
	public GameObject touchScreen;

	void Start()
	{
#if UNITY_IOS || UNITY_ANDROID || UNITY_TIZEN || UNITY_WP8 || UNITY_WP8_1
		touchScreen.SetActive(true);
#endif
	}
}

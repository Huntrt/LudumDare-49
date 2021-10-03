using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i;
	public AudioSource source;

	void Awake()
	{
		i = this;
	}
}

using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i;
	public AudioSource source;
	public float volume;

	void Awake()
	{
		//Create singelton and don't destroy on load when neede
		if(SoundManager.i == null) {i = this;DontDestroyOnLoad(this);}
		//If the game mananger are not in dontdestroyonload than destroy it
		if(gameObject.scene.name != "DontDestroyOnLoad") {Destroy(gameObject);}
	}

	void Update()
	{
		//Update volum
		source.volume = volume;
	}
}

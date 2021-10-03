using UnityEngine.UI;
using UnityEngine;

public class Volumeslider : MonoBehaviour
{
	[SerializeField] Slider slider;

    void Start()
    {
		//Update the current volume
        slider.value = SoundManager.i.volume;
    }

    void Update()
    {
		//Update the manager volume
        SoundManager.i.volume = slider.value;
    }
}

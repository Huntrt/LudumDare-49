using UnityEngine;

public class Pillar : MonoBehaviour
{
	bool passed;
	[SerializeField] bool creating;

	void Update()
	{
		//When the pillat has go out of despawn limit
		if(Vector2.Distance(Player.i.transform.position, transform.position) > Gameplay.i.despawnLimit)
		{
			//Destroy the object
			Destroy(gameObject);
		}
		//If this is the first time player has pass this pillar
		if(transform.position.x < Player.i.transform.position.x && !passed)
		{
			//Start the next pillar if this pillar need to create
			if(creating) Gameplay.i.NextPillar();
			//Has been pass
			passed = true;
		}
	}
}

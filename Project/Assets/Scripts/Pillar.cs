using UnityEngine;

public class Pillar : MonoBehaviour
{
	bool passed;
	[SerializeField] bool creating;
	[SerializeField] Transform powerPos;
	Gameplay gameplay; Player player;
	[SerializeField] Rigidbody2D rb;

	void Start()
	{
		//Get the gameplay and player
		gameplay = Gameplay.i; player = Player.i;
		//If able to spawn power by random chance
		if(Random.Range(0,101) < gameplay.chance)
		{
			//Randomly chose between an list of power
			int chose = Random.Range(0,gameplay.power.Length);
			//Create the chosed power and position with no rotation
			Instantiate(gameplay.power[chose], powerPos.position, Quaternion.identity);
		}
	}

	void Update()
	{
		//When the pillat has go out of despawn limit
		if(Vector2.Distance(player.transform.position, transform.position) > gameplay.despawnLimit)
		{
			//Destroy the object
			Destroy(gameObject);
		}
		//If this is the first time player has pass this pillar
		if(transform.position.x < player.transform.position.x && !passed)
		{
			//Start the next pillar if this pillar need to create and increase score
			if(creating) {Gameplay.i.NextPillar(); gameplay.score++;}
			//Has been pass
			passed = true;
		}
		//Change rigid body to static if power are paused
		if(player.power.paused) {rb.bodyType = RigidbodyType2D.Static;}
		//Change rigid body back to dynamic if power are unpaused
		else {rb.bodyType = RigidbodyType2D.Dynamic;}
	}
}
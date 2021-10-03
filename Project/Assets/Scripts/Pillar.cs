using UnityEngine;

public class Pillar : MonoBehaviour
{
	bool passed;
	[SerializeField] bool creating;
	Generator generate; Player player;
	[SerializeField] Rigidbody2D rb;
	public bool locked;
	[SerializeField] SpriteRenderer render;
	[SerializeField] Color defaultColor;
	[SerializeField] Color lockColor;
	[SerializeField] Color freezeColor;

	void Start()
	{
		//Get the gameplay and player
		generate = Generator.i; player = Player.i;
		//Save the default color
		defaultColor = render.color;
	}

	void Update()
	{
		//When the pillat has go out of despawn limit
		if(Vector2.Distance(player.transform.position, transform.position) > generate.despawnLimit)
		{
			//Destroy the object
			Destroy(gameObject);
		}
		//If this is the first time player has pass this pillar
		if(transform.position.x < player.transform.position.x && !passed)
		{
			//Start the next pillar if this pillar need to create
			if(creating) {generate.NextPillar();}
			//Increase the game score and point
			generate.score++; Point.i.IncreasePoint();
			//Has been pass
			passed = true;
		}
		//Change rigidbody to static if has been lock and change the color to lock, default color to lock
		if(locked) {rb.bodyType = RigidbodyType2D.Static;render.color = lockColor;defaultColor=lockColor;}
		//Change rigid body to static if power are freezed then change the color to freeze
		if(player.power.freezed) {rb.bodyType = RigidbodyType2D.Static;render.color = freezeColor;}
		//Change rigid body back to dynamic if power are unfreezed then reset to default color
		else if(!locked) {rb.bodyType = RigidbodyType2D.Dynamic; render.color = defaultColor;}
	}
}
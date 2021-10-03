using UnityEngine;

public class Pillar : MonoBehaviour
{
	bool passed;
	[SerializeField] bool creating;
	Generator generate; Player player; Point point; 
	[SerializeField] Rigidbody2D rb;
	public bool locked;
	[SerializeField] SpriteRenderer render;
	[SerializeField] SpriteRenderer shadow;
	[SerializeField] Color defaultColor;
	[SerializeField] Color goldenColor;
	[SerializeField] Color lockColor;
	[SerializeField] Color freezeColor;
	[SerializeField] bool golden;
	[SerializeField] AudioClip goldenGet;

	void Start()
	{
		//Get the gameplay, player and point
		generate = Generator.i; player = Player.i; point = Point.i;
		//Increase the generate count
		generate.created++;
		//If this pillar are at the golden rate then make golden 
		if(generate.created % point.goldenRate == 0) {;render.color = goldenColor; golden = true;}
		//Save the default color
		defaultColor = render.color;
	}

	void Update()
	{
		//Get the hsv color
		float h,s,v; Color.RGBToHSV(render.color, out h, out s, out v);
		//Lower the shadow birghtness
		shadow.color = Color.HSVToRGB(h,s,v-0.02f);
		//When the pillar has go out of despawn limit on the X asix
		if((player.transform.position.x - transform.position.x) > generate.despawnLimit)
		{
			//Destroy the object
			Destroy(gameObject);
		}
		//If this is the first time player has pass this pillar
		if(transform.position.x < player.transform.position.x && !passed)
		{
			//Start the next pillar if this pillar need to create
			if(creating) {generate.NextPillar();}
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

	public void GettingPoint() 
	{
		//Increase power normally if this pillar is not golden
		if(!golden) {point.IncreasePassPoint();}
		//Increase golen power if it is golden
		else {point.IncreaseGoldenPower();SoundManager.i.source.PlayOneShot(goldenGet);}
	}
}
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
	public Rigidbody2D rb; 
	public bool isGround;
	[SerializeField] Vector2 spawnpoint;
	[SerializeField] Transform voidArea;
	public static Player i;
	public Power power;
	public GameObject blockIndicate, lockIndicate;
	public bool started;

	//Make player into singleton
	void Awake() {i = this;}

	void Update()
	{
		//Start when pressing space
		if(!started && Input.GetKeyDown(KeyCode.Space)) 
		//Change type to dynamic and start the game
		{started = true; rb.bodyType = RigidbodyType2D.Dynamic;}
	}

	void FixedUpdate()
	{
		//Moving the rigidbody along the X axis with speed if started
		if(started) rb.velocity = new Vector2(1 * speed, rb.velocity.y);
	}

	void LateUpdate()
	{
		//Camera following the player position
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		//Make the void area follow along the player X axis
		voidArea.position = new Vector2(transform.position.x, voidArea.position.y);
		//Update the state of indicate
		blockIndicate.SetActive(power.placeBlock); lockIndicate.SetActive(power.locking);
		//If the block indicate is active
		if(blockIndicate.activeInHierarchy)
		//Make it follow the mouse
		{blockIndicate.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);}
		//If the lock indicate is active
		if(lockIndicate.activeInHierarchy)
		//Make it follow the mouse
		{lockIndicate.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);}
	}

	private void OnCollisionEnter2D(Collision2D other) 
	{
		//Is on ground if has collide with pillar
		if(other.collider.CompareTag("Pillar")) {isGround = true;}
		//Reset the player to spawnpoint if hit void
		if(other.collider.CompareTag("Void")) {transform.position = spawnpoint;}
	}

	private void OnCollisionExit2D(Collision2D other) 
	{
		//No longer on ground if exit collide with pillar
		if(other.collider.CompareTag("Pillar")) {isGround = false;}
	}
}

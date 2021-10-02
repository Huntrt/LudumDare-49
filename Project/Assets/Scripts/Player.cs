using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed, jump;
	[SerializeField] Rigidbody2D rb; 
	[SerializeField] Vector2 spawnpoint;
	[SerializeField] bool isGround; float movement;
	[SerializeField] Transform voidArea;
	public static Player i;

	void Awake()
	{
		i = this;
	}

	void Update()
	{
		//Get the moving as horizontal input smoothly
		movement = Input.GetAxisRaw("Horizontal");
		//If press the up arrow and is on the ground
		if(Input.GetKey(KeyCode.UpArrow) && isGround)
		{
			//Add the jump force upward using force
			rb.velocity = Vector2.up * jump;
		}
	}

	void FixedUpdate()
	{
		//Moving the rigidbody along the X axis with speed 
		rb.velocity = new Vector2(movement * speed, rb.velocity.y);
	}

	void LateUpdate()
	{
		//Camera following the player position
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		//Make the void area follow along the player X axis
		voidArea.position = new Vector2(transform.position.x, voidArea.position.y);
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

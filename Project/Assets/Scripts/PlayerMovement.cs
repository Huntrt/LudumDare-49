using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed, jump;
	[SerializeField] Rigidbody2D rb; 
	[SerializeField] bool isGround; float movement;

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

	private void OnCollisionEnter2D(Collision2D other) 
	{
		//Is on ground if has collide with pillar
		if(other.collider.CompareTag("Pillar")) {isGround = true;}
		//Reset the player if void
		if(other.collider.CompareTag("Void")) {transform.position = Vector3.zero;}
	}
	private void OnCollisionExit2D(Collision2D other) 
	{
		//No longer on ground if exit collide with pillar
		if(other.collider.CompareTag("Pillar")) {isGround = false;}
	}
}

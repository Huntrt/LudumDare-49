﻿using UnityEngine;

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
	[SerializeField] GameObject startTutor, lockPower;
	public GameObject overMenu;
	[SerializeField] TMPro.TextMeshProUGUI overTitle;

	//Make player into singleton and reset time scale
	void Awake() {Time.timeScale = 1;i = this;}

	void Update()
	{
		//When pressing space and haven't started
		if(Input.GetKeyDown(KeyCode.Space) && !started)
		{
			//Starting and change rigidbody to dynamic
			started=true; rb.bodyType = RigidbodyType2D.Dynamic;
			//Hide tutor and unlock power 
			startTutor.SetActive(false); lockPower.SetActive(false);
		}
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
		//Die if hit void
		if(other.collider.CompareTag("Void")) {Die();}
	}

	void Die()
	{
		//Acitve the game over menu and deactive the player
		overMenu.SetActive(true); gameObject.SetActive(false);
		//Display the over text with score
		overTitle.text = "GAME OVER - Score: " + Generator.i.score;  
	}

	private void OnCollisionExit2D(Collision2D other) 
	{
		//No longer on ground if exit collide with pillar
		if(other.collider.CompareTag("Pillar")) {isGround = false;}
	}
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Power : MonoBehaviour
{
	public int powerPoint;
	[SerializeField] TextMeshProUGUI powerCounter;
	[Header("Jump")]
		public int groundJumpCost; 
		public int airJumpCost; 
		int jumpCost;
		public float jumpForce;
		[SerializeField] GameObject groundJump, airJump, jumpEffect;
	[Header("Boost")]
		public int boostCost; 
		public float boostSpeed; public float boostDuration;
		[SerializeField] GameObject boostEffect;
		[SerializeField] TrailRenderer boosteTrail;
	[Header("Block")]
		public int blockCost;
		public bool placeBlock = false;
		public GameObject block;
	[Header("Lock")]
		public int lockCost;
		public bool locking = false;
	[Header("Freeze")]
		public int freezeCost;
		public float freezeDuration; float freezeCounter;
		public bool freezed;
		public Image freezeProgress;
	[Header("Interface")]
	public TextMeshProUGUI groundJumpCostUI;
	public TextMeshProUGUI airJumpCostUI, boostCostUI, lockCostUI, blockCostUI, freezeCostUI;
	public Button groundJumpButton, airJumpButton, boostButton, blockButton, lockButton, freezeButton;
	public AudioClip jumpSound, boostSound, lockSound, blockSound, freezeSound;
	Rigidbody2D rb;
	Vector2 mousePos;
	Player p;

	void Start() 
	{
		//Get the player and it rigidbody
		p = Player.i; rb = p.rb;
		//@ Update the cost UI
		groundJumpCostUI.text = groundJumpCost.ToString();
		airJumpCostUI.text = airJumpCost.ToString();
		boostCostUI.text = boostCost.ToString();
		lockCostUI.text = lockCost.ToString();
		blockCostUI.text = blockCost.ToString();
		freezeCostUI.text = freezeCost.ToString();
	}

	void Update()
	{
		//Updat the power counter
		powerCounter.text = "Power: " + powerPoint;
		//Get the mouse position
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Control the cost
		CostManagement();
		//Run power function
		JumpChange(); PlacingBlock(); StartLocking(); BeginFreeze();
		//The power are not been lock
		if(!Player.i.lockPower.activeInHierarchy)
		{
			//@ Run ability when press 1 to 5 and only work if it button are interactable
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				//Able to use air jump while not on the ground
				if(airJumpButton.interactable && !Player.i.isGround) {Jumping();}
				//Jump if on the ground
				if(Player.i.isGround) {Jumping();}
			}
			if(Input.GetKeyDown(KeyCode.Alpha2)&&boostButton.interactable) {Boosting();}
			if(Input.GetKey(KeyCode.Alpha3)&&blockButton.interactable) {Block();}
			if(Input.GetKey(KeyCode.Alpha4)&&lockButton.interactable) {Locking();}
			if(Input.GetKey(KeyCode.Alpha5)) {Freezing();}
		}
	}

	void CostManagement()
	{
		//@ Update the button interaction depend of it cost
		if(powerPoint >= groundJumpCost) {groundJumpButton.interactable = true;}
		else {groundJumpButton.interactable = false;}
		if(powerPoint >= airJumpCost) {airJumpButton.interactable = true;}
		else {airJumpButton.interactable = false;}
		if(powerPoint >= boostCost) {boostButton.interactable = true;}
		else {boostButton.interactable = false;}
		if(powerPoint >= blockCost) {blockButton.interactable = true;}
		else {blockButton.interactable = false;}
		if(powerPoint >= lockCost) {lockButton.interactable = true;}
		else {lockButton.interactable = false;}
		if(powerPoint >= freezeCost) {freezeButton.interactable = true;}
		else {freezeButton.interactable = false;}
	}

	#region Jump
		public void Jumping() 
		{
			//Consume the cost
			powerPoint -= jumpCost;
			//Create jump effect
			Instantiate(jumpEffect, Player.i.transform.position, Quaternion.identity);
			//Jump upward using jump force
			rb.velocity = Vector2.up * jumpForce;
			SoundManager.i.source.PlayOneShot(jumpSound);
		}
		void JumpChange()
		{
			//Ground jump if the player are touching ground and use the ground cost
			if(p.isGround) {groundJump.SetActive(true); airJump.SetActive(false); jumpCost = groundJumpCost;}
			//Air jump if the player are in the air and use the air cost
			else {groundJump.SetActive(false); airJump.SetActive(true); jumpCost = airJumpCost;}
		}
	#endregion

	#region Boost
		public void Boosting() 
		{
			//Consume the cost
			powerPoint -= boostCost;
			//Create booste effect
			Instantiate(boostEffect, Player.i.transform.position, Quaternion.identity);
			SoundManager.i.source.PlayOneShot(boostSound);
			boosteTrail.emitting = true;
			//Increase the player speed with boost speed then begin reset it after duration
			p.speed += boostSpeed; Invoke("ResetBoost", boostDuration);
		}
		//Reset the player speed from boosting
		void ResetBoost() {p.speed -= boostSpeed; boosteTrail.emitting = false;}
	#endregion

	#region Block
		public void Block() 
		{
			//If able to interact with block button	
			if(blockButton.interactable)
			{
				//Placing block
				placeBlock = true;
				//Consume the cost
				powerPoint -= blockCost;
			}
		}
		void PlacingBlock()
		{
			//If begin place block
			if(placeBlock)
			{
				//Pause the game
				Time.timeScale = 0;
				//No longer able to press block button
				blockButton.interactable = false;
				//If pressing the mouse button
				if(Input.GetMouseButtonDown(0))
				{
					SoundManager.i.source.PlayOneShot(blockSound);
					//Create theblock at mouse position with no rotation
					Instantiate(block, mousePos, Quaternion.identity);
					//Has placed block
					placeBlock = false;
					//Unpause the game
					Time.timeScale = 1;
					//Able to interact with block button
					blockButton.interactable = true;
				}
			}
		}
	#endregion

	#region Locking
		public void Locking() 
		{
			//If able to interact with lock button	
			if(lockButton.interactable)
			{
				//Locking
				locking = true;
				//Consume the cost
				powerPoint -= lockCost;
			}
		}
		void StartLocking()
		{
			//If begin locking
			if(locking)
			{
				//No longer able to press lock button
				lockButton.interactable = false;
				//If pressing the mouse button
				if(Input.GetMouseButtonDown(0))
				{
					//Get the pillar layer int
					int layer = LayerMask.NameToLayer("Pillar");
					//Create an ray at mouse position with no length at the pillar layer
					RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.up, 0, 1<<layer);
					//If ray hit something
					if(ray)
					{
						//Get the pillar raycast has hit
						Pillar pillar = ray.collider.transform.parent.GetComponent<Pillar>();
						//If pillar are not lock
						if(!pillar.locked) 
						{
							SoundManager.i.source.PlayOneShot(lockSound);
							//Lock the pillar and has used lock
							pillar.locked = true; locking = false;
							//Able to interact with lock button
							lockButton.interactable = true;
						}
					}
				}
			}
		}
	#endregion

	#region Freezing
		public void Freezing() 
		{
			//If able to freeze and still has point to freeze
			if(!freezed && powerPoint >= freezeCost) 
			{
				//Begin freeze 
				freezed = true;
				//Consume the cost
				powerPoint -= freezeCost;
				SoundManager.i.source.PlayOneShot(freezeSound);
			}
		}
		void BeginFreeze()
		{
			//If pillar are freeze
			if(freezed)
			{
				//Display the freeze progress slowly decreasing
				freezeProgress.fillAmount = 1-(freezeCounter/freezeDuration);
				//Increase the freezecounter over time
				freezeCounter += Time.deltaTime;
				//If freeze counter has reach it duration
				if(freezeCounter >= freezeDuration)
				{
					//Reset the freeze counter
					freezeCounter -= freezeCounter;
					//No longer freeze 
					freezed = false;
					//Reset freeze progress
					freezeProgress.fillAmount = 1;
				}
			}
		}
	#endregion
}
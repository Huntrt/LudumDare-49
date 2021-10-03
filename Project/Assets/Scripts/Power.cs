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
		public float jumpForce;
		[SerializeField] GameObject groundJump, airJump;
	[Header("Boost")]
		public int boostCost; 
		public float boostSpeed; public float boostDuration;
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
	Rigidbody2D rb;
	Player player;

	void Start() 
	{
		//Get the player and it rigidbody
		player = Player.i; rb = player.rb;
		//Update the cost UI
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
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//If begin place block
		if(placeBlock)
		{
			//If pressing the mouse button
			if(Input.GetMouseButtonDown(0))
			{
				//Create theblock at mouse position with no rotation
				Instantiate(block, mousePos, Quaternion.identity);
				//Has placed block
				placeBlock = false;
			}
		}
		//If begin locking
		if(locking)
		{
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
					//Lock the pillar if it haven't been lock and has used lock
					if(!pillar.locked) {pillar.locked = true; locking = false;}
				}
			}
		}
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
		//Ground jump when the player is on ground an not allow to air jump
		if(player.isGround) {groundJump.SetActive(true); airJump.SetActive(false);}
		//Air jump when the player is not on ground an not allow to ground jump
		else {groundJump.SetActive(false); airJump.SetActive(true);}
	}

	public void Jumping() 
	{
		//Jump upward using jump force
		rb.velocity = Vector2.up * jumpForce;
	}

	public void Boosting() 
	{
		//Increase the player speed with boost speed then begin reset it after duration
		player.speed += boostSpeed; Invoke("ResetBoost", boostDuration);
	}

	public void ResetBoost() 
	{
		//Reset the player speed from boosting
		player.speed -= boostSpeed;
	}

	public void Block() 
	{
		//Placing block
		placeBlock = true;
	}

	public void Locking() 
	{
		//Locking
		locking = true;
	}

	public void Freezing() 
	{
		//Begin freeze if able to
		if(!freezed) freezed = true;
	}
}

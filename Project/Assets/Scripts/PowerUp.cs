using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
	public string usePower;
	Player player;
	public float dashDuration, dashSpeed, lockDuration, pauseDuration;
	float lockCounter; bool locked; float pauseCounter;
	public bool paused;
	[SerializeField] Image durationProgress;

    public void GrantPower(string type)
	{
		//Change use power to the current power
		usePower = type+"Power";
	}

	//Get the player
	void Start() {player = Player.i;}

	void Update()
	{
		//If pressing space while has power
		if(usePower != "null" && Input.GetKeyDown(KeyCode.Space))
		{
			//Use the power and no longer has power
			Invoke(usePower, 0); usePower = "null";
		}
		//If pillar are locking
		if(locked)
		{
			//Disable the player mass
			player.rb.mass = 0;
			//Display the lock progress
			durationProgress.fillAmount = lockCounter/lockDuration;
			//Increase the lockcounter over time
			lockCounter += Time.deltaTime;
			//Lif lock counter has reach it duration
			if(lockCounter >= lockDuration)
			{
				//Reset the lock counter
				lockCounter -= lockCounter;
				//Reset the player mass
				player.rb.mass = 1;
				//No longer lock
				locked = false;
			}
		}
		//If pillar are pause
		if(paused)
		{
			//Display the pause progress
			durationProgress.fillAmount = pauseCounter/pauseDuration;
			//Increase the pausecounter over time
			pauseCounter += Time.deltaTime;
			//Lif pause counter has reach it duration
			if(pauseCounter >= pauseDuration)
			{
				//Reset the pause counter
				pauseCounter -= pauseCounter;
				//No longer pause 
				paused = false;
			}
		}
	}

	//Jump once
	void JumpPower() {player.Jump();}
	//Begin dashing by increasing speed with dash then reset it after set time
	void DashPower() {player.speed += dashSpeed; Invoke("ResetDash",dashDuration);}
	//Reset speed back to original
	void ResetDash() {player.speed -= dashSpeed;}
	//Pillar are now locked
	void LockPower() {locked = true;}
	//Pillar are now pause
	void PausePower() {paused = true;}
}

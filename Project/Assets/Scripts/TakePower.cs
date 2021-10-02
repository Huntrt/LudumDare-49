using UnityEngine;

public class TakePower : MonoBehaviour
{
    [SerializeField] string PowerType;

	private void OnTriggerEnter2D(Collider2D other) 
	{
		//If trigger with player
		if(other.CompareTag("Player"))
		{
			//Start power base on type
			Player.i.power.GrantPower(PowerType);
			//Destroy gameobject
			Destroy(gameObject);
		}
	}
}

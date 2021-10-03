using UnityEngine;

public class GetPoint : MonoBehaviour
{
	[SerializeField] Pillar pillar;
    
	private void OnCollisionEnter2D(Collision2D other) 
	{
		//Getting power point if the player hit pillar
		if(other.collider.CompareTag("Player")) {pillar.GettingPoint();}
	}
}

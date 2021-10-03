using UnityEngine;

public class Point : MonoBehaviour
{
	public static Point i;
	public Power power;

	void Awake()
	{
		//Singleton
		i = this;
	}

	public void IncreasePoint()
	{
		//Increase power
		power.powerPoint++;
	}
}

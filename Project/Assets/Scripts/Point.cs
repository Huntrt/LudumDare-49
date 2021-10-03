using UnityEngine;

public class Point : MonoBehaviour
{
	public static Point i;
	public Power power;
	public int passMin, passMax;
	public float chance;
	public int chanceMin, chanceMax;
	public int goldenRate;
	public int goldenMin, goldenMax;
	[SerializeField] TMPro.TextMeshProUGUI powerInput;
	[SerializeField] float inputFade;

	void Awake()
	{
		//Singleton
		i = this;
	}

	public void IncreasePassPoint()
	{
		//Get the result
		float result = Random.Range(0,101);
		//There is no input
		int input = 0;
		//If the result are lower than chance 
		if(result < chance) 
		{
			//Get input as power randomly given by chance power
			input = Random.Range(chanceMin, chanceMax+1);
			//Increase power point with input
			power.powerPoint += input;
		}
		//If result are higher than chance 
		else
		{
			//Get input as power randomly given by pass power
			input = Random.Range(passMin, passMax+1);
			//Increase power point with input
			power.powerPoint += input;
		}
		//Display input if there is one
		if(input != 0) {powerInput.text = "+ " + input;}
		//Reseting input
		ResetInput();
	}

	public void IncreaseGoldenPower()
	{
		//Get input as power randomly given by golden power
		int input = Random.Range(goldenMin, goldenMax+1);
		//Increase power point with input
		power.powerPoint += input;
		//Display input if there is one
		if(input != 0) {powerInput.text = "+ " + input;}
		//Reseting input
		ResetInput();
	}

	//Clear the input after an set time
	void ResetInput() {CancelInvoke(); Invoke("ClearInput", inputFade);}
	//Clear the input back to blank
	void ClearInput() {powerInput.text = "";}
}

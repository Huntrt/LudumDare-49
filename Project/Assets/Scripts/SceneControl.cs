using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
	float recentTime;

	public void ToMenu() {SceneManager.LoadScene("Menu", LoadSceneMode.Single);}
	public void ToGame() {SceneManager.LoadScene("Game", LoadSceneMode.Single);}
	
	//Save the recent time and pause the game
	public void Pause() {recentTime = Time.timeScale; Time.timeScale = 0;}
	//Continue back to the recent time
	public void Continue() {Time.timeScale = recentTime;}
}
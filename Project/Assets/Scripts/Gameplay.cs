using UnityEngine;

public class Gameplay : MonoBehaviour
{
	public static Gameplay i;
	public int score;
	[SerializeField] TMPro.TextMeshProUGUI scoreCounter;
	[Header("Pillar")] public GameObject pillar;
	[SerializeField] float minDistance, maxDistance, minHeight, maxHeight;
	public float despawnLimit;
	[SerializeField] Transform startPillar;
	public Vector2 latestPillar;

    void Awake()
    {
		//Make gameplay singleton
        i = this;
    }

	void Start()
	{
		//Set the the latest to start pillar
		latestPillar = startPillar.position;
		//Create the first 5 pillar
		for (int i = 0; i < 5; i++) {NextPillar();}
	}

	void Update()
	{
		//Display score
		scoreCounter.text = score.ToString();
	}

    public void NextPillar()
    {
		//Increase score
		score++;
		//The next position to spawn
		Vector2 nextPos = Vector2.zero;
		//Increase X axis with randomly min max distance
		nextPos.x = latestPillar.x + Random.Range(minDistance, maxDistance);
		//Randomly generated between height mion max
		nextPos.y = Random.Range(minHeight, maxHeight);
		//Spawn the next pillat at next position with no rotation
        Instantiate(pillar, nextPos, Quaternion.identity);
		//Set the the latest to position of pillar created
		latestPillar = nextPos;
    }
}

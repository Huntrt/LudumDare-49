using UnityEngine; using UnityEngine.Events;

public class Generator : MonoBehaviour
{
	public static Generator i;
	public float score;
	[SerializeField] TMPro.TextMeshProUGUI scoreCounter;
	public int created;
	[Header("Pillar")] public GameObject pillar;
	[SerializeField] float minDistance, maxDistance, minHeight, maxHeight;
	public float despawnLimit;
	[SerializeField] Transform startPillar;
	public Vector2 latestPillar;
	[SerializeField] SceneControl scene;

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
		scoreCounter.text = (System.Math.Round(score)).ToString();
		//Back to game if the game has over while pressing space
		if(Player.i.overMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Space)) {scene.ToGame();}
	}

    public void NextPillar()
    {
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

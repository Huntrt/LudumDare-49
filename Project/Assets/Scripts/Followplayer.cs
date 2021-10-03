using UnityEngine;

public class Followplayer : MonoBehaviour
{
    void LateUpdate()
    {
        transform.position = Player.i.transform.position;
    }
}

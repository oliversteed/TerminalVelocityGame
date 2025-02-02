using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleTracker : MonoBehaviour
{  
    public Transform player;
    public Transform voidWall;
    public float wallOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, voidWall.position.z + wallOffset);
    }
}

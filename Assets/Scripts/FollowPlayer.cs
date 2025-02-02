using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject gravController;
    public GameObject player;
    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.GetComponent<ControlScript>().victory){
            return;
        }
        transform.position = gravController.transform.position + offset;
    }
}

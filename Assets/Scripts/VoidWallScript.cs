using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidWallScript : MonoBehaviour
{ 

    public float voidSpeed;
    public GameObject player;
    
    float playerDistance;
    float finalSpeed;
    [HideInInspector] public bool started = false;
    public float voidMinSpeed;

    // Update is called once per frame
    void Update()
    {   
        if(player.GetComponent<ControlScript>().victory){
        return;
        }

        if(!started && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))){
            started = true;
        }
        if(started){
            //Get Z axis distance from void to player
            playerDistance = player.transform.position.z - transform.position.z;
            //Calculate final speed of void for this frame
            finalSpeed = CalcFinalSpeed(voidSpeed, voidMinSpeed, playerDistance);
            //We movin' schmovin'
            transform.position += new Vector3(0, 0, finalSpeed) * Time.deltaTime;
        }
    }

    float CalcFinalSpeed(float voidSpeed, float voidMinSpeed, float playerDistance){
        //Makes speed proportional to distance from player. Keep on the pressure
        finalSpeed = voidSpeed * (playerDistance-20);
        //Prevent speed from falling below the minimum
        if(finalSpeed < voidMinSpeed){
            finalSpeed = voidMinSpeed;
        }
        return finalSpeed;
    }
}

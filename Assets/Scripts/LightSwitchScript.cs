using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    public GameObject lampLight;
    GameObject player;

    Vector3 distance;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //Called every frame
    void Update(){
        if(GetDistance() < 30f){
            lampLight.SetActive(true);
        }
        else{
            lampLight.SetActive(false);
        }
    }

    float GetDistance(){
        float distMagnitude = 0;

        distMagnitude = (player.transform.position - transform.position).magnitude;

        return distMagnitude;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Vector2 turn;
    public GameObject gravController;
    public GameObject player;

    public float maxUp;
    public float maxDown;

    float rotSpeed = 2.0f;

    Quaternion targetAngle;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {   
        MoveCam();
    }

    void MoveCam(){
        //bail out of function if player has won
        if(player.GetComponent<ControlScript>().victory){
            return;
        }

        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");

        if(turn.y > maxUp && gravController.GetComponent<GravControl>().cameraLock){
            turn.y = maxUp;
        }
        else if(turn.y < maxDown && gravController.GetComponent<GravControl>().cameraLock){
            turn.y = maxDown;
        }

        targetAngle = Quaternion.Euler(-turn.y, turn.x, transform.rotation.z);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetAngle, rotSpeed * Time.deltaTime);
    }
}

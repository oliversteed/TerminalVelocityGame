using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;

public class GravControl : MonoBehaviour
{
    public GameObject player;
    public GameObject gameCamera;
    Ray ray;
    public LayerMask layersToHit;
    public float gravityStrength = 9.8f;
    public bool controlLock = false;
    public bool cameraLock = true;
    public GameObject camTracker;
    public AudioSource flipSound;

    //Private variables
    Quaternion rotateTarget;
    float rotateSpeed = 10f;
    bool gravLock = false;
    bool pauseGrav = false;
    bool flipping = false;
    Quaternion rotateFlip;
    [HideInInspector] public bool gravFlip = false;


    void Update(){
        //Camera follows player
        transform.position = player.transform.position;
        Debug.DrawRay(transform.position, transform.forward * 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(!pauseGrav){
            CastRay();
        }
        else{
            FlipPlayer(-Physics.gravity);
        }
    }

    void CastRay(){
        //Fire ray downwards to check for slopes etc
        ray = new Ray(transform.position, -transform.up * 2f);

        //If raycast successfully detects an object
        if(Physics.Raycast(ray, out RaycastHit hit, 2f, layersToHit)){
            cameraLock = true;
            controlLock = false;

            if(!gravLock){
                AttractToGround(hit.normal);
                Debug.DrawRay(transform.position, hit.normal);
            }
        }
        else{
            cameraLock = false;
            controlLock = true;
            player.GetComponent<ControlScript>().readySound = true;
        }
    }

    //Shift gravity & player to align with collision face normal
    void AttractToGround(Vector3 normal){
        Vector3 normalAlign = normal;
        if(gravFlip){
           normalAlign = -normal;
           gravFlip = false;
           StartCoroutine(PauseGrav());
        }
        Physics.gravity = -normalAlign * gravityStrength;
        AlignPlayer(normalAlign);
    }

    //NORMAL ROTATION  
    void AlignPlayer(Vector3 normalAlign){
        rotateTarget = Quaternion.FromToRotation(transform.up, normalAlign) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateTarget, rotateSpeed * Time.deltaTime);
    }

    //ROTATION WHEN FLIPPING
    void FlipPlayer(Vector3 normalAlign){
        if(!flipping){
            rotateFlip = Quaternion.AngleAxis(180f, camTracker.transform.forward) * transform.localRotation;
            flipSound.Play();
            flipping = true;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateFlip, rotateSpeed * Time.deltaTime);
    }

    IEnumerator PauseGrav(){
        pauseGrav = true;
        yield return new WaitForSeconds(1f);
        pauseGrav = false;
        flipping = false;
    }
}          

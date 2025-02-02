using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamWarnScript : MonoBehaviour
{
    public Animator camAnim;
    public GameObject playerTracker;
    public GameObject player;
    public AudioSource warn;

    bool soundPlay;

    Vector3 dist;

    // Update is called once per frame
    void Update()
    {
        dist = player.transform.position - playerTracker.transform.position;

        if(dist.magnitude < 30f){
            if(!soundPlay){
                StartCoroutine(SoundGate());
                soundPlay = true;
            }
            camAnim.SetBool("TooClose", true);
        }
        else if(camAnim.GetBool("TooClose")){
            camAnim.SetBool("TooClose", false);
        }
    }

    IEnumerator SoundGate(){
        warn.Play();
        yield return new WaitForSeconds(2f);
        soundPlay = false;
    }
}

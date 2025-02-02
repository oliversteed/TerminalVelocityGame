using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ControlScript : MonoBehaviour
{

     public Rigidbody rb;
     public float maxMoveForce;
     public GameObject gravController;
     public GameObject gameCamera;
     public float accelerationRate;
     public GameObject deathScreen;
     public GameObject pauseScreen;
     public GameObject uiObj;
     public GameObject voidWall;
     public GameObject hazardScreen;
     public GameObject victoryScreen;
     public TextMeshProUGUI timeText;
     [SerializeField] TextMeshProUGUI timerText;
     public float speedLimit;
     public AudioSource windSound;
     public AudioSource impactSound;
     public AudioSource music;
     public AudioSource voidSound;
     [HideInInspector] public bool victory = false;
     Vector3 moveDirection;
     float moveForce;
     bool isDead = false;
     bool isPaused = false;
     Vector3 finPos;
     float timer = 0f;
     float courseTimer = 0f;
     [HideInInspector] public bool readySound = false;

     void Start(){
          Time.timeScale = 1f;
          moveForce = maxMoveForce;
     }

     void Update(){

          float ballVelocity = rb.velocity.magnitude;
          if(ballVelocity >= speedLimit){
               moveForce = 0f;
          }
          else{
               moveForce = maxMoveForce;
          }

          if(Input.GetKeyDown(KeyCode.Escape)){
               Pause();
          }

          if(Input.GetKeyDown("r") && !isPaused && !isDead){
               uiObj.GetComponent<ButtonFunctions>().Retry();
          }

          if(readySound){
               VoidTimer();
          }

          if(voidWall.GetComponent<VoidWallScript>().started){
               CourseTimer();
          }

          if(victory){
               music.volume -= 0.05f * Time.deltaTime;
               gameCamera.transform.position = finPos;
          }

          windSound.volume = VolumeControl(ballVelocity, -0.05f);
     }
     //Controls void timer for when player is airborne too long
     void VoidTimer(){
          if(victory){
               return;
          }
          timer += Time.deltaTime;
          if(timer > 3f){
               if(!isPaused && !isDead){
                    hazardScreen.SetActive(true);
               }
               float displayNum = 8 - timer;
               if(timer > 8f){
                    displayNum = 0f;
               }
               timeText.text = displayNum.ToString("0.00");
          }
          if(timer > 8f){
               voidWall.GetComponent<VoidWallScript>().voidMinSpeed = speedLimit * 3f;
          }
     }

     void CourseTimer(){
          if(!victory){
              courseTimer += Time.deltaTime;
              int minutes = Mathf.FloorToInt(courseTimer/60);
              int seconds = Mathf.FloorToInt(courseTimer%60);
              timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
          }
     }

     //Controls volume of win SFX
     float VolumeControl(float velocity, float offset){
          float vol;
          vol = velocity/200f + offset;
          if(velocity > 100){
               velocity = 100f;
          }
          return vol;
     }
    // Update is called once per frame
    void FixedUpdate()
    {    
          moveDirection = new Vector3(0, 0, 0);

          if(Input.GetKey("w")){
               moveDirection += gameCamera.transform.forward;
          }
          if(Input.GetKey("s")){
               moveDirection += -gameCamera.transform.forward;
          }
          if(Input.GetKey("d")){
               moveDirection += gameCamera.transform.right;
          }
          if(Input.GetKey("a")){
               moveDirection += -gameCamera.transform.right;
          }

          MovePlayer(moveDirection.normalized, moveForce);
    }

     // Code for moving player
    void MovePlayer(Vector3 dir, float force){
          rb.AddForce(dir * force * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision){
          if(!readySound && collision.gameObject.tag == "terrain"){
               return;
          }
          float impact = collision.relativeVelocity.magnitude;
          impactSound.volume = impact/100f;
          impactSound.Play();
          readySound = false;
          timer = 0f;
          hazardScreen.SetActive(false);
    }

     // Trigger Checks
    void OnTriggerEnter(Collider collider){
          switch(collider.gameObject.tag){
               case "VoidWall":
                    GameOver();
                    break;
               
               case "GravityFlip":
                    gravController.GetComponent<GravControl>().gravFlip = true;
                    break;
               
               case "Goal":
                    Victory();
                    break;
          }
    }

     //Pause Code
    void Pause(){
          if(!isDead){
               if(!isPaused){
                    isPaused = true;
                    windSound.Pause();
                    music.volume = music.volume/3;
                    voidSound.Pause();
                    pauseScreen.SetActive(true);
                    hazardScreen.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0;
               }
               else{
                    isPaused = false;
                    windSound.Play();
                    music.volume = music.volume*3;
                    voidSound.Play();
                    pauseScreen.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;
               }
          }
    }

     //Game over code
    void GameOver(){
          isDead = true;
          timerText.color = Color.red;
          deathScreen.SetActive(true);
          hazardScreen.SetActive(false);
          windSound.Pause();
          music.Pause();
          voidSound.Pause();
          Cursor.lockState = CursorLockMode.None;
          Time.timeScale = 0;
    }

    void Victory(){
          victory = true;
          timerText.color = Color.green;
          hazardScreen.SetActive(false);
          finPos = gameCamera.transform.position;
          StartCoroutine(EndTime());
          StartCoroutine(Goal());
    }

    IEnumerator EndTime(){
          yield return new WaitForSecondsRealtime(10f);
          Time.timeScale = 0;
    }

    IEnumerator Goal(){
          yield return new WaitForSeconds(2f);
          victoryScreen.SetActive(true);
          Cursor.lockState = CursorLockMode.None;
    }

}

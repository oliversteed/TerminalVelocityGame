using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{  
    public AudioSource playSound;
    public AudioSource backSound;
    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadCM(){
        playSound.Play();
        StartCoroutine(CMPause());
    }

    public void Menu(){
        backSound.Play();
        StartCoroutine(MenuPause());
    }

    public void Quit(){
        backSound.Play();
        StartCoroutine(QuitPause());
    }

    IEnumerator MenuPause(){
        yield return new WaitForSecondsRealtime(.5f);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator QuitPause(){
        yield return new WaitForSecondsRealtime(.5f);
        Application.Quit();
    }

    IEnumerator CMPause(){
        yield return new WaitForSecondsRealtime(.5f);
        SceneManager.LoadScene("CyberMotorway");
    }
}

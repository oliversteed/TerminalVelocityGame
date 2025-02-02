using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsFunction : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject creditsScreen;
    public GameObject instScreen;
    public AudioSource backSound;
    public AudioSource playSound;

    public void Credits(){
        StartCoroutine(CreditsPause());
        playSound.Play();
    }

    public void Instructions(){
        StartCoroutine(InstructionPause());
        playSound.Play();
    }

    public void Return(){
        StartCoroutine(ReturnPause());
        backSound.Play();
    }

    IEnumerator CreditsPause(){
        yield return new WaitForSecondsRealtime(.5f);
        menuScreen.SetActive(false);
        creditsScreen.SetActive(true);

    }

    IEnumerator ReturnPause(){
        yield return new WaitForSecondsRealtime(.5f);
        menuScreen.SetActive(true);
        instScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    IEnumerator InstructionPause(){
        yield return new WaitForSecondsRealtime(.5f);
        menuScreen.SetActive(false);
        instScreen.SetActive(true);
    }
}

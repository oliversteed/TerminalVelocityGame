using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseScript : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler , IPointerDownHandler , IPointerUpHandler
{   
    public GameObject image;
    public AudioSource hoverSound;
    Animator playAnim;

    void Start(){
        playAnim = image.GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData){
        playAnim.SetBool("hovering", true);
        playAnim.SetBool("idling", false);

        hoverSound.Play();
    }

    public void OnPointerExit(PointerEventData eventData){
        playAnim.SetBool("idling", true);
        playAnim.SetBool("hovering", false);
    }

    public void OnPointerDown(PointerEventData eventData){
        playAnim.SetBool("Clicked", true);
        playAnim.SetBool("hovering", false);
        playAnim.SetBool("idling", false);
    }

    public void OnPointerUp(PointerEventData eventData){
        playAnim.SetBool("Clicked", false);
    }
}

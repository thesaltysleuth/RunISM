using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
public bool buttonPressed;
public Rigidbody2D rb;
public GameObject playa;
[SerializeField] private float speed = 5f;
void Update(){

    if(buttonPressed){
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        playa.transform.localScale = new Vector2(-1, 1);
    }

}
public void OnPointerDown(PointerEventData eventData){
     buttonPressed = true;
}
 
public void OnPointerUp(PointerEventData eventData){
    buttonPressed = false;
}
}
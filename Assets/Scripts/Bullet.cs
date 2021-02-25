using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    [SerializeField] private float hurtForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * -speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){

        PlayerController player = hitInfo.GetComponent<PlayerController>();
        //Debug.Log(hitInfo.name);
        if(player!= null){
            player.TakeDamage(20);
            player.hurtAnimation();
            Destroy(gameObject);
            
        }
        //Destroy(gameObject);
    }

 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;

    public GameObject warning;
    public GameObject[] bulletPrefab;
    public Transform playerpos;
    float timer=0;
    public float targetTime=2;
    int k=0;
    bool hasplayed=false;
    AudioSource audioData;
    // Update is called once per frame
    void Start(){
        audioData = GetComponent<AudioSource>();
    }
    void Update()
    {
        timer+=Time.deltaTime;
        if(playerpos.position.x>=-80 && timer>=targetTime){
            Shoot();
            timer=0;
        }
        if(playerpos.position.x>=-120 && hasplayed==false){
            music();
            hasplayed=true;
            warning.SetActive(true);
        }
        
    }

    public void music(){
        audioData.Play(0);
    }

    void Shoot(){

            k=(k+1)%4;
            Instantiate(bulletPrefab[k], firePoint.position, firePoint.rotation);
    }
}

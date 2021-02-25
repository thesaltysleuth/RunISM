using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftmusic : MonoBehaviour
{
    AudioSource audioData;
    public Transform playerpos;
    bool hasplayed=false;
    // Update is called once per frame
    void Start(){
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerpos.position.x >= -240 && hasplayed==false){
            music2();
            hasplayed=true;
        }   

        if(playerpos.position.x >= -135){
            audioData.Stop();
        }

    }

    public void music2(){
        audioData.Play(0);
    }

}

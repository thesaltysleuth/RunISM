using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int mxHealth = 100; //69
    public int currHealth;   //69
    public HealthBar healthBar;

    public GameObject finalboss;
    public Animator Bossanim;
    public GameObject wall;
    AudioSource audioData;
    void Start()
    {
        currHealth=mxHealth;
        healthBar.SetMaxHealth(mxHealth);
        audioData = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(currHealth<=0)
        {
            //Debug.Log("wow");
            Bossanim.SetTrigger("BossDeath");
            StartCoroutine(Test());
        }

    }

    IEnumerator Test(){

        yield return new  WaitForSeconds(0.25f);
        Destroy(finalboss);
        Destroy(wall);
    }


    public void BossDeath()
    {
        Debug.Log("fdsa");
        Destroy(this.gameObject);
        audioData.Stop();
    }


    public void JumpedOn()
    {
        //bossanim.SetTrigger("BossHurt");
        TakeBossDamage(20);
    }


    public void TakeBossDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().buildIndex!=5){
                Pass();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

      public void Pass()  {
         int currentLevel = SceneManager.GetActiveScene().buildIndex;

         if (currentLevel >= PlayerPrefs.GetInt("levelIsUnlocked")) {
             PlayerPrefs.SetInt("levelIsUnlocked", currentLevel);
         }

          Debug.Log("LEVEL"+ PlayerPrefs.GetInt("levelIsUnlocked") +" UNLOCKED");

    }
}

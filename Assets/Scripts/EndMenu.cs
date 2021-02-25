using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public GameObject playa;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI assignmentText;

    int v1=0;
    int v2=0;
    int v3=0;
    void Start(){
        PlayerController player = playa.GetComponent<PlayerController>();
        player.EndResult();
            v1=player.displayassignments;
            v2=player.displayenemies;
            v3=player.displaytime+180;
            timeText.text = v3.ToString();
            enemyText.text = v2.ToString();
            assignmentText.text = v1.ToString();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenYT(){
        Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }

}

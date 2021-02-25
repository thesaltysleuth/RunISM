﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    int levelIsUnlocked;


    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        levelIsUnlocked = PlayerPrefs.GetInt("levelIsUnlocked", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < levelIsUnlocked; i++)
        {
            buttons[i].interactable = true;
            buttons[i].gameObject.SetActive(true);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevels()
    {
        PlayerPrefs.DeleteAll();
    }
}
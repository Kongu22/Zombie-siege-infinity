using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get;  set; }

    String highScoreKey = "HighScore";
    private void Awake() // Singleton
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    //highscore save and load functions 
    public void SaveHighScore(int score)
    {
        //save highscore to playerprefs
        PlayerPrefs.SetInt(highScoreKey, score);
    }

    //load highscore from playerprefs 
    public int LoadHighScore()
    {
        //load highscore from playerprefs
        if(PlayerPrefs.HasKey(highScoreKey))
        {
            return PlayerPrefs.GetInt(highScoreKey);
        }
        else
        {
            return 0;
        }
        
    }
}

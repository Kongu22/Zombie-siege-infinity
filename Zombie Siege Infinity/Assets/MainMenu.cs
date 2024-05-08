using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string newGameScene = "SampleScene";

    public AudioClip bg_music;
    public AudioSource main_Channel;
    public TMP_Text HighScoreUI;
    void Start()
    {
        //play the background music
        main_Channel.PlayOneShot(bg_music);

        //set the high score text 
        int highScore = SaveLoadManager.Instance.LoadHighScore();
        HighScoreUI.text = $"Top Wave Survived: {highScore}";
    }

    public void StartNewGame()
    {
        //close the music
        main_Channel.Stop();
        //start the game 
        SceneManager.LoadSceneAsync(newGameScene);
    }

    public void QuitGame()
    {

    #if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
    #else  
        Application.Quit();
    #endif
    }

    
}

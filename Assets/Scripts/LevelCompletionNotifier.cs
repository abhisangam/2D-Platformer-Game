using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelCompletionNotifier : MonoBehaviour
{
    public delegate void LevelCompletionCallback(int levelNumber);
    public static event LevelCompletionCallback OnLevelCompleted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Levels scenes are named Level1, Level2 etc. Extract leel number out of it
            OnLevelCompleted?.Invoke(int.Parse(SceneManager.GetActiveScene().name.Substring(5)));
        }
    }
}

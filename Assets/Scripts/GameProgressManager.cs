using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : MonoBehaviour
{
    private static GameProgressManager instance;
    public static GameProgressManager Instance { get { return instance; } private set { } }

    private void Awake()
    {
        if(instance == null)
        { 
            instance = this; 
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.DeleteAll();
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetLevelStatus(1, LevelStatus.Unlocked);
    }

    public void SetLevelStatus(int levelNumber, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt("Level" + levelNumber.ToString(), (int)levelStatus);
        Debug.Log("Level " + levelNumber + " Status set to " + levelStatus);
    }

    public LevelStatus GetLevelStatus(int levelNumber)
    {
        string levelName = "Level" + levelNumber.ToString();
        LevelStatus levelStatus = LevelStatus.Locked;
        if (PlayerPrefs.HasKey(levelName))
        {
            levelStatus = (LevelStatus)PlayerPrefs.GetInt(levelName, 0);
        }

        return levelStatus;
    }
}

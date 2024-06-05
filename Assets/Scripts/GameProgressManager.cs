using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : MonoBehaviour
{
    private static GameProgressManager instance;
    public static GameProgressManager Instance { get { return instance; } private set { } }

    private int currentLevelNumber = 1;
    [SerializeField] int numberOfLevels;

    private void Awake()
    {
        if(instance == null)
        { 
            instance = this; 
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.DeleteAll();
            SetLevelStatus(1, LevelStatus.Unlocked);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void Start()
    {
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

    public void LoadLevel(int levelNumber)
    {
        LevelStatus levelStatus = GameProgressManager.Instance.GetLevelStatus(levelNumber);
        if (levelStatus == LevelStatus.Unlocked)
        {
            currentLevelNumber = levelNumber;
            SceneManager.LoadScene("Level" + levelNumber.ToString());
        }
        else if (levelStatus == LevelStatus.Completed)
        {
            currentLevelNumber = levelNumber;
            SceneManager.LoadScene("Level" + levelNumber.ToString());
        }
        else if (levelStatus == LevelStatus.Locked)
        {
            Debug.Log("Level " + levelNumber + " is locked. Complete the previous levels to play this level");
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevelNumber + 1 > numberOfLevels)
            return;
        LevelStatus levelStatus = GameProgressManager.Instance.GetLevelStatus(currentLevelNumber + 1);
        if (levelStatus == LevelStatus.Unlocked)
        {
            currentLevelNumber += 1;
            SceneManager.LoadScene("Level" + currentLevelNumber.ToString());
        }
        else if (levelStatus == LevelStatus.Completed)
        {
            currentLevelNumber += 1;
            SceneManager.LoadScene("Level" + currentLevelNumber.ToString());
        }
        else if (levelStatus == LevelStatus.Locked)
        {
            Debug.Log("Level " + (currentLevelNumber + 1) + " is locked. Complete the previous levels to play this level");
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Level" + currentLevelNumber.ToString());
    }
}

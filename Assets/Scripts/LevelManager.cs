using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform levelStartPoint;

    [SerializeField]
    private LevelCompletionNotifier levelCompletionNotifier;
    [SerializeField]
    private DeathFallNotifier deathFallNotifier;

    [SerializeField]
    private string nextLevelName;

    void Start()
    {
        levelCompletionNotifier.OnLevelCompleted += OnLevelCompleted;
        deathFallNotifier.OnDeathFall += OnDeathFall;
    }

    private void OnDestroy()
    {
        levelCompletionNotifier.OnLevelCompleted -= OnLevelCompleted;
        deathFallNotifier.OnDeathFall -= OnDeathFall;
    }

    void OnLevelCompleted()
    {
        //Load next scene
        SceneManager.LoadScene(nextLevelName);
    }

    void OnDeathFall()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

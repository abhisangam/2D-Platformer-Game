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
    private string nextLevelName;

    void Start()
    {
        levelCompletionNotifier.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnDestroy()
    {
        levelCompletionNotifier.OnLevelCompleted -= OnLevelCompleted;
    }

    void OnLevelCompleted()
    {
        //Load next scene
        SceneManager.LoadScene(nextLevelName);
    }
}

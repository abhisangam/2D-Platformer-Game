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

    [SerializeField]
    private PlayerHealthManager playerHealthManager;

    void Start()
    {
        levelCompletionNotifier.OnLevelCompleted += OnLevelCompleted;
        deathFallNotifier.OnDeathFall += OnDeathFall;
        playerHealthManager.OnPlayerDead += OnPlayerDead;
    }

    private void OnDestroy()
    {
        levelCompletionNotifier.OnLevelCompleted -= OnLevelCompleted;
        deathFallNotifier.OnDeathFall -= OnDeathFall;
        playerHealthManager.OnPlayerDead -= OnPlayerDead;
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

    void OnPlayerDead()
    {
        StartCoroutine(LoadSceneAfterDuration(2.0f));
    }

    IEnumerator LoadSceneAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

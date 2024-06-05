using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform levelStartPoint;

    [SerializeField]
    private DeathFallNotifier deathFallNotifier;

    [SerializeField]
    private PlayerHealthManager playerHealthManager;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject levelCompletePanel;

    void Start()
    {
        LevelCompletionNotifier.OnLevelCompleted += OnLevelCompleted;
        deathFallNotifier.OnDeathFall += OnDeathFall;
        playerHealthManager.OnPlayerDead += OnPlayerDead;
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    private void OnDestroy()
    {
        LevelCompletionNotifier.OnLevelCompleted -= OnLevelCompleted;
        deathFallNotifier.OnDeathFall -= OnDeathFall;
        playerHealthManager.OnPlayerDead -= OnPlayerDead;
    }

    void OnLevelCompleted(int levelNumber)
    {
        //Set level as completed
        GameProgressManager.Instance.SetLevelStatus(levelNumber, LevelStatus.Completed);
        //Unlock next level
        GameProgressManager.Instance.SetLevelStatus(levelNumber + 1, LevelStatus.Unlocked);

        levelCompletePanel.SetActive(true);
    }

    void OnDeathFall()
    {
        ShowGameOver(0.5f);
    }

    void OnPlayerDead()
    {
        ShowGameOver(1.0f);
    }

    void ShowGameOver(float delay)
    {
        if (delay < 0.0001f)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(ShowGameOverAfterDuration(delay));
        }
    }

    IEnumerator ShowGameOverAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameOverPanel.SetActive(true);
    }
}

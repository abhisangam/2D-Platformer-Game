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
    private LevelCompletionNotifier levelCompletionNotifier;
    [SerializeField]
    private DeathFallNotifier deathFallNotifier;

    [SerializeField]
    private string nextLevelName;

    [SerializeField]
    private PlayerHealthManager playerHealthManager;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private Button playAgainButton;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
    }

    void Start()
    {
        levelCompletionNotifier.OnLevelCompleted += OnLevelCompleted;
        deathFallNotifier.OnDeathFall += OnDeathFall;
        playerHealthManager.OnPlayerDead += OnPlayerDead;
        gameOverPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        levelCompletionNotifier.OnLevelCompleted -= OnLevelCompleted;
        deathFallNotifier.OnDeathFall -= OnDeathFall;
        playerHealthManager.OnPlayerDead -= OnPlayerDead;
        playAgainButton.onClick.RemoveAllListeners();
    }

    void OnLevelCompleted()
    {
        //Load next scene
        SceneManager.LoadScene(nextLevelName);
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

    void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

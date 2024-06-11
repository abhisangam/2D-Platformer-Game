using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletionManager : MonoBehaviour
{
    [SerializeField]
    private Button playAgainButton;

    [SerializeField]
    private Button nextButton;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        nextButton.onClick.AddListener(OnPlayNextClicked);
    }

    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();
    }

    void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.Play("ButtonClick");
    }

    void OnPlayNextClicked()
    {
        GameProgressManager.Instance.LoadNextLevel();
        AudioManager.Instance.Play("ButtonClick");
    }
}

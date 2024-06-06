using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Button playAgainButton;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
    }

    private void OnEnable()
    {
        AudioManager.Instance.Play("DeathBG");
    }

    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveAllListeners();
    }

    void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.Play("ButtonClick");
    }
}

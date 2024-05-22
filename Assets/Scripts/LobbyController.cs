using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject levelSelectorPanel;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        quitButton.onClick.AddListener(OnExitClicked);
    }

    void Start()
    {
        levelSelectorPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    void OnPlayClicked()
    {
        levelSelectorPanel.SetActive(true);   
    }

    void OnExitClicked()
    {
        Application.Quit();
    }
}

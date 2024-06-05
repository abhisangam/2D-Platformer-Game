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

    private Button[] levelSelectionButtons;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        quitButton.onClick.AddListener(OnExitClicked);

        levelSelectionButtons = levelSelectorPanel.GetComponentsInChildren<Button>();
    }

    void Start()
    {
        levelSelectorPanel.SetActive(false);

        for(int i = 0; i < levelSelectionButtons.Length; i++)
        {
            LevelStatus ls = GameProgressManager.Instance.GetLevelStatus(levelSelectionButtons[i].GetComponent<LevelLoader>().levelNumber);
            if(ls == LevelStatus.Locked)
            {
                levelSelectionButtons[i].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                levelSelectionButtons[i].GetComponent<Image>().color = Color.white;
            }
        }
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

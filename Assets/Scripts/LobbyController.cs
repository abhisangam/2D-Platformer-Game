using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button m_playButton;

    private void Awake()
    {
        m_playButton.onClick.AddListener(OnPlayClicked);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        m_playButton.onClick.RemoveAllListeners();
    }

    void OnPlayClicked()
    {
        SceneManager.LoadScene(1);
    }
}

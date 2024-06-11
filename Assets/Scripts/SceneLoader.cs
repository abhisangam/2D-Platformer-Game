using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    private Button button;
    public string sceneName;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        //GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    void OnClick()
    {
        SceneManager.LoadScene(sceneName);
        AudioManager.Instance.Play("ButtonClick");
    }
}

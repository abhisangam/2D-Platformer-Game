using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button button;
    public int levelNumber;

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
        GameProgressManager.Instance.LoadLevel(levelNumber);
        AudioManager.Instance.Play("ButtonClick");
    }
}

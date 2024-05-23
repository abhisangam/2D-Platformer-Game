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
        LoadLevel(levelNumber);
    }

    public void LoadLevel(int levelNumber)
    {
        LevelStatus levelStatus = GameProgressManager.Instance.GetLevelStatus(levelNumber);
        if (levelStatus == LevelStatus.Unlocked)
        {
            SceneManager.LoadScene("Level" + levelNumber.ToString());
        }
        else if (levelStatus == LevelStatus.Completed)
        {
            SceneManager.LoadScene("Level" + levelNumber.ToString());
        }
        else if (levelStatus == LevelStatus.Locked)
        {
            Debug.Log("Level " + levelNumber + " is locked. Complete the previous levels to play this level");
        }
    }
}

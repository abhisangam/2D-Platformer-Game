using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    int playerHealth = 3;

    [SerializeField]
    private Image[] playerLives;

    public Action OnPlayerDead;

    void Start()
    {
        for (int i = 0; i < playerLives.Length; i++)
        {
            playerLives[i].gameObject.SetActive(true);
        }

        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterPlayerInjury()
    {
        playerHealth -= 1;
        playerLives[playerHealth >= 0 ? playerHealth:0].gameObject.SetActive(false);

        if(playerHealth == 0)
        {
            OnPlayerDead.Invoke();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFallNotifier : MonoBehaviour
{
    public Action OnDeathFall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            OnDeathFall?.Invoke();
        }
    }
}

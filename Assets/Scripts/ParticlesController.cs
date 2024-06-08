using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEffect()
    {
        if(particleSystem != null)
            particleSystem.Play();
    }

    public void StopEffect()
    {
        if (particleSystem != null)
            particleSystem.Stop();
    }
}

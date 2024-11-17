using System;
using UnityEngine;

public class ParticlesByState : MonoBehaviour
{
    public ParticleSystem particles;
    public float particleRate = 20.0f;
    private Playermovement _playermovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var emission = particles.emission;
        emission.rateOverTime = 0;
        _playermovement = GetComponent<Playermovement>();
        particles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = particles.emission;
        var shape = particles.shape;
        if (_playermovement.spriteRenderer.flipX)
        {
            shape.rotation = new Vector3(-169, -90, 0);
        }
        else
        {
            shape.rotation = new Vector3(-11, -90, 0);
        }
        if (_playermovement.CurrentState.GetType() == typeof(WalkingState))
        {
            emission.rateOverTime = particleRate;
        }
        else
        {
            emission.rateOverTime = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    private Animator _animator = null;
    private ParticleSystem _particles = null;
    private PlayerEnergyControl _playerEnergy = null;

    [SerializeField] private float _particleSpeed = 1;
    [SerializeField] private float _particleRate = 0;
    [SerializeField] private float _particlesInRadius = 0;
    [SerializeField] private float _particlesOutRadius = 0;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _particles = GetComponent<ParticleSystem>();
        _playerEnergy = GetComponent<PlayerEnergyControl>();
    }

    // Update is called once per frame
    void Update()
    {
        InputReader();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Fog"))
        {
            _animator.SetLayerWeight(_animator.GetLayerIndex("RedGlow"), 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fog"))
        {
            _animator.SetLayerWeight(_animator.GetLayerIndex("RedGlow"), 0);
        }
    }

    void InputReader()
    {
        float _input = Input.GetAxis("EnergyControl");
        var main = _particles.main;
        var shape = _particles.shape;
        var emission = _particles.emission;
        if (_input != 0 && _playerEnergy.GetEnergy() > 0)
        {
            main.startSpeed = _input > 0 ? _particleSpeed : -_particleSpeed;
            shape.radius = _input > 0 ? _particlesOutRadius : _particlesInRadius;
            emission.rateOverTime = _particleRate;
        }
        else
        {
            emission.rateOverTime = 0;
        }
        
    }


}

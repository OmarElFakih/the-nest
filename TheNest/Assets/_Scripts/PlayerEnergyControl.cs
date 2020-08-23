﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyControl : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private float _startingHealth = 9;
    [SerializeField] private float _maxEnergy = 10;
    [SerializeField] private float _startingEnergy = 9;

    [SerializeField] private float _healthLossRate = 0;
    [SerializeField] private float _energyLossRate = 0;
    [SerializeField] private float _energyRestorePerFruit = 0;
    [SerializeField] private float _healthReastoreRate = 0;

    [SerializeField] private Image _healthBar = null;
    [SerializeField] private Image _energyBar = null;

    private bool _inRangeOfNest = false;

    private float _currentHealth = 9;
    private float _currentEnergy = 9;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _startingHealth;
        _currentEnergy = _startingEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        HealthControl();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Nest"))
        {
            if (Input.GetAxis("EnergyControl") > 0 && _currentEnergy > 0)
            {
                other.GetComponent<NestControl>().Feed();
                _currentEnergy -= _energyLossRate * Time.deltaTime;
                if (_currentEnergy < 0)
                {
                    _currentEnergy = 0;
                }
            }
            else
            {
                other.GetComponent<NestControl>().UnFeed();
            }
        }


        if (other.CompareTag("Fog"))
        {
            _currentHealth -= _healthLossRate * Time.deltaTime;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            _currentEnergy += _energyRestorePerFruit;
            if (_currentEnergy > _maxEnergy)
            {
                _currentEnergy = _maxEnergy;
            }
        }
    }

    void HealthControl()
    {
        if (Input.GetAxis("EnergyControl") < 0 && _currentEnergy > 0 && _currentHealth < _maxHealth)
        {
            _currentHealth += _healthReastoreRate * Time.deltaTime;
            _currentEnergy -= _energyLossRate * Time.deltaTime;
        }

        if (_healthBar != null)
        {
            _healthBar.fillAmount = _currentHealth / _maxHealth;
        }

        if (_energyBar != null)
        {
            _energyBar.fillAmount = _currentEnergy / _maxEnergy;
        }
    }

    


}
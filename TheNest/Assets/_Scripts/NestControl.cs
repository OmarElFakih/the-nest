using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NestControl : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private float _initialHealth = 9;
    private float _currentHealth = 9;
    [SerializeField] private float _healthLossRate = 0;
    [SerializeField] private float _starveDelay = 0;
    [SerializeField] private float _healthIncreaseRate = 0;
    [SerializeField] private float _secondsToHatch = 100;

    [SerializeField] private Image _healthBar = null;

    private bool _hasHatched = false;
    [SerializeField] private bool _isHurting = false;
    [SerializeField] private bool _isFeeding = false;
    [SerializeField] private bool _isVulnerable = false;


    private IEnumerator _hatchRoutine;
    private bool _dyingDelayIsRunning = false;



    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _initialHealth;
        //BeginCountDown();
    }

    // Update is called once per frame
    void Update()
    {
        HealthControl();
    }

    void Die()
    {
        Debug.Log("The Nest Died");
        StopCoroutine(_hatchRoutine);
    }

    void Hatch()
    {
        _hasHatched = true;
        Debug.Log("The eggs have hatched");
    }

    IEnumerator HatchCountDown()
    {
        //camera transition
        yield return new WaitForSeconds(_starveDelay);
        _isHurting = true;
        yield return new WaitForSeconds(_secondsToHatch);
        Hatch();
    }

    IEnumerator DyingDelay()
    {
        _dyingDelayIsRunning = true;
        yield return new WaitForSeconds(_starveDelay);
        _isHurting = true;
        _dyingDelayIsRunning = false;
    }


    void HealthControl()
    {
       
        if (!_hasHatched && _isHurting && _isVulnerable)
        {
            _currentHealth -= _healthLossRate * Time.deltaTime;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }
        }

        else if (!_isHurting && _isFeeding)
        {
            _currentHealth += _healthIncreaseRate * Time.deltaTime;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }

        if (_healthBar != null)
        {
            _healthBar.fillAmount = _currentHealth / _maxHealth;
        }
    }

    public void Feed()
    {
        _isFeeding = true;
        _isHurting = false;
        _isVulnerable = false;

    }

    public void UnFeed()
    {
        _isFeeding = false;
        if (!_dyingDelayIsRunning)
        {
            StartCoroutine(DyingDelay());
        }
    }

    public void BeginCountDown()
    {
        _isHurting = true;
        _isVulnerable = true;
        _hatchRoutine = HatchCountDown();
        StartCoroutine(_hatchRoutine);
    }

    
}

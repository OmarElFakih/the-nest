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
    [SerializeField] private bool _canHurt = false;
    public static bool NestIsDead = false;


    private IEnumerator _hatchRoutine;
    private bool _dyingDelayIsRunning = false;


    private void OnEnable()
    {
        GameManager.StartTheGame += BeginCountDown;
    }

    private void OnDisable()
    {
        GameManager.StartTheGame -= BeginCountDown;
    }



    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _initialHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthControl();
    }

    void Die()
    {
        Debug.Log("The Nest Died");
        GameManager.Instance.GameEnd();
        NestIsDead = true;
        var emission = GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = 0;
        StartCoroutine(DeathAnimation());
        StopCoroutine(_hatchRoutine);
        

    }

    IEnumerator DeathAnimation()
    {
        Animator _animator = GetComponent<Animator>();
        _animator.speed = 4;
        yield return new WaitForSeconds(2);
        _animator.SetTrigger("Dead");

    }

    void Hatch()
    {
        _hasHatched = true;
        GetComponent<Animator>().SetBool("isHurting", false);
        var emission = GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = 0;
        GameManager.Instance.GameEnd();
        Debug.Log("The eggs have hatched");
    }

    IEnumerator HatchCountDown()
    {
        //camera transition
        yield return new WaitForSeconds(_starveDelay);
        _isHurting = true;
        _canHurt = true;
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
       
        if (!_hasHatched && _isHurting && _canHurt && !PlayerEnergyControl.playerIsDead)
        {
            _currentHealth -= _healthLossRate * Time.deltaTime;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }
        }

        else if (!_isHurting && _isFeeding && !NestIsDead)
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
        _canHurt = true;
        GetComponent<Animator>().SetBool("isHurting", true);
        _hatchRoutine = HatchCountDown();
        StartCoroutine(_hatchRoutine);
    }

    
}

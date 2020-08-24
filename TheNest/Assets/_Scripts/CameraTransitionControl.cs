using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionControl : MonoBehaviour
{
    [SerializeField] private float _initialNestFocusDuration = 0;


    private Animator _animator = null;


    private void OnEnable()
    {
        GameManager.StartTheGame += InitialBlend;
        GameManager.EndTheGame += FinalBlend;
    }

    private void OnDisable()
    {
        GameManager.StartTheGame -= InitialBlend;
        GameManager.EndTheGame -= FinalBlend;
    }



    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitialBlend()
    {
        StartCoroutine(BlendRoutine());
    }

    IEnumerator BlendRoutine()
    {
        _animator.SetTrigger("ToNest");
        yield return new WaitForSeconds(_initialNestFocusDuration);
        _animator.SetTrigger("ToPlayer");
    }

    public void FinalBlend()
    {
        if (!PlayerEnergyControl.playerIsDead)
        {
            _animator.SetTrigger("ToNest");
        }
    }

}

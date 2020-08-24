using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogControl : MonoBehaviour
{
    [SerializeField] private float _fogTimeDelay = 0;

    private Collider2D _collider = null;

    private void OnEnable()
    {
        GameManager.StartTheGame += FogStart;
        GameManager.EndTheGame += FogEnd;
    }

    private void OnDisable()
    {
        GameManager.StartTheGame -= FogStart;
        GameManager.EndTheGame -= FogEnd;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FogStart()
    {
        StartCoroutine(FogDelay());
    }

    public void FogEnd()
    {
        _collider.enabled = false;
        GetComponent<Animator>().SetTrigger("GameEnd");
    }

    IEnumerator FogDelay()
    {
        yield return new WaitForSeconds(_fogTimeDelay);
        _collider.enabled = true;
        GetComponent<Animator>().SetTrigger("EnterFog");
    }




}

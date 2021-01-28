using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    [SerializeField] private float _initialSpeed = 0;
    [SerializeField] private float _lerpT = 0;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0f, _initialSpeed, 0f);
        transform.Translate(movement * Time.deltaTime);
        _initialSpeed = Mathf.Lerp(_initialSpeed, 0, _lerpT);
    }
}

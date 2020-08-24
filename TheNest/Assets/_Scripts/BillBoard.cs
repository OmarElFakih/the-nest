using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Animator _animator = null;
    [SerializeField] private KeyCode _keyToFade = KeyCode.A;
    [SerializeField] private bool _fadesOut = true;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyToFade) && _fadesOut)
        {
            _animator.SetTrigger("FadeOut");
        }
    }


}

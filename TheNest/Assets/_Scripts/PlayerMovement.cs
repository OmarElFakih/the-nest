using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier = 1;

    private bool _isFacingRight = true;
    private Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FlipControl();
    }

    void Movement()
    {
        float _xInput = Input.GetAxis("Horizontal");
        float _yInput = Input.GetAxis("Vertical");

        Vector3 _movementVector = new Vector3(_xInput, _yInput, 0f) * _speedMultiplier;

        _rigidBody.AddForce(_movementVector);

    }


    void Flip()
    {
        float _currentWidth = transform.localScale.x;
        Vector3 _newScale = new Vector3(_currentWidth * -1, transform.localScale.y, transform.localScale.z);
        transform.localScale = _newScale;

    }

    void FlipControl()
    {
        if (_isFacingRight && Input.GetAxis("Horizontal") < 0)
        {
            Flip();
            _isFacingRight = false;
        }
        else if (!_isFacingRight && Input.GetAxis("Horizontal") > 0)
        {
            Flip();
            _isFacingRight = true;
        }
    }

}

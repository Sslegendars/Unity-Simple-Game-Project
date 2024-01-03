using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce = 10f;

    Rigidbody2D _rb;
    bool isKinematic = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if(isKinematic)
                {
                    _rb.isKinematic = false;
                    isKinematic = false;
                }
                Jump();
            }
        }
    }
    void Jump()
    {
        _rb.velocity = Vector2.up * _jumpForce;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBounce : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 10f;
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

   public void Bounce()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}

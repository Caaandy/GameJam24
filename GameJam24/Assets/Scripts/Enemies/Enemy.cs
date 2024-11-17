using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 5;
    public float damage = 10;
    public float speed = 5;
    
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    
    private LayerMask _layerMask = ~((1 << 2) + (1<< 6) + (1 << 7));
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        Debug.Log(CheckForCliff());
        if (CheckForCliff())
        {
            _sr.flipX = !_sr.flipX;
        }
        Walk();
    }
    
    private void Walk()
    {
        if (_sr.flipX)
        {
            _rb.linearVelocityX = -speed;
        }
        else
        {
            _rb.linearVelocityX = speed;
        }

    }
    
    private bool CheckForCliff()
    {
        if (_sr.flipX)
        {
            RaycastHit2D hit = Physics2D.Raycast(_sr.bounds.max + Vector3.left * _sr.size.x, Vector2.down,  _sr.size.y + 0.05f, _layerMask);
            return hit.collider == null;
        } else
        {
            RaycastHit2D hit = Physics2D.Raycast(_sr.bounds.max, Vector2.down,  _sr.size.y + 0.05f, _layerMask);
            return hit.collider == null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            health -= other.GetComponent<Attack>().damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

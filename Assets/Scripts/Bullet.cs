using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour , IpooledObject
{
    

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _shootForce;
    [SerializeField] private GameObject _explosion;

    public void OnObjectSpawn()
    {
        _rigidbody.AddForce(transform.right * _shootForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
    }
}

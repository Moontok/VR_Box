using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class WandProjectile : MonoBehaviour
{
    [SerializeField] int speed = 200;
    [SerializeField] float lifeTime = 5.0f;

    private Rigidbody rb = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
        Destroy(gameObject, lifeTime);
    }
}

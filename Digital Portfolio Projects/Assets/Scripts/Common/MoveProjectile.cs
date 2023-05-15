using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Space space = Space.Self;
    Vector3 direction;

    void Start()
    {
        direction = transform.forward;
    }

    void Update()
    {
        this.gameObject.transform.Translate(speed * Time.deltaTime * direction, space);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}

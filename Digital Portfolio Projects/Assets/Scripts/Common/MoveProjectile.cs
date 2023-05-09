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
        Debug.Log("Projectile direction: " +  direction);
    }

    void Update()
    {
        this.gameObject.transform.Translate(speed * Time.deltaTime * direction, space);
    }
}

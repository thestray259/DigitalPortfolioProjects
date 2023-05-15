using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] float timer; 

    void Start()
    {
        Destroy(gameObject, timer);
    }
}

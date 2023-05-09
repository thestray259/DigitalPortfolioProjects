using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] string tag;
    [SerializeField] float damage = 0;
    [SerializeField] bool oneTime = true; 

    private void OnTriggerEnter(Collider other)
    {
        if (!oneTime) return; 

        if (other.CompareTag(tag))
        {
            if (other.gameObject.TryGetComponent<Health>(out Health health))
            {
                health.Damage(damage);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (oneTime) return; 

        if (other.CompareTag(tag))
        {
            if (other.gameObject.TryGetComponent<Health>(out Health health))
            {
                health.Damage(damage * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!oneTime) return; 

        if (other.gameObject.CompareTag(tag))
        {
            if (other.gameObject.TryGetComponent<Health>(out Health health))
            {
                health.Damage(damage);
            }
        }
    }
}

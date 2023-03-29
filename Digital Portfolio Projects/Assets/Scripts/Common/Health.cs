using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject deathPrefab; // death animation
    [SerializeField] int maxHealth = 100; 
    [SerializeField] bool destroyOnDeath = true;
    [SerializeField] bool destroyRoot = false;

    public float health;
    public bool isDead = false;

    Animator animator; 

    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        if (!isDead && health <= 0)
        {
            isDead = true;
            Debug.Log("Dead");

            if (TryGetComponent<IDestructable>(out IDestructable destructable))
            {
                destructable.Destroyed();
            }

            if (deathPrefab != null)
            {
                Instantiate(deathPrefab, transform.position, transform.rotation);
            }

            if (destroyOnDeath)
            {
                animator.SetTrigger("dead");
                if (destroyRoot) Destroy(gameObject.transform.root.gameObject);
                //else Destroy(gameObject, 3.5f); 
                else gameObject.SetActive(false); // actually disables it, not destroys it 
            }
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (!isDead && health <= 0)
        {
            isDead = true;
            if (TryGetComponent<IDestructable>(out IDestructable destructable))
            {
                destructable.Destroyed();
            }

            if (deathPrefab != null)
            {
                Instantiate(deathPrefab, transform.position, transform.rotation);
            }

            if (destroyOnDeath)
            {
                if (destroyRoot) Destroy(gameObject.transform.root.gameObject);
                else Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject deathPrefab; // death animation
    [SerializeField] bool destroyOnDeath = true;
    [SerializeField] bool destroyRoot = false;
    public int maxHealth = 100; 

    public float health;
    public bool isDead = false;

    Animator animator;

    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void Damage(float damage)
    {
        Debug.Log("ouch");
        health -= damage;
        animator.SetTrigger("hit");
        DeathCheck();
    }

    public void DeathCheck()
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
                GameObject death = Instantiate(deathPrefab, transform.position, transform.rotation);
                Destroy(death, 3.5f);
            }

            if (destroyOnDeath)
            {
                animator.SetTrigger("dead");
                if (destroyRoot) Destroy(gameObject.transform.root.gameObject);
                //else Destroy(gameObject, 3.5f); 
                else gameObject.SetActive(false); // actually disables it, not destroys it 

                transform.position = new Vector3(-100, -100, -100);
            }
        }
    }
}

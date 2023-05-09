using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform transform;
    GameObject proj;

    public Projectile(GameObject projectile, Transform transform)
    {
        this.projectile = projectile;
        this.transform = transform;
    }

    public void ShootProjectile()
    {
        proj = Instantiate(projectile, transform.position, transform.rotation);
    }
}

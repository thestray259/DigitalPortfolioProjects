using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform transform;
    GameObject proj;

    Vector3 position;
    Quaternion rotation;

    public Projectile(GameObject projectile, Transform transform)
    {
        this.projectile = projectile;
        this.transform = transform;
    }

    public Projectile(GameObject projectile, Vector3 position, Quaternion rotation)
    {
        this.projectile = projectile;
        this.position = position;
        this.rotation = rotation;
    }

    public void ShootProjectile()
    {
        //proj = Instantiate(projectile, transform.position, transform.rotation);
        proj = Instantiate(projectile, position, rotation);
    }
}

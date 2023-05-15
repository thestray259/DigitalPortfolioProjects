using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    Vector3 position;
    Quaternion rotation;

    public Projectile(GameObject projectile, Vector3 position, Quaternion rotation)
    {
        this.projectile = projectile;
        this.position = position;
        this.rotation = rotation;
    }

    public void ShootProjectile()
    {
        Instantiate(projectile, position, rotation);
    }
}

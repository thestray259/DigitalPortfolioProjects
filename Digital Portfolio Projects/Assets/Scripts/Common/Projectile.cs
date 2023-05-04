using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform transform;
    [SerializeField] Space space = Space.Self;
    [SerializeField] float speed;
    Vector3 direction;
    GameObject proj;

    public Projectile(GameObject projectile, Transform transform, Space space, float speed)
    {
        this.projectile = projectile;
        this.transform = transform;
        this.space = space;
        this.speed = speed;
    }

    private void Update()
    {
/*        if (proj != null)
        {
            proj.transform.Translate(speed * Time.deltaTime * direction, space);
        }*/
    }

    public void ShootProjectile()
    {
        proj = Instantiate(projectile, transform.position, transform.rotation);
        direction = transform.forward;
        //proj.transform.Translate(speed * Time.deltaTime * direction, space);
    }
}

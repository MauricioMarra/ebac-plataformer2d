using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float bulletSpeed = .5f;
    public float direction = 1;

    private void Update()
    {
        this.transform.Translate(direction * bulletSpeed * Time.deltaTime, 0, 0);
    }
}

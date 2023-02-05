using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float bulletSpeed = .5f;
    public float direction = 1;

    private float _lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        Debug.Log(direction);
        this.transform.Translate(direction * bulletSpeed * Time.deltaTime, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLimit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var c = collision.gameObject.GetComponent<ProjectileBase>();
        if (c != null)
            Destroy(collision.gameObject);
    }
}

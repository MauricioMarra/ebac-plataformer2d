using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    private string playerTag = "Player";
    private float _destroyDelay = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag)) OnCollect();
    }

    protected void Collect()
    {
        var collider = this.GetComponent<Collider2D>();

        if (collider != null) 
            collider.enabled = false;

        VFXManager.instance.PlayVfx(VfxType.Collect, this.transform.position);

        Destroy(gameObject, _destroyDelay);
    }

    protected virtual void OnCollect()
    {
        Collect();
    }
}

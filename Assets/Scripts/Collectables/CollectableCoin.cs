using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : CollectableBase
{
    private Animator _animator;
    private string _coinTrigger = "collect";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void OnCollect()
    {
        base.OnCollect();

        ItemManager.instance.AddCoin();

        _animator.SetTrigger(_coinTrigger);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateInterface : MonoBehaviour
{
    public TextMeshProUGUI collectableText;
    public SOCollectables soCollectable;

    private void Awake()
    {
        ItemManager.instance.OnChangeValues += OnUpdateInterface;
    }

    public void OnUpdateInterface()
    {
        collectableText.text = soCollectable.value.ToString();
    }
}

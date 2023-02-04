using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRendererList = new List<SpriteRenderer>();
    private float _flashTime = 0.05f;
    private Tween _currentTween = null;

    private void OnValidate()
    {
        foreach (var element in GetComponentsInChildren<SpriteRenderer>())
            spriteRendererList.Add(element);
    }

    public void FlashComponent()
    {
        if(_currentTween != null)
        {
            _currentTween.Kill();
            spriteRendererList.ForEach( sr => sr.color = Color.white );
        }

        foreach (var element in spriteRendererList)
        {
            _currentTween = element.DOColor(Color.red, _flashTime).SetLoops(10, LoopType.Yoyo);
        }
    }
}

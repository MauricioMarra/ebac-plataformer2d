using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRendererList = new List<SpriteRenderer>();
    private float _flashTime = 0.05f;
    private Tween _currentTween = null;
    private Coroutine _coroutineFlash;

    private void OnValidate()
    {
        spriteRendererList.Clear();

        foreach (var element in GetComponentsInChildren<SpriteRenderer>())
            spriteRendererList.Add(element);
    }

    private void FlashComponent()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
            spriteRendererList.ForEach(sr => sr.color = Color.white);
        }

        foreach (var element in spriteRendererList)
        {
            _currentTween = element.DOColor(Color.red, _flashTime).SetLoops(10, LoopType.Yoyo);
        }
    }

    private IEnumerator FlashComponentCoroutine()
    {
        FlashComponent();

        yield return new WaitForSeconds(_flashTime);

        _currentTween.onComplete += FlashBackToNormal;
        _coroutineFlash = null;
    }

    public void OnFlashComponent()
    {
        if (_coroutineFlash == null)
        {
            _coroutineFlash = StartCoroutine(FlashComponentCoroutine());
        }
    }

    private void FlashBackToNormal()
    {
        spriteRendererList.ForEach(sr => sr.color = Color.white);
    }
}

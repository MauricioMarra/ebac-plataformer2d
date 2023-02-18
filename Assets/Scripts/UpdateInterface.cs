using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpdateInterface : MonoBehaviour
{
    public TextMeshProUGUI collectableText;
    public SOCollectables soCollectable;
    public UnityEvent unityEvent;

    private void Awake()
    {
        ItemManager.instance.OnChangeValues += OnUpdateInterface;

        if (unityEvent == null)
            unityEvent = new UnityEvent();

        unityEvent.AddListener(OnUpdateEnemyCount);
    }

    public void OnUpdateInterface()
    {
        this.collectableText.text = this.soCollectable.value.ToString();
    }

    public void OnUpdateEnemyCount()
    {
        this.collectableText.text = this.soCollectable.value.ToString();
    }
}

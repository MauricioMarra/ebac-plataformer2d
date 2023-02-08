using System;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int initialHealth;
    public Action OnKill;

    private int _currentHealth;
    private Flash _flashComponent;

    private void Awake()
    {
        _currentHealth = initialHealth;

        _flashComponent= GetComponent<Flash>();
    }

    public void Damage(int damage = 1)
    {
        this._currentHealth -= damage;

        if (_currentHealth <= 0)
            Kill();

        if (_flashComponent != null)
            _flashComponent.OnFlashComponent();
    }

    private void Kill()
    {
        var player = GetComponent<Player>();

        if (player != null)
        {
            //player.Respawn();
            //_currentHealth = initialHealth;
            OnKill.Invoke();
        }
        else
        {
            //Destroy(gameObject);
            OnKill.Invoke();
        }
    }
}

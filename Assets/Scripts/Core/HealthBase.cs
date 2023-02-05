using System;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int initialHealth;
    public Action OnKill;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = initialHealth;
    }

    public void Damage(int damage = 1)
    {
        this._currentHealth -= damage;

        if (_currentHealth <= 0)
            Kill();
    }

    private void Kill()
    {
        var player = GetComponent<Player>();

        if (player != null)
        {
            player.Respawn();
            _currentHealth = initialHealth;
        }
        else
        {
            //Destroy(gameObject);
            OnKill.Invoke();
        }
    }
}

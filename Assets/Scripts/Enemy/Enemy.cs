using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var healthComponent = collision.gameObject.GetComponent<HealthBase>();

        if (healthComponent != null)
        {
            healthComponent.Damage(this.damage);
            Debug.Log($"{this.damage} point of damage to {collision.gameObject}");
        }

    }
}

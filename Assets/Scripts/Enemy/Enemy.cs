using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public HealthBase health;
    public Animator animator;
    public float pushBackForce;

    private string _animatorTriggerForDeath = "Death";

    private void Awake()
    {
        health.OnKill += OnEnemyKill;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var healthComponent = collision.gameObject.GetComponent<HealthBase>();

        if (healthComponent != null)
            healthComponent.Damage(this.damage);
    }

    void OnEnemyKill()
    {
        health.OnKill -= OnEnemyKill;

        var collider = this.GetComponentInChildren<Collider2D>();

        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

        if (collider != null)
            collider.enabled = false;

        animator.SetTrigger(_animatorTriggerForDeath);
    }
}

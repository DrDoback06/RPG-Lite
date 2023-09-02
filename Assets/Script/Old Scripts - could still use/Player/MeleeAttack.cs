using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackDuration = 0.5f;
    private Collider2D attackCollider;

    private void Start()
    {
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = false;
    }

    public void Attack()
    {
        attackCollider.enabled = true;
        Invoke(nameof(DisableAttackCollider), attackDuration);
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}

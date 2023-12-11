public interface IDamageable
{
    void Damage(float damageAmount);
    void Die();

    float maxHealth { get; set; }
    float currentHealth { get; set; }
}

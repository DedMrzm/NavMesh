public interface IDamagable
{
    bool IsAlive { get; }   

    void TakeDamage(float damage);
}

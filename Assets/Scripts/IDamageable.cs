using UnityEngine;

public interface IDamageable
{
    public int Health { get; set; }
    public int DamageAmount { get; set; }
    public void GetDamage(int amount);
    public void Dying();
    public void CheckHealth();
}

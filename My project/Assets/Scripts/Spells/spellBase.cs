using UnityEngine;

public abstract class spellBase : MonoBehaviour
{
    public float cooldown = 1f;
    public float lifetime = 3f;

    public interface IDamageable
    {
        int Damage { get; set; }
    }
    public abstract void Cast(Vector3 dir);
}

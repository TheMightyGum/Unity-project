using UnityEngine;
using UnityEngine.AI;

public class enemyBase : MonoBehaviour
{
    public float health;
    public float damage;
    public float attackCooldown;
    public float range;
    public GameObject purityEffect;
    public float purityEffectDuration = 5f;
    private NavMeshAgent agent;
    float cooldown;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (cooldown > attackCooldown)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= range)
            {
                Attack(player);
                cooldown = 0f;
            }
            else
            {
                agent.SetDestination(player.transform.position);
            }
        }
        cooldown += Time.deltaTime;
    }

    public virtual void Attack(GameObject player)
    {
        player.GetComponent<playerHealth>().TakeDamage(damage);
    }

    public virtual void TakeDamage(float dmg)
    {
        GameObject splatter = Instantiate(purityEffect, transform.position, transform.rotation);
        Destroy(splatter, purityEffectDuration);
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}

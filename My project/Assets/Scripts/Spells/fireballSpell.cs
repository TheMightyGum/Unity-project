using UnityEngine;

public class fireballSpell : spellBase {

    Rigidbody rb;
    public ParticleSystem flameADD;
    public ParticleSystem flameALPHA;
    public float speed = 1f;
    public int damage { get; set; } = 1;
    public float AOErange = 2f;
    public GameObject explosionEffect;
    public LayerMask mask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cooldown = 2f;
        ParticleSystem.MainModule particle = flameADD.main;
        particle.simulationSpace = ParticleSystemSimulationSpace.Local;
        particle = flameALPHA.main;
        particle.simulationSpace = ParticleSystemSimulationSpace.Local;
    }
    public override void Cast(Vector3 dir)
    {
        flameADD.Clear();
        flameALPHA.Clear();
        ParticleSystem.MainModule partice = flameADD.main;
        partice.simulationSpace = ParticleSystemSimulationSpace.World;
        partice = flameALPHA.main;
        partice.simulationSpace = ParticleSystemSimulationSpace.World;
        rb.linearVelocity = dir * speed;
        Destroy(gameObject, lifetime);
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rb.linearVelocity, out hit, 1f))
        {
            if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Wall"))
            {
                GameObject explosion = Instantiate(explosionEffect, rb.position, rb.rotation);
                Destroy(explosion, 1.5f);
                Destroy(gameObject);

                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<enemyBase>().TakeDamage(damage);
                }

                Collider[] colliders = Physics.OverlapSphere(transform.position, AOErange);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Enemy") && collider.gameObject != hit.collider.gameObject)
                    {
                        collider.GetComponent<enemyBase>().TakeDamage(Mathf.Clamp01(1 - Mathf.Pow(Vector3.Distance(collider.transform.position, transform.position) / AOErange, 2)) * damage);
                    }
                }
            }
        }
    }
}

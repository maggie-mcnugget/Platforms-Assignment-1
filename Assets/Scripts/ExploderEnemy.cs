using UnityEngine;
using UnityEngine.AI;

public class ExploderEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 20f;

    [Header("Explosion")]
    public float explodeRange = 2f;
    public int explodeDamage = 25;

    private Transform player;
    private NavMeshAgent agent;

    private bool hasExploded = false;

    private Animator animator;
    private bool isPreparingToExplode = false;
    private bool hasTriggeredExplosion = false;

    public int health = 40;

    [Header("Audio")]
    public AudioSource explodeAudio;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed;

        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
        {
            player = obj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
        }

        if (distance <= explodeRange && !hasExploded && !isPreparingToExplode)
        {
            StartCoroutine(PrepareExplosion());
        }
    }

    void Explode()
    {
        if (hasExploded) return;

        hasExploded = true;

        if (explodeAudio != null)
        {
            explodeAudio.Play();
        }

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= explodeRange)
            {
                PlayerHealth ph = player.GetComponent<PlayerHealth>();

                if (ph != null)
                {
                    ph.TakeDamage(explodeDamage, "Explosion");
                }
            }
        }

        Destroy(gameObject);
    }
    System.Collections.IEnumerator PrepareExplosion()
    {
        isPreparingToExplode = true;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        if (animator != null)
        {
            animator.SetTrigger("ExplodePrep");
        }

        yield return new WaitForSeconds(0.6f);

        Explode();
    }

    public void TakeDamage(int damage)
    {
        if (hasExploded) return;

        health -= damage;

        if (health <= 0)
        {
            Explode();
        }
    }
}

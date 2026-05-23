using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public float health = 100f;

    private Transform player;
    private NavMeshAgent agent;

    private Animator animator;

    [Header("Patrol / Zone")]
    public Transform guardPoint;
    public float patrolRadius = 3f;
    public float detectionRange = 10f;
    public float returnSpeedThreshold = 0.1f;

    private Vector3 currentTarget;
    private bool chasingPlayer;

    [Header("Combat")]
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;

    private float attackTimer;
    private bool isAttacking;

    private PlayerHealth playerHealth;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");

            if (obj != null)
            {
                player = obj.transform;
                playerHealth = obj.GetComponentInChildren<PlayerHealth>();
            }

            Patrol();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            chasingPlayer = true;
        }
        else if (distanceToPlayer > detectionRange * 1.5f)
        {
            chasingPlayer = false;
        }

        if (!chasingPlayer)
        {
            Patrol();
            return;
        }


        attackTimer -= Time.deltaTime;

        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;
            animator.SetBool("Moving", false);

            TryAttack();
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);

        animator.SetBool("Moving", true);
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }

    public void TakeDamage(float damage, string hitType)
    {
        Debug.Log($"{gameObject.name} was hit by {hitType} for {damage}");

        if (animator != null)
        {
            if (hitType == "Punch")
                animator.SetTrigger("HitPunch");
            else if (hitType == "Kick")
                animator.SetTrigger("HitKick");
            else if (hitType == "Shoot")
                animator.SetTrigger("HitShoot");
        }

        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Patrol()
    {
        agent.isStopped = true;

        animator.SetBool("Moving", false);
    }

    void TryAttack()
    {
        if (attackTimer > 0f) return;

        attackTimer = attackCooldown;

        bool isPunch = Random.Range(0, 2) == 0;

        animator.SetTrigger(isPunch ? "Punch" : "Kick");

        PlayerHealth ph = player.GetComponent<PlayerHealth>();

        if (ph != null)
        {
            Debug.Log("Player found, dealing damage");

            if (isPunch)
                ph.TakeDamage(5, "Punch");
            else
                ph.TakeDamage(10, "Kick");
        }
        else
        {
            Debug.Log("PlayerHealth NOT found");
        }
    }

}

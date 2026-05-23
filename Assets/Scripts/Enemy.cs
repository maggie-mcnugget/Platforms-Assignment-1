using UnityEngine;
using UnityEngine.AI;

public class SimpleFollowAI : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootRange = 10f;
    public float shootCooldown = 2f;
    public float projectileForce = 15f;

    private float shootTimer;

    [Header("Look Visibility")]
    public float lookHideTime = 2f;

    private float lookTimer = 0f;
    private Renderer[] renderers;
    private bool isHidden = false;

    private Transform player;
    private NavMeshAgent agent;
    private Transform cam;

    private Animator animator;

    public float startDelay = 10f;
    private float startTimer;
    private bool canAct = false;

    [Header("Audio")]
    public AudioSource walkAudio;

    bool IsBeingLookedAt()
    {
        if (cam == null) return false;

        Vector3 dirToEnemy = (transform.position - cam.position).normalized;

        float dot = Vector3.Dot(cam.forward, dirToEnemy);

        bool inViewAngle = dot > 0.6f;

        return inViewAngle;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        shootTimer = 0f;

        renderers = GetComponentsInChildren<Renderer>();

        animator = GetComponentInChildren<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            SetTarget(playerObj.transform);
        }

        startTimer = startDelay;
        canAct = false;
    }


    void Update()
    {
        if (!canAct)
        {
            startTimer -= Time.deltaTime;

            if (startTimer > 0f)
                return;

            canAct = true;
        }

        if (player == null) return;

        bool beingLookedAt = IsBeingLookedAt();

        if (beingLookedAt)
        {
            lookTimer += Time.deltaTime;

            if (lookTimer >= lookHideTime && !isHidden)
            {
                SetVisible(false);
                isHidden = true;
                walkAudio.Stop();
            }

            agent.isStopped = true;

            if (animator != null)
                animator.SetBool("Moving", false);

            return;
        }
        else
        {
            lookTimer = 0f;

            if (isHidden)
            {
                SetVisible(true);
                isHidden = false;
            }
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);

        bool isMoving = agent.velocity.magnitude > 0.1f;
        if (!isMoving)
        {
            if (walkAudio.isPlaying)
                walkAudio.Stop();
        }
        else
        {
            if (!walkAudio.isPlaying)
                walkAudio.Play();
        }

        if (animator != null)
            animator.SetBool("Moving", true);

        shootTimer -= Time.deltaTime;

        if (IsInShootRange() && shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    public void SetTarget(Transform target)
    {
        player = target;

        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        if (animator != null)
            animator.SetTrigger("Shoot");

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = (player.position - firePoint.position).normalized;
            rb.linearVelocity = dir * projectileForce;
        }
    }

    bool IsInShootRange()
    {
        if (player == null) return false;

        float dist = Vector3.Distance(transform.position, player.position);
        return dist <= shootRange;
    }

    void SetVisible(bool visible)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = visible;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
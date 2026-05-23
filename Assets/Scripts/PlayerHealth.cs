using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    private PlayerRespawn respawn;
    private Animator animator;

    private PlayerMovement movement;

    void Awake()
    {
        respawn = GetComponent<PlayerRespawn>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage, string hitType)
    {
        Debug.Log("Player took damage");

        if (health <= 0) return;

        health -= damage;

        if (health <= 0)
        {
            Die();
            return;
        }

        if (animator != null)
        {
            if (hitType == "Punch")
                animator.SetTrigger("HitPunch");

            else if (hitType == "Kick")
                animator.SetTrigger("HitKick");

            else if (hitType == "Explosion")
                animator.SetTrigger("HitExplosion");
        }

    }

    void Die()
    {
        if (movement != null)
        {
            movement.canMove = false;
        }

        if (animator != null)
            animator.SetTrigger("Die");

        Invoke(nameof(RestartGame), 2f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

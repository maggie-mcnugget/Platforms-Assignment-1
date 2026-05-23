using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(100, "Projectile");
            }

            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float lifeTime = 3f;
    public float damage = 20f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        MeleeEnemy melee = collision.gameObject.GetComponentInParent<MeleeEnemy>();

        if (melee != null)
        {
            melee.TakeDamage(damage, "Shoot");
        }

        SimpleFollowAI simple = collision.gameObject.GetComponentInParent<SimpleFollowAI>();

        if (simple != null)
        {
            simple.Die();
        }

        ExploderEnemy exploder = collision.gameObject.GetComponentInParent<ExploderEnemy>();

        if (exploder != null)
        {
            exploder.TakeDamage((int)damage);
        }

        Destroy(gameObject);
    }
}

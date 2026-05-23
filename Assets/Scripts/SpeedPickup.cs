using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public float speedBoostMultiplier = 1.5f;
    public float duration = 10f;

    public GameObject crystalEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (crystalEffect != null)
        {
            Instantiate(crystalEffect, transform.position, Quaternion.identity);
        }
        PlayerMovement player = other.transform.root.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.StartCoroutine(
                player.SpeedBoost(speedBoostMultiplier, duration)
            );
        }
    }
}

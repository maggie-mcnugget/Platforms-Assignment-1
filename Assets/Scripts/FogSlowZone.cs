using UnityEngine;

public class FogSlowZone : MonoBehaviour
{
    public float speedMultiplier = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.transform.root.GetComponent<PlayerMovement>();

        if (player != null)
        {
            Debug.Log("Fog entered");
            player.speedMultiplier = 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement player = other.transform.root.GetComponent<PlayerMovement>();

        if (player != null)
        {
            Debug.Log("Fog exited");
            player.speedMultiplier = 1f;
        }
    }
}

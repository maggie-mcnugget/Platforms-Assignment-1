using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Floating")]
    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;

    [Header("Rotation")]
    public float rotationSpeed = 50f;

    [Header("Audio")]
    public AudioSource pickupAudio;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Floating motion
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            startPos.x,
            newY,
            startPos.z
        );

        // Rotation
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickupAudio != null)
            {
                pickupAudio.Play();

                // destroy after sound finishes
                Destroy(gameObject, pickupAudio.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

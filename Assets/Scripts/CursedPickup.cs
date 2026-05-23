using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CursedPickup : MonoBehaviour
{
    [TextArea]
    public string deathMessage = "Who picks up Glowing rocks in a forest? You died.";

    public float restartDelay = 5f;

    public GameObject messageUI;
    public TextMeshProUGUI messageText;

    private bool triggered = false;

    public GameObject bloodEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (bloodEffect != null)
            {
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
            }

            PlayerHealth ph = other.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.TakeDamage(9999, "Trap");
            }

            ShowMessage();

            Invoke(nameof(RestartScene), restartDelay);
        }
    }

    void ShowMessage()
    {
        if (messageUI != null)
            messageUI.SetActive(true);

        if (messageText != null)
            messageText.text = deathMessage;
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

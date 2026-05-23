using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider healthSlider;

    void Update()
    {
        if (playerHealth != null)
        {
            healthSlider.value = playerHealth.health;
        }

        Debug.Log(playerHealth.health);
    }
}
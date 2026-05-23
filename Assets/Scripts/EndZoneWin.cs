using UnityEngine;
using TMPro;

public class EndZoneWin : MonoBehaviour
{
    public GameObject winUI;
    public TextMeshProUGUI winText;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            ShowWinMessage();
        }
    }

    void ShowWinMessage()
    {
        if (winUI != null)
            winUI.SetActive(true);

        if (winText != null)
            winText.text = "You made it to the end alive, you win!";
    }
}
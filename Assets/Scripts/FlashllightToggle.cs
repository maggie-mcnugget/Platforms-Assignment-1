using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlight;

    void Start()
    {
        flashlight.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}

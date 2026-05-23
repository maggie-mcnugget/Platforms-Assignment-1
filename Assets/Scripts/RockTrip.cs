using UnityEngine;
using System.Collections;

public class TripHazard : MonoBehaviour
{
    public float tripTime = 1.5f;
    private bool hasTripped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTripped) return;

        if (other.CompareTag("Player"))
        {
            hasTripped = true;

            Animator animator = other.GetComponentInChildren<Animator>();
            PlayerMovement movement = other.GetComponent<PlayerMovement>();

            if (animator != null)
            {
                animator.SetTrigger("Trip");
            }

            if (movement != null)
            {
                StartCoroutine(TripPlayer(movement));
            }
        }
    }

    IEnumerator TripPlayer(PlayerMovement movement)
    {
        CharacterController cc = movement.GetComponent<CharacterController>();
        Animator animator = movement.GetComponentInChildren<Animator>();

        movement.canMove = false;

        if (cc != null)
            cc.enabled = false;

        animator.SetTrigger("Trip");

        yield return new WaitForSeconds(tripTime);

        animator.SetBool("isGettingUp", true);


        yield return new WaitForSeconds(7f);

        animator.SetBool("isGettingUp", false);

        if (cc != null)
            cc.enabled = true;

        movement.canMove = true;
    }
}

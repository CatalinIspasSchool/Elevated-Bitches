using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
   public AudioClip footstepSFX;
    private PlayerMovement movement;
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        StartCoroutine(PlayFootsteps());
    }
    IEnumerator PlayFootsteps()
    {
        while(true)
        {
            if (movement.moveDirection.magnitude > 0.1f)
            {
                AudioManager.instance.PlaySFX(footstepSFX);
            }
            yield return new WaitForSeconds(0.35f);
        }
    }
}

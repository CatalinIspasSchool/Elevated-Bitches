using UnityEngine;
using System.Collections;

public class JumpscareTrigger : MonoBehaviour
{
    [Header("Setări Jumpscare")]
    public GameObject jumpscareObject; 
    public float duration = 1.0f;      
    public bool destroyAfterUse = true; 

    [Header("Efecte Cameră")]
    public float shakeIntensity = 0.5f;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !hasPlayed)
        {
            StartCoroutine(ActivateJumpscare());
        }
    }

    IEnumerator ActivateJumpscare()
    {
        hasPlayed = true;
        
        
        if (jumpscareObject != null)
            jumpscareObject.SetActive(true);

        
        yield return new WaitForSeconds(duration);

        
        if (jumpscareObject != null)
            jumpscareObject.SetActive(false);

        
        if (destroyAfterUse)
            Destroy(gameObject);
    }
}
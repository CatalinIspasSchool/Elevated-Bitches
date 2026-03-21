using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 3f; 
    public LayerMask interactableLayer; 
    
    
    public Transform playerCamera; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformInteraction();
        }
    }

    void PerformInteraction()
    {
        
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            
            InteractiveObject interactiveObj = hit.collider.GetComponent<InteractiveObject>();

            if (interactiveObj != null)
            {
                
                interactiveObj.Interact();
            }
        }
    }
}
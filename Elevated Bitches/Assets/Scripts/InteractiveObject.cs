using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    
    public GameObject uiToActivate;
    public GameObject player;
    public UnityEvent onTrigger;
   

    
    public void Interact()
    {
        if (uiToActivate != null)
        {
            
            uiToActivate.SetActive(!uiToActivate.activeSelf);
            Debug.Log("Eh");
            player.GetComponentInChildren<CameraController>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;

			if (uiToActivate.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; 
                Cursor.visible = false;
            }
        }

        onTrigger.Invoke();
    }
}
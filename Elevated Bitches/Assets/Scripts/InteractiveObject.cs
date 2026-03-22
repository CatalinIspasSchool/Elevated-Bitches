using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    
    public GameObject uiToActivate; 
    public DialogueManager dialogueManager;
    public GameObject player;

    
    public void Interact()
    {

		player.GetComponentInChildren<CameraController>().enabled = !player.GetComponentInChildren<CameraController>().enabled;
		player.GetComponent<PlayerMovement>().enabled = !player.GetComponent<PlayerMovement>().enabled;

		if (dialogueManager != null)
        {
            
            dialogueManager.StartDialogue();
        }

        if (uiToActivate != null)
        {
            
            uiToActivate.SetActive(!uiToActivate.activeSelf);

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
    }
}
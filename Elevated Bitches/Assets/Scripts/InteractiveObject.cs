using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    
    public GameObject uiToActivate; 
    public DialogueManager dialogueManager;

    
    public void Interact()
    {

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
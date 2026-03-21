using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections; 

public class WireTaskSimple : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    
    public RectTransform panelRect;
    public GameObject wirePrefab; 
    public Transform wireParent; 
    public PlayerInput playerInput;

   
    public GameObject doorObject; 
    public float downDistance = 2.5f; 
    public float moveSpeed = 2f; 

    
    public MonoBehaviour playerMovement; 
    public MonoBehaviour cameraLook; 

    private GameObject currentStartPoint;
    private RectTransform currentWireRect;
    private Image currentWireImage;
    private Vector2 startPointPosition;
    private int successfulConnections = 0;
    private bool minigameCompleted = false; 

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMinigame();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (minigameCompleted) return; 

        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<Image>() != null)
        {
            if (eventData.pointerEnter.CompareTag("Untagged")) return;

            currentStartPoint = eventData.pointerEnter;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, currentStartPoint.transform.position, eventData.pressEventCamera, out startPointPosition);

            GameObject newWire = Instantiate(wirePrefab, wireParent);
            currentWireRect = newWire.GetComponent<RectTransform>();
            currentWireImage = newWire.GetComponent<Image>();

            currentWireImage.color = currentStartPoint.GetComponent<Image>().color;
            currentWireRect.localPosition = startPointPosition;
            
            UpdateWire(eventData.position, eventData.pressEventCamera);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentWireRect == null || minigameCompleted) return;
        UpdateWire(eventData.position, eventData.pressEventCamera);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentWireRect == null || minigameCompleted) return;

        bool success = false;

        if (eventData.pointerEnter != null)
        {
            GameObject endPoint = eventData.pointerEnter;
            string sName = currentStartPoint.name.ToLower();
            string eName = endPoint.name.ToLower();

            bool isStartLeft = sName.Contains("left");
            bool isEndRight = eName.Contains("right");
            bool isStartRight = sName.Contains("right");
            bool isEndLeft = eName.Contains("left");

            if ((isStartLeft && isEndRight) || (isStartRight && isEndLeft))
            {
                if (currentStartPoint.CompareTag(endPoint.tag)) 
                {
                    Vector2 startPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, currentStartPoint.transform.position, eventData.pressEventCamera, out startPos);
                    Vector2 endPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, endPoint.transform.position, eventData.pressEventCamera, out endPos);

                    PositionWireBetweenPoints(startPos, endPos);

                    currentStartPoint.GetComponent<Image>().raycastTarget = false;
                    endPoint.GetComponent<Image>().raycastTarget = false;

                    success = true;
                    successfulConnections++;
                    CheckWinCondition();
                }
            }
        }

        if (!success) Destroy(currentWireRect.gameObject);
        
        currentStartPoint = null;
        currentWireRect = null;
    }

    void UpdateWire(Vector2 screenMousePosition, Camera cam)
    {
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, screenMousePosition, cam, out localMousePos);
        PositionWireBetweenPoints(startPointPosition, localMousePos);
    }

    void PositionWireBetweenPoints(Vector2 start, Vector2 end)
    {
        Vector2 direction = end - start;
        float length = direction.magnitude;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        currentWireRect.sizeDelta = new Vector2(length, currentWireRect.sizeDelta.y);
        currentWireRect.localRotation = Quaternion.Euler(0, 0, angle);
    }

    void CheckWinCondition()
    {
        if (successfulConnections >= 4 && !minigameCompleted)
        {
            minigameCompleted = true; 
            
            
            
            if (doorObject != null)
            {
                StartCoroutine(MoveDoorDown());
            }

            
            Invoke("CloseMinigame", 0.5f);
        }
    }

    
    IEnumerator MoveDoorDown()
    {
        Vector3 startPosition = doorObject.transform.position;
        
        Vector3 targetPosition = startPosition + (Vector3.down * downDistance);
        
        float elapsedTime = 0f;
        
        float duration = downDistance / moveSpeed;

        while (elapsedTime < duration)
        {
            
            doorObject.transform.position = Vector3.MoveTowards(doorObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        
        doorObject.transform.position = targetPosition;
        Debug.Log("Usa a ajuns jos.");
    }

    void CloseMinigame()
    {
        
        gameObject.SetActive(false); 
    }

    void OnEnable()
    {
        if (playerMovement != null) playerMovement.enabled = false;
        if (cameraLook != null) cameraLook.enabled = false;
        if (playerInput != null) playerInput.DeactivateInput();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        if (cameraLook != null) cameraLook.enabled = true;
        if (playerInput != null) playerInput.ActivateInput();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
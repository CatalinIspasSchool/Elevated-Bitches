using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	Image image; 

	[HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag (PointerEventData eventData)
    {
        //Debug.Log("BeginDrag");
		parentAfterDrag = transform.parent;
		transform.SetParent(transform.root);
		transform.SetAsLastSibling();

		image.raycastTarget = false;
	}
	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("Drag");

		transform.position = Input.mousePosition;

	}
	public void OnEndDrag(PointerEventData eventData)
	{
		//Debug.Log("EndDrag");
		transform.SetParent(parentAfterDrag);
		image.raycastTarget = true;

	}




	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

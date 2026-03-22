using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSlot : MonoBehaviour, IDropHandler
{
	public DraggableItem targetImage;
	public bool hasCorrectFragment;
	public void OnDrop (PointerEventData eventData)
	{
		GameObject dropped = eventData.pointerDrag;
		DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
		if (transform.childCount == 0)
		{
			draggableItem.parentAfterDrag = transform;

		}


		Debug.Log(targetImage);
		Debug.Log(draggableItem);
		if (draggableItem == targetImage)
		{
			hasCorrectFragment = true;
		}
		else
		{
			hasCorrectFragment = false;
		}

	}


}

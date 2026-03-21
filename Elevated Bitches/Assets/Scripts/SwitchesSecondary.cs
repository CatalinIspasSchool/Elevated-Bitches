using UnityEngine;

public class SwitchesSecondary : MonoBehaviour
{
	SwitchesCore switchesCore;
	[SerializeField] int indexNum = -1;
	[SerializeField] int[] targets;
	[SerializeField] GameObject activeChild;
	[SerializeField] GameObject inactiveChild;


	private void Start()
	{
		switchesCore = transform.parent.GetComponent<SwitchesCore>();
		switchesCore.switchArray[indexNum] = this;
	}
	public void FlipSwitch()
	{
		switchesCore.TriggerLight(targets);
	}

	public void SetStatus(bool newStatus)
	{
		if (newStatus) 
		{
			activeChild.SetActive(true);
			inactiveChild.SetActive(false);
		}
		else
		{
			activeChild.SetActive(false);
			inactiveChild.SetActive(true);
		}
	}
}

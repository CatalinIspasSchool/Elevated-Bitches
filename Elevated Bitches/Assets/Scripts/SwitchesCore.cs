using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class SwitchesCore : MonoBehaviour
{
	public UnityEvent PuzzleComplete;
	public SwitchesSecondary[] switchArray =
	{
		null,	null,	null,	null,	null,	null,	null,	null
	};

	bool[] lightMatrix =
	{
		false,	false,	false,	false,	false,	false,	false,	false
	};


	private void Update()
	{
		bool finished = true;
		
		for (int i = 0; i < 8; i++) {
			if (!lightMatrix[i]) 
			{
				finished = false; break;
			}
		}
		if (finished) PuzzleComplete.Invoke();

	}

	public void TriggerLight(int[] targets, int id)
	{

		lightMatrix[id] = !lightMatrix[id];
		switchArray[id].SetStatus(lightMatrix[id]);

		foreach (int target in targets)
		{
			lightMatrix[target] = !lightMatrix[target];
			switchArray[target].SetStatus(lightMatrix[target]);
		}
	}
}

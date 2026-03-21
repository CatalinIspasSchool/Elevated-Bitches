using UnityEngine;
using UnityEngine.Events;

public class SwitchesCore : MonoBehaviour
{
	public UnityEvent PuzzleComplete;

	bool[] lightMatrix =
	{
		false, false, false, false, false, false, false, false
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

	public void TriggerLight(int x)
	{
		while (x > 0)
		{
			lightMatrix[x % 10] = !lightMatrix[x % 10];
			x = x / 10;
		}
	}
}

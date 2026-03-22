using UnityEngine;
using UnityEngine.Events;

public class PiecesPuzzleCore : MonoBehaviour
{
	public UnityEvent PuzzleComplete;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

		bool finished = true;

		foreach (GridSlot child in GetComponentsInChildren<GridSlot>())
		{
            if (!child.hasCorrectFragment)
            {
                finished = false; break;
            }
		}
        if (finished)
        {
            Debug.Log(finished);
            PuzzleComplete.Invoke();
        }
	}
}

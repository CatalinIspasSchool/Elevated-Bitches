using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BasicTimer : MonoBehaviour
{
    public float secondsToBeat = 180;
    float timeElapsed = 0;
    int timeToDisplay;
    UnityEvent onTimerFinish;
	public Text timeText;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime; 
        if (timeElapsed > secondsToBeat) 
        {
            onTimerFinish.Invoke();
        }
        timeToDisplay =  (int)(secondsToBeat - timeElapsed);

        timeText.text = timeToDisplay.ToString();


    }
}

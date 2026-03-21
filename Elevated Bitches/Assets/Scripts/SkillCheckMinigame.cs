using UnityEngine;
using UnityEngine.UI;

public class SkillCheckMinigame : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform cursor;
    public RectTransform greenZone;
    public RectTransform barBackground;

    [Header("Settings")]
    public float cursorSpeed = 300f;
    public Animator handsAnimator;
    public int requiredSuccesses = 5; 

    private int currentSuccesses = 0; 
    private bool movingRight = true;
    private float barWidth;
    private bool isMinigameActive = false;

    void OnEnable()
    {
        barWidth = barBackground.rect.width;
        isMinigameActive = true;
        currentSuccesses = 0; // Resetăm progresul la început
        
        cursor.anchoredPosition = new Vector2(-barWidth / 2, 0);
        RandomizeGreenZone();

        // Deblocăm mouse-ul dacă e nevoie
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (!isMinigameActive) return;

        MoveCursor();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }

    void MoveCursor()
    {
        float limit = barWidth / 2;
        if (movingRight)
        {
            cursor.anchoredPosition += new Vector2(cursorSpeed * Time.deltaTime, 0);
            if (cursor.anchoredPosition.x >= limit) movingRight = false;
        }
        else
        {
            cursor.anchoredPosition -= new Vector2(cursorSpeed * Time.deltaTime, 0);
            if (cursor.anchoredPosition.x <= -limit) movingRight = true;
        }
    }

    void CheckSuccess()
    {
        float greenStart = greenZone.anchoredPosition.x - (greenZone.rect.width / 2);
        float greenEnd = greenZone.anchoredPosition.x + (greenZone.rect.width / 2);
        float cursorX = cursor.anchoredPosition.x;

        if (cursorX >= greenStart && cursorX <= greenEnd)
        {
            
            currentSuccesses++;
            
            
            if (handsAnimator != null) handsAnimator.SetTrigger("HitSuccess");

            
            if (currentSuccesses >= requiredSuccesses)
            {
                WinMinigame();
                return; 
            }
        }
        else
        {
            
            Debug.Log("<color=red>FAIL!</color>");
            if (handsAnimator != null) handsAnimator.SetTrigger("HitFail");
            
            // Opțional: Poți scădea din progres dacă vrei să fie mai greu
            // if (currentSuccesses > 0) currentSuccesses--;
        }

        
        RandomizeGreenZone();
    }

    void RandomizeGreenZone()
    {
        float halfBar = barWidth / 2;
        float margin = greenZone.rect.width / 2;
        float randomX = Random.Range(-halfBar + margin, halfBar - margin);
        greenZone.anchoredPosition = new Vector2(randomX, 0);
    }

    void WinMinigame()
    {
        isMinigameActive = false;
        
        
        
        Invoke("CloseUI", 0.6f);
    }

    void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
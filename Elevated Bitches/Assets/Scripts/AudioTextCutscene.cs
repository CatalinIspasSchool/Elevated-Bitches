using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DelayedTextCutscene : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI cutsceneText;
    [SerializeField] private string nextSceneName;

    [Header("Timing Settings")]
    [SerializeField] private float textDelay = 2.0f; // Seconds to wait BEFORE text starts
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float waitAfterAudio = 1.5f;

    [Header("Content")]
    [TextArea(3, 10)]
    [SerializeField] private string fullStoryText;

    private void Start()
    {
        if (cutsceneText != null)
        {
            cutsceneText.text = ""; // Start with no text
            StartCoroutine(PlaySequence());
        }
    }

    private IEnumerator PlaySequence()
    {
        // 1. Start the Audio immediately
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // 2. WAIT for the specific amount of time you chose
        yield return new WaitForSeconds(textDelay);

        // 3. Start the Typewriter effect
        foreach (char letter in fullStoryText.ToCharArray())
        {
            cutsceneText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // 4. Wait for audio to finish (if it's still playing)
        if (audioSource != null && audioSource.isPlaying)
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        // 5. Short pause before loading next scene
        yield return new WaitForSeconds(waitAfterAudio);
        SceneManager.LoadScene(nextSceneName);
    }
}
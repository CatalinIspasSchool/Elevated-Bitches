using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSceneTransition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string sceneToLoad;

    [Header("Settings")]
    [SerializeField] private float extraDelay = 0.1f; // Tiny buffer so it doesn't cut off too sharply

    private void Start()
    {
        // Start the process automatically when the scene begins
        if (audioSource != null && audioSource.clip != null)
        {
            StartCoroutine(WaitForAudioAndSkip());
        }
        else
        {
            Debug.LogError("Missing AudioSource or Clip on " + gameObject.name);
        }
    }

    private IEnumerator WaitForAudioAndSkip()
    {
        // 1. Play the sound
        audioSource.Play();

        // 2. Calculate how long the clip is
        float clipLength = audioSource.clip.length;

        // 3. Wait for the duration of the clip plus the tiny buffer
        yield return new WaitForSeconds(clipLength + extraDelay);

        // 4. Switch scenes immediately
        SceneManager.LoadScene(sceneToLoad);
    }
}
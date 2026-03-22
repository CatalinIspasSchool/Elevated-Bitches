using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

	public void QuitGame()
	{
		Application.Quit();
	}
	public void SetScene(string newScene)
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));
	}
}

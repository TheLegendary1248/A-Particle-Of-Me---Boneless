using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
	public void LoadGame(string SceneName)
	{
		SceneManager.LoadScene(SceneName);
	}

}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Center : MonoBehaviour
{
	public ParticleSystem ps;
	public Material material;
	public static Center main;
	Color color;
	public GameObject trans;
	public static int lives = 5;
	public static Transform position;
	public static float size = 15f;
	private void FixedUpdate()
	{
		color = Color.HSVToRGB(Mathf.Repeat(Time.time / 20f, 1f), 1f, 1f);
	}
	private void LateUpdate()
	{
		material.color = color;
	}
	void Awake()
	{
		position = transform;
		main = this;
		lives = 5;
	}

	public void TakeDamage()
	{
		StopCoroutine("Pulse");
		StartCoroutine("Pulse");
		lives--;
		var main = ps.main;
		main.maxParticles = lives * 50;
		if (lives <= 0)
		{
			//GameObject it = Instantiate(trans, Vector3.right * 89, Camera.main.transform.rotation);
			//DontDestroyOnLoad(it);
			SceneManager.LoadScene("Start");
			StartCoroutine("Transition");
		}
	}
	IEnumerator Transition()
	{
		yield return new WaitForSeconds(1f);
		//SceneManager.LoadScene("Start");
	}
	IEnumerator Pulse()
	{
		float stamp = Time.time;
		do
		{
			yield return new WaitForFixedUpdate();
			color = Color.Lerp(Color.red, color, Time.time - stamp);
		}
		while (Time.time - stamp < 1f);
	}
}

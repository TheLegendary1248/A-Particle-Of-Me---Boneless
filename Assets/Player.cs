using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Transform position;
	public static Player main;
	public Vector2 mouseClickPos;
	public static Vector2 velocity;
	public float speedReduce;
	public float slowDownFactor;
	public float ScreenWidth;
	public float ScreenHeight;
	public static bool isEnabled = true;

	void Start()
	{
		main = this;
		position = transform;
	}
	void FixedUpdate()
	{
		velocity /= slowDownFactor;
		transform.Translate(velocity);
		if (Mathf.Abs(transform.position.x) > ScreenWidth)
		{ velocity.x *= -1.1f; }
		if (Mathf.Abs(transform.position.y) > ScreenHeight)
		{ velocity.y *= -1.1f;}
	}
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		}
		else if (Input.GetMouseButtonUp(0))
		{ 
			velocity = mouseClickPos - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			velocity /= speedReduce;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
		}
	}
	public void Disable()
	{
		ParticleSystem ps = GetComponent<ParticleSystem>();
		ps.Stop();
		isEnabled = false;
		velocity = Vector2.zero;
		StartCoroutine("Enable");
	}

	IEnumerator Enable()
	{
		yield return new WaitForSeconds(2f);
		ParticleSystem ps = GetComponent<ParticleSystem>();
		ps.Play();
		isEnabled = true;
	}
}

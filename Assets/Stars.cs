using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
	static GameObject self;

	private void Start()
	{
		self = gameObject;
	}
	public static void AddScore(int score)
	{
		var main = self.GetComponent<ParticleSystem>().main;
		main.maxParticles += score;
		//You have to put it in a var because it is an interface. Idk why
	}

}

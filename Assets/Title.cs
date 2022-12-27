using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
	public float speed = 10f;
	public int ScreenWidth;

	void FixedUpdate()
	{
		transform.Translate(-speed * Time.deltaTime, 0f, 0f);
		if (transform.position.x < -ScreenWidth)
			transform.position = new Vector3(ScreenWidth, 3.5f , 0);
		//Here you actually don’t need brackets because it’s just one statement. You need them if
		//there’s more. What this does is send the object back to the right of the screen if past the 	//screen width
	}

}


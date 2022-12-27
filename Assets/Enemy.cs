using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject deathFX;
	public Vector2 dir;
	public float speed = 1f;
	public float size = 5f;
	public int health = 1;
	public Vector2 velocity;
	public float slowDownFactor = 1.05f;
	public bool isTangible = false;
	public enum Type
	{
		reg, heavy, explode, kill
	}
	public Type type;
	private void Start()
	{
		if (type != Type.kill)
		{
			dir = (Vector2)Center.position.position - (Vector2)transform.position;
		}
		else
		{
			dir = (Vector2)Center.position.position + new Vector2(Random.Range(-50, 50), Random.Range(-50, 50)) - (Vector2)transform.position;
			StartCoroutine("OutOfBoundsCheck");
		}
		dir.Normalize();
	}
	void FixedUpdate()
	{
		transform.Translate(dir * speed * Time.fixedDeltaTime);
		if(type == Type.heavy)
		{
			transform.Translate(velocity * Time.fixedDeltaTime);
			velocity /= slowDownFactor;
		}
		if (Vector3.Distance(Player.position.position, transform.position) < size & Player.isEnabled & isTangible)
			DestroyEnemy();
		if (Vector3.Distance(Center.position.position, transform.position) < Center.size & type != Type.kill)
		{
			Center.main.TakeDamage();
			GetComponent<ParticleSystem>().Stop();
			Destroy(this);
		}
	}
	void DestroyEnemy()
	{
		switch (type)
		{
			case Type.reg:
				goto default;
			case Type.heavy:
				velocity = Player.velocity * 40f;
				Player.velocity /= 4f;
				health--;
				if (health < 1)
					goto default;
				isTangible = false;
				StartCoroutine("Wait");
				break;
			case Type.explode:
				Player.velocity += (Vector2)(Player.position.position - transform.position) * 1f;
				goto default;
			case Type.kill:
				Player.main.Disable();
				goto default;
			default:
				GetComponent<ParticleSystem>().Stop();
				Instantiate(deathFX, transform.position, transform.rotation);
				Stars.AddScore(1);
				Destroy(this);
				break;
		}
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(0.5f);
		isTangible = true;
		dir = (Vector2)Center.position.position + new Vector2(Random.Range(-50, 50), Random.Range(-50, 50)) - (Vector2)transform.position;
		dir.Normalize();
	}
	IEnumerator OutOfBoundsCheck()
	{
		yield return new WaitForSeconds(10f);
		if (Vector3.Distance(transform.position, Vector3.zero) > 500f)
		{
			Destroy(gameObject);
		}
		else
		{
			StartCoroutine("OutOfBoundsCheck");
		}
	}

}

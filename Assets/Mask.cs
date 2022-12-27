using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public float baseSpeed;
    public Vector2 speed;
    public float dirOffset;
    public float scale;
    private void Start()
    {
        scale = Random.Range(0.75f, 3f);
        transform.localScale *= scale;
        speed = (Vector2)Center.position.position + new Vector2(Random.Range(-dirOffset, dirOffset), Random.Range(-dirOffset, dirOffset)) - (Vector2)transform.position;
        speed.Normalize();
        speed *= baseSpeed;
    }
    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime / scale);
        transform.Rotate(Vector3.forward, 3f * Time.fixedDeltaTime / scale);
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

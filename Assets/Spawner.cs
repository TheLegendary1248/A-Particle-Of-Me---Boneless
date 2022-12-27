using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject NormEnemy;
    public GameObject Mask;
    public GameObject Heavy, Kill, Explode;
    public int Wave;
    public float SpawnRadius;
    public float CamSpeedDir;
    private void Start()
    {
        StartCoroutine("Flip");
        StartCoroutine("Spawn");
    }
    
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Mathf.Max(2f, 6f - (Wave/10f)));
        GameObject selected;
        if (Wave % 5 == 0)
        {
            selected = Mask;
        }
        else if (Wave % 5 == 1 & Wave > 10) 
        {
            selected = Heavy;
        }
        else if(Wave % 5 == 2 & Wave > 15)
        {
            selected = Explode;
        }
        else if (Wave % 5 == 3 & Wave > 20)
        {
            selected = Kill;
        }
        else
        {
            selected = NormEnemy;
        }
        Instantiate(selected, Random.insideUnitCircle.normalized * SpawnRadius, Quaternion.identity);

        Wave++;
        StartCoroutine("Spawn");
    }
    IEnumerator Flip()
    {
        yield return new WaitForSeconds(10f);
        float sign = Mathf.Sign(-CamSpeedDir);
        float stamp = Time.time;
        do
        {
            yield return new WaitForFixedUpdate();
            CamSpeedDir = sign * Mathf.Lerp(0, 1, (Time.time - stamp) / 5f);
        }
        while (Time.time - stamp < 5f);
        StartCoroutine("Flip");
    }

    private void FixedUpdate()
    {
        Camera.main.transform.Rotate(Vector3.forward, Wave * Time.fixedDeltaTime * CamSpeedDir / 3f);
    }
}

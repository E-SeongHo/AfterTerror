using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 origin_position;
    private float time = 0.5f;
    private float amount = 30f;

    private void Start()
    {
        origin_position = gameObject.transform.position;
    }

    IEnumerator ShakeCamera()
    {
        time = 0.5f;
        int count = 0;
        while(time > 0)
        {
            if(count % 5 == 0)
                gameObject.transform.position = origin_position + new Vector3(Random.value * amount, 0, 0);
            time -= Time.deltaTime;
            count++;
            yield return null;
        }
        Debug.Log(count);
        gameObject.transform.position = origin_position;
    }
}

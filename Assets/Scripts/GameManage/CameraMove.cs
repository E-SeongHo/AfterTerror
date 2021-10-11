using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private Vector3 origin_position;
    private float time = 1f;
    private float amount = 50f;

    private void Start()
    {
        origin_position = gameObject.transform.position;
    }

    IEnumerator ShakeCamera()
    {
        while(time > 0)
        {
            gameObject.transform.position = Random.insideUnitSphere * amount + origin_position;
            time -= Time.deltaTime;
            Debug.Log(gameObject.transform.position);
            yield return null;
        }
        time = 0.0f;
        gameObject.transform.position = origin_position;
    }
}

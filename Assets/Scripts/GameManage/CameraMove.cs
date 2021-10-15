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

    public void HitProcess(float shake_time, float shake_amount)
    {
        StartCoroutine(ShakeCamera(shake_time, shake_amount));
    }
    public void ShieldProcess(float shake_time, float shake_amount)
    {

    }

    IEnumerator ShakeCamera(float shake_time, float shake_amount)
    {
        time = shake_time;
        amount = shake_amount;
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
    IEnumerator SlowMotion(float time_scale)
    {
        // make slow
        Time.timeScale = time_scale;
        Time.fixedDeltaTime = 0.02f * time_scale;

        // generate effect
        yield return null;

        // reset time
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
}

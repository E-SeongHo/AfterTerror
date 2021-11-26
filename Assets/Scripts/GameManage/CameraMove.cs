using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 originPosition;
    private float time = 0.5f;
    private float amount = 30f;

    private GameUI uiController;

    private void Start()
    {
        originPosition = gameObject.transform.position;
        uiController = GameObject.Find("GameManage").gameObject.GetComponent<GameUI>();
    }

    public void HitProcess(float shake_time, float shake_amount)
    {
        uiController.BloodScreenON();
        StartCoroutine(ShakeCamera(shake_time, shake_amount));
    }
    public void ShieldProcess(float shake_time, float shake_amount)
    {
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        StartCoroutine(ShakeCamera(shake_time, shake_amount));
    }
    public void EnemyAttackProcess(float shake_time, float shake_amount)
    {
        StartCoroutine(ShakeCamera(shake_time, shake_amount));
    }

    private IEnumerator ShakeCamera(float shake_time, float shake_amount)
    {
        time = shake_time;
        amount = shake_amount;
        while(time > 0)
        {
            gameObject.transform.position = originPosition + new Vector3(Random.value * amount, 0, 0);
            time -= Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = originPosition;
        
        // no think
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        uiController.BloodScreenOFF();
    }
}

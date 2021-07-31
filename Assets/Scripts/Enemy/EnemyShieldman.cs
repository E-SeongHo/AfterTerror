using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 방패 꽂고 나면 체력 상승
public class EnemyShieldman : MonoBehaviour
{
    // Only shield
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int startHealth = 3;
    [SerializeField] private float speed = 100f;

    private bool perform = false;
    private EnemyController controller;
    
    private void Start()
    {
        controller = GetComponent<EnemyController>();
        controller.SetCurrentHealth(startHealth);
        controller.SetMaxHealth(maxHealth);
    }
    private void FixedUpdate()
    {
        // 지금 interactionState를 자신기준이라 SmashDownShield 계속호출됨
        if (controller.GetInteractionState() && !perform)
        {
            StartCoroutine("RunForward");
        }
    }
    private void SmashDownShield()
    {
        // shield animation

        // apply shield's health (+7)  
        controller.ChangeHealth(7);
    }

    IEnumerator RunForward()
    {
        // run animation 
        // Debug.Log(gameObject.transform.position.x + " / " + EnemyButtonManage.Instance.GetTargetPos().x);
        
        while(gameObject.transform.position.x - EnemyButtonManage.Instance.GetTargetWorldPos().x > -250f)
        {
            // Vector2 target_pos = EnemyButtonManage.Instance.GetTargetLocalPos();

            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }

        SmashDownShield();
        perform = true;

        yield return null;
    }
}

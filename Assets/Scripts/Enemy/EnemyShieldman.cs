using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ¹æÆÐ ²È°í ³ª¸é Ã¼·Â »ó½Â
public class EnemyShieldman : MonoBehaviour
{
    // Only shield
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int startHealth = 3;
    [SerializeField] private float speed = 30f;

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
        if (controller.GetInteractionState() & !perform)
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
        
        while(gameObject.transform.position.x - EnemyButtonManage.Instance.GetTargetWorldPos().x > -100f)
        {
            // Vector2 target_pos = EnemyButtonManage.Instance.GetTargetLocalPos();

            Debug.Log("go");
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            Debug.Log(transform.position.x);
            yield return new WaitForFixedUpdate();
        }

        SmashDownShield();
        perform = true;

        yield return null;
    }
}

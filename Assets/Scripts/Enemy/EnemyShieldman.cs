using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 방패 꽂고 나면 체력 상승
public class EnemyShieldman : MonoBehaviour
{
    // Only shield
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int startHealth = 3;

    private EnemyController controller;
    

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        controller.SetCurrentHealth(startHealth);
        controller.SetMaxHealth(maxHealth);
    }
    private void FixedUpdate()
    {
        if(controller.GetInteractionState())
        {

        }

        // 앞에 적이 없을 때 까지
    }
    private void SmashDownShield()
    {
        // animation

    }
}

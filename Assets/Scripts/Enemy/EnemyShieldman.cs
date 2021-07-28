using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �Ȱ� ���� ü�� ���
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

        // �տ� ���� ���� �� ����
    }
    private void SmashDownShield()
    {
        // animation

    }
}

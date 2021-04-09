using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 6;
    int attackAbility = 1;
    [SerializeField] private int currentHealth;
    public int health
    {
        get{return currentHealth;}
    }
    public int attack
    {
        get{return attackAbility;}
    }

    //public GameObject healthBarUI;
    //public Slider slider;

    void Start()
    {
        currentHealth = 3;
        //slider.value = CalculateHealth();
    }

    void Update()
    {
        //slider.value = CalculateHealth();

        // if (health < maxHealth)
        // {
        //     healthBarUI.SetActive(true);
        // }

        // if (health <= 0)
        // {
        //     Destroy(gameObject);
        // }

        // if (health > maxHealth)
        // {
        //     health = maxHealth;
        // }
    }

    // float CalculateHealth()
    // {
    //     return health / maxHealth;
    // }

    public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            // 피격 효과 애니메이션 처리
        }
        else
        {
            // 회복 효과 애니메이션 처리
        }
        // Clamp 메소드 -> 최소 0, 최대 maxHealth로 구현
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + " / " + maxHealth);
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}

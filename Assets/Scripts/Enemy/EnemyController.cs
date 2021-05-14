using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    GameObject currentButton;
    public int maxHealth = 6;
    bool isButtonOn = false;
    [SerializeField] private int currentHealth;
    public int health
    {
        get { return currentHealth; }
    }
    public bool buttonActive
    {
        get { return isButtonOn; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {

    }
    public void GenerateEnemy(GameObject button)
    {
        Vector2 position = GetComponent<Rigidbody2D>().position;
        GameObject newButton = Instantiate(button, position + Vector2.up * 1.5f, Quaternion.identity);
        currentButton = newButton;
        currentButton.transform.parent = gameObject.transform;
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
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

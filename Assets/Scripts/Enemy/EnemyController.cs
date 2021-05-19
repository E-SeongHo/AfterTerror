using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int shotbulletbound = 3; // 3이상이면 총알발사
    private int currentHealth;
    private int attackCount; // MainCharacter에게 맞은 횟수
    
    private GameObject myButton = null;
    private Rigidbody2D rb = null;

    // Getters
    public float GetSpeed() { return speed; }
    public int GetCurrentHealth() { return currentHealth; }
    public int GetAttackCount() { return attackCount; }

    // Init
    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }
    // Enemy Move
    private void Update()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        if (attackCount > shotbulletbound)
        {
            ShotBullet();
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            // animator 처리 부분 (After Time)
        }
        // Clamp 메소드 이용, 최대값이 maxHealth넘지 못하게 구현 
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + " / " + maxHealth);
        if (currentHealth <= 0)
        {
            // 죽는 애니메이션
            Destroy(gameObject);
        }
    }
    public void ShotBullet()
    {
        // Object Pooling bullet 관리
    }

    public void GenerateButton(GameObject button)
    {
        Vector2 position = rb.position + Vector2.up*1.5f;
        GameObject newButton = Instantiate(button, position, Quaternion.identity);
        myButton = newButton;
        myButton.transform.parent = gameObject.transform;
    }
    public void DeleteButton()
    {
        Destroy(myButton);
    }

}

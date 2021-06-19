using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 랜덤발사 타이머

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int shotbulletbound = 3; // 3이상이면 총알발사
    private int currentHealth;
    private int attackCount = 0; // MainCharacter에게 맞은 횟수
    private float autoShotTime;
    private GameObject bullet = null; 

    private GameObject myButton = null;
    private Rigidbody2D rb = null;

    // Getters
    public int GetCurrentHealth() { return currentHealth; }
    public int GetAttackCount() { return attackCount; }
    // Setters
    public void ChangeAttackCount(int amount) { attackCount = attackCount + amount; }

    // Init
    private void Awake()
    {
        currentHealth = maxHealth;
        autoShotTime = Random.Range(0.5f, 3.0f);
        rb = GetComponent<Rigidbody2D>();
    }
    // Enemy Move
    private void Update()
    {
        if (autoShotTime > 0)
        {
            autoShotTime -= Time.deltaTime;
            if (autoShotTime < 0)
            {
                ShotBullet();
                autoShotTime = 3f;
            }
        }
        if (attackCount >= shotbulletbound)
        {
            ShotBullet();
            attackCount = 0;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            // 피격 animator 처리 부분 
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

    // Bullet을 쏘는 것 까지는 책임, 쏜 이후는 책임 X
    private void ShotBullet()
    {
        bullet = BulletPool.Instance.AllocateBullet();
        // OnEnable에서 v값 계산하니까 position이 정해진 후에 SetActive(true)해야 한다.
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int shotbulletbound = 3; // 3�̻��̸� �Ѿ˹߻�
    private int currentHealth;
    private int attackCount = 0; // MainCharacter���� ���� Ƚ��
    private GameObject bullet = null; 

    private GameObject myButton = null;
    private Rigidbody2D rb = null;

    // Getters
    public float GetSpeed() { return speed; }
    public int GetCurrentHealth() { return currentHealth; }
    public int GetAttackCount() { return attackCount; }
    // Setters
    public void ChangeAttackCount(int amount) { attackCount = attackCount + amount; }

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
            // �ǰ� animator ó�� �κ� 
        }
        // Clamp �޼ҵ� �̿�, �ִ밪�� maxHealth���� ���ϰ� ���� 
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + " / " + maxHealth);
        if (currentHealth <= 0)
        {
            // �״� �ִϸ��̼�
            Destroy(gameObject);
        }
    }

    // Bullet�� ��� �� ������ å��, �� ���Ĵ� å�� X
    private void ShotBullet()
    {
        bullet = BulletPool.Instance.AllocateBullet();
        // bullet�� SetActive(true), SetActive(false)�� ��� �ؾ� ������,,
        // �ϴ��� BulletPool���� ����
        // OnEnable���� v�� ����ϴϱ� position�� ������ �Ŀ� SetActive(true)�ؾ� �Ѵ�.
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

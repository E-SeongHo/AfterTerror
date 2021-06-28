using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����߻� Ÿ�̸�

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int shotbulletbound = 3; // 3�̻��̸� �Ѿ˹߻�
    private int currentHealth; 
    private int attackCount = 0; // MainCharacter���� ���� Ƚ��
    private float autoShotTime;
    // private float rand;
    private GameObject bullet = null; 

    private GameObject myButton = null;
    private Rigidbody2D rb = null;
    private Transform playerTransform;
    private bool interaction = false;
    private bool run = false;

    private float rundist = 240f;
    // Getters
    public int GetCurrentHealth() { return currentHealth; }
    public int GetAttackCount() { return attackCount; }
    public bool GetInteractionState() { return interaction; }
    public bool GetRunState() { return run; }
    // Setters
    public void ChangeAttackCount(int amount) { attackCount = attackCount + amount; }
    // Init
    private void Awake()
    {
        currentHealth = maxHealth;
        //rand = Random.Range(0.5f, 3.0f);
        //autoShotTime = rand;
        autoShotTime = Random.Range(0.5f, 3.0f);
        rb = GetComponent<Rigidbody2D>();
        playerTransform = ShieldmanController.Instance.transform;
    }
    // Enemy Move
    private void Update()
    {   
        // FixedUpdate���� �ϴ°� ������ ��
        if (interaction)
        {
            AutoShotProcess();
            HitShotProcess();
        }
        if (run)
        {
            Destroy(gameObject);
        }
    }
    // ��Ȯ�� �������� ���� ����
    private void FixedUpdate()
    {
        // �ʿ��� ���̱� ������ �� �� ��� ����
        if (!interaction && transform.position.x - playerTransform.position.x <= 1920
            && transform.position.x - playerTransform.position.x > rundist)
        {
            interaction = true;
        }
        else if (transform.position.x - playerTransform.position.x <= rundist)
        {
            interaction = false;
            run = true;
            // �ִϸ��̼� ���� 
            // �ִϸ��̼� ���� �� �ı� �ǵ��� ����
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
        // OnEnable���� v�� ����ϴϱ� position�� ������ �Ŀ� SetActive(true)�ؾ� �Ѵ�.
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }
    private void AutoShotProcess()
    {
        autoShotTime -= Time.deltaTime;
        if (autoShotTime < 0)
        {
            // �� �� ���� ��� autoShotTime�� �ٽ� random�ϰ� �ٲ��. 
            ShotBullet();
            autoShotTime = Random.Range(0.5f, 3.0f);
            
            //autoShotTime = rand;
        }
    }
    private void HitShotProcess()
    {
        if (attackCount >= shotbulletbound)
        {
            ShotBullet();
            attackCount = 0;
        }
    }
    public void GenerateButton(GameObject button)
    {
        Vector2 position = rb.position + Vector2.up*150f + Vector2.right*30f;
        GameObject newButton = Instantiate(button, position, Quaternion.identity);
        myButton = newButton;
        myButton.transform.parent = gameObject.transform;
    }
    public void DeleteButton()
    {
        Destroy(myButton);
    }

}

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
        // FixedUpdate에서 하는게 나은지 비교
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
    // 정확한 지점에서 적이 도망
    private void FixedUpdate()
    {
        // 맵에서 보이기 시작할 때 총 쏘기 시작
        if (!interaction && transform.position.x - playerTransform.position.x <= 1920
            && transform.position.x - playerTransform.position.x > rundist)
        {
            interaction = true;
        }
        else if (transform.position.x - playerTransform.position.x <= rundist)
        {
            interaction = false;
            run = true;
            // 애니메이션 실행 
            // 애니메이션 실행 후 파괴 되도록 보장
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
    private void AutoShotProcess()
    {
        autoShotTime -= Time.deltaTime;
        if (autoShotTime < 0)
        {
            // 한 번 총을 쏘면 autoShotTime은 다시 random하게 바뀐다. 
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

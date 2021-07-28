using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int maxHealth; // Setter를 통해 다른 스크립트에서 설정
    private int currentHealth;

    private int attackCount = 0; // MainCharacter에게 맞은 횟수
    

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
    public void SetMaxHealth(int value) { maxHealth = value; }
    public void SetCurrentHealth(int health) { currentHealth = health; }
    public void ResetAttackCount() { attackCount = 0; }
    public void ChangeAttackCount(int amount) { attackCount = attackCount + amount; }
    // Init
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = ShieldmanController.Instance.transform;
    }
    // Enemy Run... 
    private void FixedUpdate()
    {
        // 맵에서 보이기 시작할 때 상호작용 시작 
        // Overhead :: --> Coroutine
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

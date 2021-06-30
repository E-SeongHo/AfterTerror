using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldmanController : MonoBehaviour
{
    // singleton 구현
    /*    private GameObject myButton;
        private bool buttonON = false;*/
    public static ShieldmanController Instance;
    private SpriteRenderer sprRenderer;
    public int maxHealth = 6;
    [SerializeField] private int attackAbility = 1;
    [SerializeField] private int currentHealth = 3;
    private bool invincibility = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        /*myButton = transform.GetChild(0).gameObject;
        myButton.transform.localPosition = new Vector2(0, 1.5f);
        myButton.SetActive(false);
        buttonON = false;*/
    }

    // Getters
    public int GetCurrentHealth() { return currentHealth; }
    public int GetAttackAbility() { return attackAbility; }

    // Setters
    public void ChangeHealth(int amount)
    {
        if(!invincibility && amount < 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log(currentHealth + " / " + maxHealth);
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                // 이벤트함수 호출 
            }
            else
            {
                invincibility = true;
                StartCoroutine("InvincibleTime");
            }
        }
    }
    public void ChangeAttackAbility(int amount)
    {
        // Attack능력 최대값 2
        attackAbility = Mathf.Clamp(attackAbility + amount, 0, 2);
        Debug.Log("Attack Stat : " + attackAbility);        
    }
    IEnumerator InvincibleTime()
    {
        int count = 0;
        while(count < 6)
        {
            // Alpha Effect
            if (count % 2 == 0)
                sprRenderer.color = new Color32(255, 255, 255, 90);
            else
                sprRenderer.color = new Color32(255, 255, 255, 180);

            // Wait Update Frame 
            yield return new WaitForSeconds(0.2f);
            count++;
        }

        // Alpha Effect End & Flag Off
        sprRenderer.color = new Color32(255, 255, 255, 255);
        invincibility = false;

        yield return null;
    }

/*    public void ActiveButton()
    {
        myButton.SetActive(true);
        buttonON = true;
    }
    public void InActiveButton()
    {
        myButton.SetActive(false);
        buttonON = false;
    }
    public void GenerateButton(GameObject button)
    {
        Vector2 position = GetComponent<Rigidbody2D>().position;
        GameObject newButton = Instantiate(button, position + Vector2.up * 1.5f, Quaternion.identity);
        myButton = newButton; 
        myButton.transform.parent = gameObject.transform;
    }
    public void DeleteButton()
    {
        Destroy(myButton);
        Debug.Log("Delete");
    }*/
}
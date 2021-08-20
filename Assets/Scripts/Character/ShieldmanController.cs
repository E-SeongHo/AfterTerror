using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Android;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ShieldmanController : MonoBehaviour
{
    // singleton 구현
    /*    private GameObject myButton;
        private bool buttonON = false;*/
    public static ShieldmanController Instance;
    private bool die = false; // Gameover

    [SerializeField] private int attackAbility = 1;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image healthImg;
    
    private Stack<Image> healthes = new Stack<Image>();
    private Vector2[] healthPos = new Vector2[3];

    private Animator animator;
    private Animator hand_animator;
    private Animator fire_animator;

    private GameObject hand;
    private GameObject fire_hand;

    private SpriteRenderer sprRenderer;
    public int maxHealth = 6;
    private bool invincibility = false;

    private void Awake()
    {
        Instance = this;

        // health init
        healthPos[0] = new Vector2(-713f, 382f);
        healthPos[1] = new Vector2(-626f, 382f);
        healthPos[2] = new Vector2(-536f, 382f);

        for(int i = 0; i < currentHealth; i++)
        {
            Debug.Log("hpst");
            Debug.Log(healthes.Count % 3);
            CreateHealthStack();
        }
    }
    private void Start()
    {
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        hand_animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        fire_animator = gameObject.transform.GetChild(1).GetComponent<Animator>();

        hand = gameObject.transform.GetChild(0).gameObject;
        fire_hand = gameObject.transform.GetChild(1).gameObject;

        Run();
        
        /*myButton = transform.GetChild(0).gameObject;
        myButton.transform.localPosition = new Vector2(0, 1.5f);
        myButton.SetActive(false);
        buttonON = false;*/
    }
    private void Update()
    {
        
        /*if(Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine("AttackAnimation");
        }*/
        /*if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            JumpWithHand();
        }*/

        /*Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Debug.Log(hand_animator.GetCurrentAnimatorStateInfo(0).normalizedTime);*/
    }
    // Getters
    public bool GetDieState() { return die; }
    public int GetCurrentHealth() { return currentHealth; }
    public int GetAttackAbility() { return attackAbility; }

    // Setters
    public void ChangeHealth(int amount)
    {
        if(!invincibility && amount < 0) 
        {
            int iter = currentHealth;
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log(currentHealth + " / " + maxHealth);
            iter -= currentHealth;
            
            for(int i = 0; i < iter; i++)
            {
                Image todel = healthes.Pop();
                Debug.Log("delete" + todel);
                Destroy(todel);
            }
            if (currentHealth <= 0)
            {
                animator.SetTrigger("die");
                invincibility = true;
                Destroy(gameObject, 2f);
                die = true;
                Destroy(hand);
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
    public void Run()
    {
        animator.SetTrigger("run");
        hand_animator.SetTrigger("run");
    }
    public void RunWithHand()
    {
        animator.Play("PlayerShieldman_Run");
        hand_animator.Play("HandMove", 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
    public void JumpWithHand()
    {
        animator.SetTrigger("jump");
        hand_animator.SetTrigger("jump");
    }

    IEnumerator AttackAnimation()
    {
        hand.SetActive(false);
        fire_hand.SetActive(true);
        float norm = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        fire_animator.Play("HandFire", 0, norm);

        yield return new WaitForSeconds(0.1f);

        fire_hand.SetActive(false);
        hand.SetActive(true);
        RunWithHand();
    }
    private void CreateHealthStack()
    {
        Image newHealth = Instantiate(healthImg);
        newHealth.transform.SetParent(canvas.transform);
        newHealth.transform.localPosition = healthPos[healthes.Count % 3];
        healthes.Push(newHealth);
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
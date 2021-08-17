using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HurdleController : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons = new GameObject[4];
    private int damage = 2;

    private bool buttonON = false;
    private int buttonIdx; // 0 : up, 1 : down, 2 : left, 3 : right
    private GameObject myButton; // 현재 이 Hurdle의 button객체
    private SpriteRenderer sprRenderer; // button renderer 
    private Animator animator; // this hurdle's animator

    private Vector2 playerPosition;
    private bool avoid = false; // 버튼 성공 여부 

    // Getters
    public bool GetButtonON()
    {
        return buttonON;
    }
    public int GetButtonIdx()
    {
        return buttonIdx;
    }

    // Init
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        
        buttonIdx = Random.Range(0, 4);
        Vector3 position = gameObject.transform.position + new Vector3(0, 100f, 0);
        myButton = Instantiate(buttons[buttonIdx], position, Quaternion.identity);
        myButton.transform.parent = gameObject.transform;

        // 불투명 처리
        sprRenderer = myButton.GetComponent<SpriteRenderer>();
        sprRenderer.color = new Color32(255, 255, 255, 180);
        buttonON = false;
    }

    // Hurdle Move
    void Update()
    {
        float dx = gameObject.transform.position.x - playerPosition.x;
        if (!buttonON)
        {
            if (dx <= 300f) ActiveButton();
        }
        else InputProcess(); // buttonON일 때만 InputProcessing
    }

    // Buttons 
    private void ActiveButton()
    {
        sprRenderer.color = new Color32(255, 255, 255, 255);
        buttonON = true;
    }
    // 방향키에 대한 입력만 false / true 처리해야한다.
    // 성공 실패 상관 없이 방향키 입력이 있으면 버튼은 지워진다.
    private void InputProcess()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (buttonIdx == 0) avoid = true;
            DeleteButton(); 
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            if (buttonIdx == 1) avoid = true;
            DeleteButton();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (buttonIdx == 2) avoid = true;
            DeleteButton();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (buttonIdx == 3) avoid = true;
            DeleteButton();
        }
    }
    public void DeleteButton()
    {
        myButton.SetActive(false);
    }

    // Hurdle과 MainCharacter 충돌 event
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (avoid) 
            {
                ShieldmanController.Instance.JumpWithHand();
            }
            else
            {
                ShieldmanController.Instance.ChangeHealth(-1 * damage);
                animator.SetTrigger("explosion");
                myButton.SetActive(false);
                Destroy(gameObject, 2f);
            }
        }
    }
}

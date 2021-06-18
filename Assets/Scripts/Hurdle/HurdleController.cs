using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleController : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons = new GameObject[4];
    
    private bool buttonON = false;
    private int buttonIdx; // 0 : up, 1 : down, 2 : left, 3 : right
    private GameObject myButton; // 현재 이 Hurdle의 button객체
    private Vector2 playerPosition;
    private bool avoid = false; // 버튼 성공 여부 

    private Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        buttonIdx = Random.Range(0, 4);
        myButton = Instantiate(buttons[buttonIdx], rb.position + Vector2.up*1.5f, Quaternion.identity);
        myButton.transform.parent = gameObject.transform;
        myButton.SetActive(false); 
        buttonON = false;
    }

    // Hurdle Move
    void Update()
    {
        //transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        /*Vector2 position = rb.position;
        position.x = position.x - speed * Time.deltaTime;
        rb.MovePosition(position);*/
        float dx = rb.position.x - playerPosition.x;
        if (!buttonON)
        {
            if (dx <= 2f) ActiveButton();
        }
        else InputProcess(); // buttonON일 때만 InputProcessing
    }

    // Buttons 
    private void ActiveButton()
    {
        myButton.SetActive(true);
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
        if(!avoid)
        {
            ShieldmanController player = other.gameObject.GetComponent<ShieldmanController>();
            if (player != null)
            {
                Debug.Log(this + " - " + other + "충돌");
                player.ChangeHealth(-1);
            }
        }
    }
}

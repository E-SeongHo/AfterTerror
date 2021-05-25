using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hurdle은 맵에 여러개 존재
// 각 Hurdle 위에 버튼은 미리 떠있음 (가까워 졌을 때 뜨는 식 X)
// UP, DOWN, LEFT, RIGHT키 이용 각 Hurdle이 가까워 졌을 때 누르면 판정
public class HurdleController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private bool buttonON = false;
    private int buttonIdx; // 0 : up, 1 : down, 2 : left, 3 : right

    private GameObject myButton; // 현재 이 Hurdle의 button객체
    // 나중에 각 Button에 대한 스크립트 만들어서, 자신의 Idx소유하도록 코딩이 더 좋을듯

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
    public float GetSpeed()
    {
        return speed;
    }

    // Init
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Hurdle Move
    void Update()
    {
        //transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        /*Vector2 position = rb.position;
        position.x = position.x - speed * Time.deltaTime;
        rb.MovePosition(position);*/
    }
    
    // Hurdle과 MainCharacter 충돌 event
    private void OnTriggerEnter2D(Collider2D other) 
    {
        ShieldmanController player = other.gameObject.GetComponent<ShieldmanController>();
        if(player != null)
        {
            Debug.Log(this + " - " + other + "충돌");
            player.ChangeHealth(-1);
        }        
    }

    // Buttons
    
    // HurdleButtonManage객체가 호출
    public void GenerateButton(GameObject button, int kind)
    {
        if(!buttonON)
        {
            Vector2 position = rb.position; // 현재 pos 
            GameObject newButton = Instantiate(button, position + Vector2.up * 1.5f, Quaternion.identity);
            // 자식으로 할당 : 동시에 움직이도록 구현
            myButton = newButton;
            myButton.transform.parent = gameObject.transform;
            buttonON = true;
            buttonIdx = kind;
        }
        else 
        {
            //Debug.Log(gameObject + "Already generated button");
        }
    }
    public void DeleteButton()
    {
        Destroy(myButton);
    }
}

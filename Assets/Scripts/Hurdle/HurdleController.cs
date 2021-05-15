using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleController : MonoBehaviour
{
    Rigidbody2D rb;
    private bool buttonON = false;
    public bool buttonActive
    {
        get{return buttonON;}
    }
    private int buttonIdx;
    public int buttonKind
    {
        get{return buttonIdx;}
    }
    [SerializeField] private float speed = 5f;
    private GameObject myButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 position = rb.position;
        position.x = position.x - speed * Time.deltaTime;
        rb.MovePosition(position);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        ShieldmanController player = other.gameObject.GetComponent<ShieldmanController>();
        Debug.Log("피격");
        if(player != null)
        {
            Debug.Log("피격");
            player.ChangeHealth(-1);
        }        
    }
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

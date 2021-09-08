using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonInfo : MonoBehaviour
{
    // 0 : A, 1 : S, 2 : D
    [SerializeField] private GameObject[] buttonPrefabs = new GameObject[3];

    private EnemyController core; // 본체

    private Queue<BasicButton> prequeue;
    private Queue<BasicButton> showing;

    private Rigidbody2D rb;
    
    private float interval = 30f; // 버튼사이 간격
    private Vector2 add = new Vector2(70f, -65f);

    private void Start()
    {
        core = gameObject.GetComponent<EnemyController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        // Enque (미리 생성해두기)
        // 하려면 Enemy Script maxHealth할당을 Awake()로 변경해야함
    }

    private void EnqueButtons()
    {
        for (int i = 0; i < core.GetMaxHealth(); i++)
        {
            int rand = Random.Range(0, 3);
            BasicButton topush = new BasicButton(Instantiate(buttonPrefabs[rand], rb.position + add, Quaternion.identity), rand);
            
            topush.button.transform.parent = gameObject.transform;
            topush.button.SetActive(false);
            prequeue.Enqueue(topush);
        }
    }

    public void ShowButtons(int num)
    {
        for(int i = 0; i < num; i++)
        {
            BasicButton togen = prequeue.Peek();
        }
    }

}

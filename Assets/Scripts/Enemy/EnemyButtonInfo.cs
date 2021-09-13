using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonInfo : MonoBehaviour
{
    // 0 : A, 1 : S, 2 : D
    [SerializeField] private GameObject[] buttonPrefabs = new GameObject[3];

    private EnemyController core; // ��ü

    private Queue<BasicButton> prequeue;
    private Queue<BasicButton> showing;

    private Rigidbody2D rb;
    
    private float interval = 30f; // ��ư���� ����
    private Vector2 add = new Vector2(70f, -65f);

    private void Start()
    {
        core = gameObject.GetComponent<EnemyController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        // Enque (�̸� �����صα�)
        // �Ϸ��� Enemy Script maxHealth�Ҵ��� Awake()�� �����ؾ���
    }
    private void EnqueButtons()
    {
        for (int i = 0; i < core.GetMaxHealth(); i++)
        {
            int rand = Random.Range(0, 3);
            // rb.position + add == ù ��ư ��ġ
            BasicButton to_push = new BasicButton(Instantiate(buttonPrefabs[rand], rb.position + add, Quaternion.identity), rand);
            to_push.button.transform.parent = gameObject.transform;
            to_push.button.SetActive(false);
            prequeue.Enqueue(to_push);
        }
    }
    public void ShowButtons(int num)
    {
        for(int i = 0; i < num; i++)
        {
            if(prequeue.Count != 0)
            {
                BasicButton to_gen = prequeue.Peek();
                to_gen.button.transform.Translate(add * showing.Count);
                to_gen.button.SetActive(true);
                showing.Enqueue(to_gen);
            }
        }
    }
    public void DeleteButton()
    {

    }
    
}

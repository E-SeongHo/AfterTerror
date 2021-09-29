using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonInfo : MonoBehaviour
{   
    private GameObject xSheet;
    private Animator xSheet_anim;

    public EnemyController core; // 본체
    private Rigidbody2D rb;

    // for basic buttons
    // 0 : A, 1 : S, 2 : D
    [SerializeField] private GameObject[] button_prefabs = new GameObject[3];
    private Queue<BasicButton> prequeue = new Queue<BasicButton>();
    private Queue<BasicButton> showing = new Queue<BasicButton>();

    private float interval = 30f; // interval for buttons
    private Vector2 add = new Vector2(-72f, 77f);

    // for special buttons
    public bool special_ON;
    private int num_special_gen = 1;
    public bool[] special_flag = new bool[2]; // if flag on, then that type can be generate

    [SerializeField] private GameObject[] specialbutton_prefabs = new GameObject[3];
    [SerializeField] private GameObject back_gauge_prefab;
    [SerializeField] private GameObject[] front_gauge_prefabs = new GameObject[3];

    private Queue<SpecialButton> special_prequeue = new Queue<SpecialButton>();

    // for button type 0 (연타)
    private Stack<GameObject> back_gauge = new Stack<GameObject>();
    private Stack<GameObject> front_gauge = new Stack<GameObject>();

    // position
    private const int bar_length = 129;
    private const int bar_first_x = -30;

    private void Start()
    {
        core = gameObject.GetComponent<EnemyController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        EnqueueButtons(); // enqueue buttons to prequeue as much as max hp (from now equal start hp)
        
        xSheet = gameObject.transform.GetChild(0).gameObject;
        xSheet_anim = xSheet.GetComponent<Animator>();
    }
    // Getters
    public int GetTopIndex() { return showing.Peek().index; }
    public GameObject GetTopButton() { return showing.Peek().button; }
    
    private void EnqueueButtons()
    {
        // special button and basic button can not co-exist
        if(special_ON)
        {
            for(int i = 0; i < num_special_gen; i++)
            {
                int type;
                while (true)
                {
                    type = Random.Range(0, 2);
                    if (special_flag[type]) break;
                }
                int rand = Random.Range(0, 3);
                SpecialButton to_push = new SpecialButton(Instantiate(specialbutton_prefabs[rand], rb.position + add, Quaternion.identity), rand, type);
                to_push.button.transform.parent = gameObject.transform;
                to_push.button.SetActive(true);
                special_prequeue.Enqueue(to_push);
            }
        }
        else
        {
            for (int i = 0; i < core.GetMaxHealth(); i++)
            {
                int rand = Random.Range(0, 3);
                // rb.position + add == 첫 버튼 위치
                BasicButton to_push = new BasicButton(Instantiate(button_prefabs[rand], rb.position + add, Quaternion.identity), rand);
                to_push.button.transform.parent = gameObject.transform;
                to_push.button.SetActive(false);
                prequeue.Enqueue(to_push);
            }
        }
    }
    // caller : ShowSpecialButton()
    private void SpecialDetail(int type)
    {
        if(type == 0) // 연타버튼
        {
            // make back gauges
            int reps = special_prequeue.Peek().times;
            float size = bar_length / (reps + 1); // bar length is 129px
            float last_x = bar_length - size + bar_first_x;
            float dis = last_x - bar_first_x;
            float dx = dis / (reps - 1);

            for(int i = 0; i < reps; i++)
            {
                // back-end gauge
                GameObject to_push = Instantiate(back_gauge_prefab);
                to_push.transform.parent = special_prequeue.Peek().button.transform;
                to_push.transform.localScale = new Vector3(1 / (float)(reps + 1), 1, 1);
                to_push.transform.localPosition = new Vector3(bar_first_x + dx * i, 0, 0);
                back_gauge.Push(to_push);

                // front-end gauge
                GameObject to_push_f = Instantiate(front_gauge_prefabs[special_prequeue.Peek().index]);
                to_push_f.transform.parent = special_prequeue.Peek().button.transform;
                to_push_f.transform.localScale = new Vector3(1 / (float)(reps + 1), 1, 1);
                to_push_f.transform.localPosition = new Vector3(bar_first_x + dx * i, 0, 0);
                front_gauge.Push(to_push_f);
            }
        }
        else if(type == 1)
        {

        }
    }
    private void CreateFromPrequeue()
    {
        if (prequeue.Count > 0)
        {
            BasicButton to_gen = prequeue.Dequeue();
            to_gen.button.transform.Translate(new Vector2(interval, 0) * showing.Count);
            to_gen.button.SetActive(true);
            showing.Enqueue(to_gen);
        }
    }
    public void ShowButtons(int num)
    {
        for(int i = 0; i < num; i++)
        {
            CreateFromPrequeue();
        }
    }
    public void ShowSpecialButton()
    {
        SpecialDetail(special_prequeue.Peek().type);


    }
    public void EmptyOutShowingQueue()
    {
        int reps = showing.Count;
        for(int i = 0; i < reps; i++)
        {
            BasicButton item = showing.Dequeue();
            item.button.SetActive(false);
            prequeue.Enqueue(item);
        }
    }
    public void HitProcess()
    {
        core.ChangeHealth(-1);
        SortButtons();
        CreateFromPrequeue();
    }
    private void SortButtons()
    {
        BasicButton to_del = showing.Dequeue();
        Destroy(to_del.button);
        int reps = showing.Count;
        for(int i = 0; i < reps; i++)
        {
            BasicButton item = showing.Dequeue();
            item.button.transform.Translate(new Vector2(-interval, 0));
            showing.Enqueue(item);
        }
    }
    IEnumerator PlayXSheet()
    {   // Caller : EnemyButtonManager > InputProcess
        xSheet.transform.position = showing.Peek().button.transform.position;
        xSheet.SetActive(true);
        xSheet_anim.SetTrigger("play");
        core.ChangeAttackCount(1);

        yield return new WaitForSeconds(0.2f);
        // after x play
        xSheet.SetActive(false);
    }
}

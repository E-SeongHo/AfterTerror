using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonInfo : MonoBehaviour
{
    // 0 : A, 1 : S, 2 : D
    [SerializeField] private GameObject[] buttonPrefabs = new GameObject[3];

    private EnemyController core; // 본체

    private Queue<BasicButton> prequeue = new Queue<BasicButton>();
    private Queue<BasicButton> showing = new Queue<BasicButton>();

    private Rigidbody2D rb;
    
    private float interval = 30f; // 버튼사이 간격
    private Vector2 add = new Vector2(-72f, 77f);

    private GameObject xSheet;
    private Animator xSheet_anim;

    private void Start()
    {
        core = gameObject.GetComponent<EnemyController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        EnqueueButtons(); // enqueue buttons to prequeue as much as max hp (from now equal start hp)
        
        xSheet = gameObject.transform.GetChild(0).gameObject;
        xSheet_anim = xSheet.GetComponent<Animator>();
    }

    public int GetTopIndex() { return showing.Peek().index; }
    private void EnqueueButtons()
    {
        for (int i = 0; i < core.GetMaxHealth(); i++)
        {
            int rand = Random.Range(0, 3);
            // rb.position + add == 첫 버튼 위치
            BasicButton to_push = new BasicButton(Instantiate(buttonPrefabs[rand], rb.position + add, Quaternion.identity), rand);
            to_push.button.transform.parent = gameObject.transform;
            to_push.button.SetActive(false);
            prequeue.Enqueue(to_push);
        }
        Debug.Log("prequeue : " + prequeue.Count);
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
    public void DeleteButton()
    {
        BasicButton to_del = showing.Dequeue();
        Destroy(to_del.button);
        SortButtons();
        CreateFromPrequeue();
    }
    private void SortButtons()
    {
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

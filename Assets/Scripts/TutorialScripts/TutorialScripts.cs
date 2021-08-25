using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScripts : MonoBehaviour
{
    public GameObject ShieldMan;
    public GameObject Enemy1;
    public GameObject AttackEnemy;
    public GameObject mine;
    public Image Black;
    public Sprite[] buttonsPrefabs;
    Animator animator;
    public Text talkText;
    public Image speechBubble;
    public GameObject x_prefab;
    public GameObject o_prefab;
    private Rigidbody2D rigidbody2D;
    public GameObject bullet;
    private int randNum;
    int waitingTime;
    float timer;
    float timeToStop = 2f;
    private float enemySpeed = 200f;
    private float bulletSpeed = 600f;
    public bool isPause = false;
    public bool isExecuted = false;
    GameObject buttonSpawner;
    IEnumerator enumerator;
    bullet_projectileManager bp;


    void Start()
    {
        Black = GameObject.Find("Black").GetComponent<Image>();
        talkText.text = "여기는 통신병, 임무 수행을 원활하게 진행하기 위해서 방침을 알려주겠다";
        timer = 0.0f;
        waitingTime = 2;
        Enemy1.SetActive(false);
        AttackEnemy.SetActive(false);
        x_prefab.gameObject.SetActive(false);
        o_prefab.gameObject.SetActive(false);

        mine.SetActive(false);
        // randNum = Random.Range(0, buttonsPrefabs.Length);
        // GetComponent<SpriteRenderer>().sprite = buttonsPrefabs[randNum];

        // buttonSpawner = Enemy1.transform.GetChild(0).gameObject;
        // buttonSpawner.SetActive(false);
        Debug.Log(Enemy1.transform.position.x);

        bullet = GameObject.Find("AttackEnemy").GetComponentInChildren<GameObject>();

    }

    void Update()
    {
        // speechBubble Scripts
        timer += Time.deltaTime;
        // Debug.Log("waitingTime is " + waitingTime + " current time is " + timer);

        if (timer > waitingTime)
        {
            talkText.alignment = TextAnchor.MiddleCenter;
            talkText.fontSize = 10;
            talkText.text = "아무 키나 눌러서\n대화 넘어가기";
            Debug.Log("text enabled");
        }

        // 아무 키나 눌러서 넘어가기
        if (Input.anyKeyDown)
        {
            Debug.Log("you pressed any key");

            // enemy
            Enemy1.SetActive(true);
            Debug.Log("enemy is active now");

            speechBubble.gameObject.SetActive(false);
            Debug.Log("speechBubble is not active now");
        }

        // enemyScripts starts
        if (Enemy1.activeSelf)
        {
            Vector3 pos = Enemy1.transform.position;
            Enemy1.transform.Translate(-1 * enemySpeed * Time.deltaTime, 0, 0);

            timePause1(Enemy1, pos);
            // StartCoroutine("makeButtonCoroutine");
            // StopCoroutine("makeButtonCoroutine");
        }


        // bullet guarding
        if (Enemy1.activeSelf == false)
        {
            AttackEnemy.SetActive(true);
            bullet.SetActive(true);
            if (bullet.activeSelf)
            {
                float moveX = -1 * bulletSpeed * Time.deltaTime;
                transform.Translate(moveX, 0, 0);
            }
            Vector3 attackEnemypos = AttackEnemy.transform.position;
            AttackEnemy.transform.Translate(-1 * enemySpeed * Time.deltaTime, 0, 0);
            Defend();
            if (bullet.transform.position.x <= ShieldMan.transform.position.x)
            {
                Destroy(bullet);
            }
        }

        if (AttackEnemy.activeSelf == false)
        {
            Escape();
        }
    }

    void timePause1(GameObject Enemy1, Vector3 position)
    {
        timeToStop -= Time.deltaTime;
        // Debug.Log(timeToStop);

        if (position.x < 878)
        {
            // do { isExecuted = true; } while (timeToStop >= 0.0f && isPause != true);
            isPause = true;
            Time.timeScale = 0;

            Color color = Black.color;
            color.a = 0.355f;
            Black.color = color;

            speechBubble.gameObject.SetActive(true);
            talkText.text = "적 머리 위에 떠 있는 버튼에 맞춰 누르면 공격을 할 수 있다.";
            // isExecuted = true;
        }
        enumerator = makeButtonCoroutine();
        StartCoroutine(enumerator);

        if (isPause)
        {
            StopCoroutine(enumerator);
        }
    }

    IEnumerator makeButtonCoroutine()
    {
        randNum = Random.Range(0, buttonsPrefabs.Length);
        Enemy1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = buttonsPrefabs[randNum];
        Debug.Log("randnum is " + randNum);

        string nowKey = Input.inputString;
        bool isRight = false;
        Debug.Log("nowkey is " + nowKey);


        if (randNum == 0)
        {
            Debug.Log("buttonprefab " + randNum);
            if (nowKey == "A")
            {
                isRight = true;
                Debug.Log("you pressed right button");
                o_prefab.gameObject.SetActive(true);
                DeleteButton();
            }
            else { x_prefab.gameObject.SetActive(true); isRight = false; }
        }
        else if (randNum == 1)
        {
            Debug.Log("buttonprefab " + randNum);
            if (nowKey == "D")
            {
                isRight = true;
                Debug.Log("you pressed right button");
                o_prefab.gameObject.SetActive(true);
                DeleteButton();
            }
            else { x_prefab.gameObject.SetActive(true); isRight = false; }
        }
        else if (randNum == 2)
        {
            Debug.Log("buttonprefab " + randNum);
            if (nowKey == "S")
            {
                isRight = true;
                Debug.Log("you pressed right button");
                o_prefab.gameObject.SetActive(true);
                DeleteButton();
            }
            else { x_prefab.gameObject.SetActive(true); isRight = false; }
            yield return null;
        }

        if (isRight == false)
        {
            Enemy1.transform.Translate(1184, -195, 0);
        }
    }
    void GenerateButton()
    {
        // buttonSpawner.SetActive(true);
        randNum = Random.Range(0, buttonsPrefabs.Length);
        Enemy1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = buttonsPrefabs[randNum];
        Debug.Log("randnum is " + randNum);

        string nowKey = Input.inputString;
        bool isRight = false;
        Debug.Log("nowkey is " + nowKey);


        if (randNum == 0)
        {
            Debug.Log("buttonprefab " + randNum);
            if (nowKey == "A")
            {
                isRight = true;
                Debug.Log("you pressed right button");
                o_prefab.gameObject.SetActive(true);
                DeleteButton();
            }
            else { x_prefab.gameObject.SetActive(true); isRight = false; }
        }
        else if (randNum == 1)
        {
            Debug.Log("buttonprefab " + randNum);
            if (nowKey == "D")
            {
                isRight = true;
                Debug.Log("you pressed right button");
                o_prefab.gameObject.SetActive(true);
                DeleteButton();
            }
            else { x_prefab.gameObject.SetActive(true); isRight = false; }
        }
        else if (randNum == 2)
        {
            Debug.Log("buttonprefab " + randNum);
            if (nowKey == "S")
            {
                isRight = true;
                Debug.Log("you pressed right button");
                o_prefab.gameObject.SetActive(true);
                DeleteButton();
            }
            else { x_prefab.gameObject.SetActive(true); isRight = false; }
        }
    }
    void DeleteButton()
    {
        Destroy(Enemy1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
    }

    void Defend()
    {
        // if (bp..transform.position.x <= -692)
        if (bp.bullet.transform.position.x <= -692)
        {
            Time.timeScale = 0;

            Color color = Black.color;
            color.a = 0.355f;
            Black.color = color;

            speechBubble.gameObject.SetActive(true);
            talkText.text = "총알이 충분히 가까워지면 space 버튼을 눌러 방어를 할 수 있다.";

            string rightkey = Input.inputString;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator = ShieldMan.GetComponent<Animator>();
                animator.SetTrigger("guard_on");
                Time.timeScale = 1;

                timeToStop = 2f;
                timeToStop -= Time.deltaTime;

                if (timeToStop <= 0.0f)
                {
                    AttackEnemy.SetActive(false);
                }
            }

            if (rightkey != "Space")
            {
                talkText.text = "정말 실망스럽군, 다시 한 번 시도해봐라. 만약 실전이었다면 다음은 없었을 것이다.";
                AttackEnemy.transform.Translate(1184, -195, 0);
            }
        }


    }
    void Escape()
    {
        mine.SetActive(true);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("jump");
        }

        talkText.text = "이것으로 기본적인 훈련은 끝이 났다. 건투를 빌겠다.";
    }


}
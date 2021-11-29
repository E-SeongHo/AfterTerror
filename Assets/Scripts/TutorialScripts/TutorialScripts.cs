using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class TutorialScripts : MonoBehaviour
{
    [Header("--- Tutorial UI ---")]
    public GameObject blackPanel;
    public GameObject speechBubble;
    public Text talkText;
    public Sprite[] buttonSpriteArr;

    [Header("Shield Man")]
    public GameObject ShieldMan;
    public GameObject spaceButton;
    public GameObject normalHand;
    public GameObject fireHand;
    public GameObject jumpHand;
    public GameObject shieldEffect;

    [Header("--- Enemy_1 ---")]
    public GameObject Enemy1;
<<<<<<< HEAD
    public GameObject enemy1_Button;
    public GameObject enemy1_O;
    public GameObject enemy1_X;
=======
    public GameObject AttackEnemy;
    public GameObject mine;
    public Image Black;
    public Sprite[] buttonsPrefabs;
    public Animator animator;
    public Animator EnemyAnimator;
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
    DistantBackgroundRepeating dbr;
    MapRepeating mr;


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
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151

    [Header("--- Enemy_2 ---")]
    public GameObject enemy2;
    public GameObject bulletObj;

<<<<<<< HEAD
    [Header("Mine")]
    public GameObject mineObj;
    public GameObject mineButton;

    private KeyCode keyCode;

    private int tutorialIndex = -1;
    private int randNum;
=======
        // buttonSpawner = Enemy1.transform.GetChild(0).gameObject;
        // buttonSpawner.SetActive(false);
        Debug.Log(Enemy1.transform.position.x);
        dbr = GameObject.Find("DistantMap").GetComponent<DistantBackgroundRepeating>();
        mr = GameObject.Find("FrontMap_Tutorials").GetComponent<MapRepeating>();
        // bullet = AttackEnemy.transform.GetChild(0).GetComponent<GameObject>();
        Debug.Log("start");
    }
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151



    private void Start()
    {
        StartCoroutine(TutorialCor_0());
    }
    private void Update()
    {
        if (tutorialIndex == -1)
        {
            return;
        }
        // Debug.Break();

        if (tutorialIndex == 1)
        {
            if (Input.GetKeyDown(keyCode))
            {
                speechBubble.SetActive(false);
                StartCoroutine(TutorialCor_2());
            }
            else if (Input.anyKeyDown)
            {
                StartCoroutine(TutorialCor_1(true));
            }
        }
        else if (tutorialIndex == 2)
        {
            if (Input.GetKeyDown(keyCode))
            {
                Time.timeScale = 1;

                tutorialIndex = -1;

                bulletObj.SetActive(false);
                spaceButton.SetActive(false);
                speechBubble.SetActive(false);

<<<<<<< HEAD
                ShieldMan.GetComponent<Animator>().SetTrigger("guard_on");
                enemy2.GetComponent<Animator>().Play("Runaway");

                StartCoroutine(TutorialCor_4(false));
            }
            else if (Input.anyKeyDown)
            {
                tutorialIndex = -1;

                bulletObj.transform.localPosition = bulletObj.GetComponent<bullet_projectileManager>().startPos;
                bulletObj.SetActive(false);
=======
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Enemy1.SetActive(false);
        }

        // enemyScripts starts
        // if (Enemy1.activeSelf)
        // {
        //     Vector3 pos = Enemy1.transform.position;
        //     Enemy1.gameObject.transform.Translate(-1 * enemySpeed * Time.deltaTime, 0, 0);
        //     Debug.Log(-1 * enemySpeed * Time.deltaTime);

        //     timePause1(Enemy1, pos);

        //     GenerateButton();

        //     // StartCoroutine("makeButtonCoroutine");
        //     // StopCoroutine("makeButtonCoroutine");
        // }

>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151

                StartCoroutine(TutorialCor_3(true));
            }
        }
        else if (tutorialIndex == 3)
        {
<<<<<<< HEAD
            if (Input.GetKeyDown(keyCode))
            {
                Time.timeScale = 1;

                tutorialIndex = -1;

                bulletObj.SetActive(false);
                mineButton.SetActive(false);
                speechBubble.SetActive(false);

                normalHand.SetActive(false);
                jumpHand.SetActive(false);
                ShieldMan.GetComponent<Animator>().Play("Jump");

                //나중에 특능 관련 튜토리얼 코루틴 추가할 자리

                tutorialIndex = -1;

                StartCoroutine(TutorialCor_5());
            }
            else if (Input.anyKeyDown)
            {
                tutorialIndex = -1;
=======
            AttackEnemy.SetActive(true);
            Vector3 attackEnemypos = AttackEnemy.transform.position;
            AttackEnemy.transform.Translate(-1 * enemySpeed * Time.deltaTime, 0, 0);

            if (AttackEnemy.transform.position.x <= 878)
            {
                dbr.speed = 0;
                mr.speed = 0;
                enemySpeed = 0;
                animator.speed = 0;

                // bullet.gameObject.SetActive(true);

                // if (bullet.gameObject.activeSelf)
                // {
                //     float moveX = -1 * bulletSpeed * Time.deltaTime;
                //     transform.Translate(moveX, 0, 0);
                // }
                BulletFlies();

                Defend();
                if (bullet.transform.position.x <= ShieldMan.transform.position.x)
                {
                    Destroy(bullet);
                }
            }
            // bullet = AttackEnemy.transform.GetChild(0).GetComponent<GameObject>();
        }
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151

                mineButton.SetActive(false);
                mineObj.transform.localPosition = mineObj.GetComponent<MineScripts>().startPos;
                mineObj.SetActive(false);

                StartCoroutine(TutorialCor_4(true));
            }
        }
    }

<<<<<<< HEAD

    private IEnumerator TutorialCor_0()
=======
    void BulletFlies()
    {
        // GameObject EnemyBullet = AttackEnemy.transform.GetChild("bullet_0").GetComponent<GameObject>();
        // GameObject EnemyBullet = GameObject.Find("bullet_0").GetComponent<GameObject>();
        GameObject EnemyBullet = AttackEnemy.gameObject.transform.FindChild("bullet_0").GetComponent<GameObject>();
        EnemyBullet.transform.Translate(-1 * bulletSpeed * Time.deltaTime, 0, 0);
    }

    void timePause1(GameObject Enemy1, Vector3 position)
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151
    {
        yield return new WaitForSeconds(2f);

<<<<<<< HEAD
        speechBubble.SetActive(true);
=======
        if (Enemy1.transform.position.x < 878)
        {
            // do { isExecuted = true; } while (timeToStop >= 0.0f && isPause != true);
            isPause = true;
            // Time.timeScale = 0;
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151

        yield return new WaitUntil(() => Input.anyKeyDown);

<<<<<<< HEAD
        speechBubble.SetActive(false);
        Enemy1.SetActive(true);

        yield return new WaitForSeconds(2f);

        StartCoroutine(TutorialCor_1(false));
=======

            speechBubble.gameObject.SetActive(true);
            talkText.text = "적 머리 위에 떠 있는 버튼에 맞춰 누르면 공격을 할 수 있다.";
            mr.speed = 0;
            dbr.speed = 0;
            enemySpeed = 0;
            animator.speed = 0;

            // isExecuted = true;
            // enumerator = makeButtonCoroutine();
            // StartCoroutine(enumerator);
            // StartCoroutine(makeButtonCoroutine());

            timeToStop = 2f;
            timeToStop -= Time.deltaTime;

        }


>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151
    }

    private IEnumerator TutorialCor_1(bool isWrong)
    {
<<<<<<< HEAD
        tutorialIndex = 0;
=======
        randNum = Random.Range(0, buttonsPrefabs.Length);
        Enemy1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = buttonsPrefabs[randNum];
        Debug.Log("randnum is " + randNum);

        // string nowKey = Input.inputString;
        bool isRight = false;
        // Debug.Log("nowkey is " + nowKey);
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151

        blackPanel.SetActive(true);
        speechBubble.SetActive(true);

<<<<<<< HEAD
        if (isWrong == true)
        {
            enemy1_X.SetActive(true);

            yield return null;

            talkText.text = "정말 실망스럽군, 다시 한번 시도해봐라. 만약 실전 이였다면 다음은 없었을 것이다.\n(아무 키나 눌러서 넘어가기)";

            yield return new WaitUntil(() => Input.anyKeyDown);
        }
        talkText.text = "적 머리 위에 떠 있는 버튼에 맞춰 누르면 공격을 할 수 있다";

        enemy1_X.SetActive(false);
        ButtonGenerate();

        Time.timeScale = 0;
=======
        if (randNum == 0)
        {
            Debug.Log("buttonprefab " + randNum);
            if (Input.GetKeyDown(KeyCode.A))
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
            if (Input.GetKeyDown(KeyCode.D))
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
            if (Input.GetKeyDown(KeyCode.S))
            {
                isRight = true;
                Debug.Log("you pressed right button");
                o_prefab.gameObject.SetActive(true);
                DeleteButton();
            }
            else { x_prefab.gameObject.SetActive(true); isRight = false; }
        }

        // if (isRight == false)
        // {
        //     Enemy1.transform.Translate(1184, -195, 0);
        // }

        if (isRight)
        {
            mr.speed = 200f;
            dbr.speed = 50f;
            enemySpeed = 200f;
            animator.speed = 1;
        }
        yield return null;
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151
    }

    private IEnumerator TutorialCor_2()
    {
        Time.timeScale = 1;

        tutorialIndex = -1;

        enemy1_O.SetActive(true);

        //총 쏘는 애니메이션 추가

        Enemy1.transform.parent.GetComponent<EnemyDieTrigger>().isDie = true;
        Enemy1.GetComponent<Animator>().Play("Die");

        yield return new WaitForSeconds(0.7f);

        Enemy1.transform.parent.gameObject.SetActive(false);
        Enemy1.SetActive(false);

        StartCoroutine(TutorialCor_3(false));
    }

    private IEnumerator TutorialCor_3(bool isWrong)
    {
        if (isWrong == true)
        {
            Time.timeScale = 1;

            yield return null;

            spaceButton.SetActive(false);
            talkText.text = "정말 실망스럽군, 다시 한번 시도해봐라. 만약 실전 이였다면 다음은 없었을 것이다.\n(아무 키나 눌러서 넘어가기)";

            enemy2.GetComponent<Animator>().Play("Tutorial_Enemy_Move");

            yield return new WaitUntil(() => Input.anyKeyDown);

            speechBubble.SetActive(false);
        }
        else
        {
            enemy2.SetActive(true);

            yield return new WaitForSeconds(2f);
        }

        enemy2.GetComponent<Animator>().Play("Fire");
        bulletObj.SetActive(true);

        yield return new WaitForSeconds(1.6f);

        blackPanel.SetActive(true);
        speechBubble.SetActive(true);
        talkText.text = "총알이 충분히 가까워지면 Space 버튼을 눌러 방어를 할 수 있다.";
        spaceButton.SetActive(true);

        keyCode = KeyCode.Space;
        tutorialIndex = 2;

        Time.timeScale = 0;
    }

    private IEnumerator TutorialCor_4(bool isWrong)
    {
<<<<<<< HEAD
        if (isWrong == true)
=======
        // if (bp..transform.position.x <= -692)
        if (bullet.transform.position.x <= -692)
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151
        {
            Time.timeScale = 1;

            yield return null;

            spaceButton.SetActive(false);
            talkText.text = "정말 실망스럽군, 다시 한번 시도해봐라. 만약 실전 이였다면 다음은 없었을 것이다.\n(아무 키나 눌러서 넘어가기)";

            yield return new WaitUntil(() => Input.anyKeyDown);

            speechBubble.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            enemy2.SetActive(false);
        }

        mineObj.SetActive(true);
        FindObjectOfType<MapRepeating>().speed = 500f;

<<<<<<< HEAD
        yield return new WaitForSeconds(3.7f);

        speechBubble.SetActive(true);
        talkText.text = "장애물이 충분히 가까워지면 버튼을 눌러 회피 할 수 있다";
=======
            // if (rightkey != "Space")
            // {
            //     talkText.text = "정말 실망스럽군, 다시 한 번 시도해봐라. 만약 실전이었다면 다음은 없었을 것이다.";
            //     AttackEnemy.transform.Translate(1184, -195, 0);
            // }
        }
>>>>>>> af8e0bf0c0bbb0237b014c1a9a0d054aa2642151

        mineButton.SetActive(true);

        keyCode = KeyCode.UpArrow;
        tutorialIndex = 3;

        Time.timeScale = 0;
    }


    private void ButtonGenerate()
    {
        tutorialIndex = 1;

        randNum = Random.Range(0, buttonSpriteArr.Length);
        enemy1_Button.SetActive(true);
        enemy1_Button.GetComponent<SpriteRenderer>().sprite = buttonSpriteArr[randNum];

        if (randNum == 0)
        {
            keyCode = KeyCode.A;
        }
        else if (randNum == 1)
        {
            keyCode = KeyCode.D;
        }
        else if (randNum == 2)
        {
            keyCode = KeyCode.S;
        }
    }

    private IEnumerator TutorialCor_5()
    {
        yield return new WaitForSeconds(2f);

        speechBubble.SetActive(true);
        talkText.text = "이것으로 기본적인 훈련은 끝났다. 건투를 빌겠다.";

        yield return null;
    }
}
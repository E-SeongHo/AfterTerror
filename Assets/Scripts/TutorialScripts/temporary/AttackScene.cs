using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackScene : MonoBehaviour
{
    public GameObject shieldMan;
    public GameObject Enemy1;
    public Image Black;
    public Sprite[] buttonPrefabs;
    public Text talkText;
    public Image speechBubble;
    public GameObject x_prefab;
    public GameObject o_prefab;
    public Animator animator;
    int waitingTime = 2;
    float timer = 0f;
    private float enemySpeed = 200f;
    DistantBackgroundRepeating dbr;
    MapRepeating mr;
    EnemyButtonCreator enemyButtonCreator;

    void Start()
    {
        Black = GameObject.Find("Black").GetComponent<Image>();
        talkText.text = "여기는 통신병, 임무 수행을 원활하게 진행하기 위해서 방침을 알려주겠다";
        timer = 0.0f;
        waitingTime = 2;
        Enemy1.SetActive(false);

        x_prefab.gameObject.SetActive(false);
        o_prefab.gameObject.SetActive(false);

        dbr = GameObject.Find("DistantMap").GetComponent<DistantBackgroundRepeating>();
        mr = GameObject.Find("FrontMap_Tutorials").GetComponent<MapRepeating>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

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

        if (Enemy1.activeSelf)
        {
            Vector3 pos = Enemy1.transform.position;
            Enemy1.gameObject.transform.Translate(-1 * enemySpeed * Time.deltaTime, 0, 0);
        }

        if (Enemy1.transform.position.x <= 878)
        {
            dbr.speed = 0;
            mr.speed = 0;
            enemySpeed = 0;
            animator.speed = 0;
        }

        GameObject buttonSpawner = GameObject.Find("Enemy1").GetComponentInChildren<GameObject>();

        if (buttonSpawner.activeSelf == false)
        {
            Enemy1.SetActive(false);
        }
    }
}

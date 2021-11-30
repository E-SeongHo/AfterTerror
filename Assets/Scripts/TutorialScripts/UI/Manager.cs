using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    private float enemySpeed = 200f;
    public bool isPause = false;
    public bool enemyOff = false;
    public Text talkText;
    public Image speechBubble;
    private float timer;
    private int waitingTime;
    EnemyControl enemyControl;
    EnemyButtonCreator enemyButtonCreator;
    FadeScript fade;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Black;

    public bool InputAnyKey;

    void Start()
    {
        // image = GetComponent<Image>();
        // image.gameObject.SetActive(false);
        talkText.text = "여기는 통신병, 임무 수행을 원활하게 진행하기 위해서 방침을 알려주겠다";
        timer = 0.0f;
        waitingTime = 2;
        enemyControl = Enemy.gameObject.GetComponent<EnemyControl>();
        Enemy.SetActive(false);
        enemyButtonCreator = Enemy.gameObject.GetComponent<EnemyButtonCreator>();
        fade = Black.gameObject.GetComponent<FadeScript>();
        // enemyControl.isPause = false;
        Debug.Log("enemyControl's variable isPause is " + enemyControl.isPause);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log("now time is " + timer + "in this code");

        if (timer > waitingTime)
        {
            talkText.alignment = TextAnchor.MiddleCenter;
            talkText.fontSize = 10;
            talkText.text = "아무 키나 눌러서\n대화 넘어가기";
            Debug.Log("text enabled");
        }

        if (Input.anyKeyDown)
        {
            if (InputAnyKey)
                return;
            InputAnyKey = true;
            Debug.Log("you pressed any key buttons");
            // Debug.Log("Manger's isPause is correctly referenced: " + enemyControl.isPause);

            // enemy scripts

            Enemy.SetActive(true);
            Debug.Log("enemy is active now");
            enemyOff = true;

            // speechBubble scripts
            speechBubble.gameObject.SetActive(false);
            Debug.Log("speechBubble setActive(false)");

        }

        // speechBubble talks
        if (enemyControl.isPause == true)
        {
            timer = 0.0f;
            timer += Time.deltaTime;
            // Debug.Log("isPause is true");
            speechBubble.gameObject.SetActive(true);
            talkText.text = "적 머리 위에 떠 있는 버튼에 맞춰 누르면 공격을 할 수 있다.";
            // enemyControl.isPause = true;
            // if (timer > waitingTime)
            // {
            // }
            speechBubble.gameObject.SetActive(false);

            if (enemyButtonCreator.right == true)
            {
                talkText.text = "잘했다. 다음 훈련으로 넘어가도록 하지";

            }
            if (enemyButtonCreator.right == false)
            {
                fade.image.gameObject.SetActive(false);
                speechBubble.gameObject.SetActive(true);
                talkText.fontSize = 9;
                talkText.text = "정말 실망스럽군, 다시 시도해봐라. 만약 실전이였다면 다음은 없었을 것이다.";
            }
        }


    }
}

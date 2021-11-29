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
    public GameObject enemy1_Button;
    public GameObject enemy1_O;
    public GameObject enemy1_X;

    [Header("--- Enemy_2 ---")]
    public GameObject enemy2;
    public GameObject bulletObj;

    [Header("Mine")]
    public GameObject mineObj;
    public GameObject mineButton;

    private KeyCode keyCode;

    private int tutorialIndex = -1;
    private int randNum;



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

                ShieldMan.GetComponent<Animator>().SetTrigger("guard_on");
                enemy2.GetComponent<Animator>().Play("Runaway");

                StartCoroutine(TutorialCor_4(false));
            }
            else if (Input.anyKeyDown)
            {
                tutorialIndex = -1;

                bulletObj.transform.localPosition = bulletObj.GetComponent<bullet_projectileManager>().startPos;
                bulletObj.SetActive(false);

                StartCoroutine(TutorialCor_3(true));
            }
        }
        else if (tutorialIndex == 3)
        {
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

                mineButton.SetActive(false);
                mineObj.transform.localPosition = mineObj.GetComponent<MineScripts>().startPos;
                mineObj.SetActive(false);

                StartCoroutine(TutorialCor_4(true));
            }
        }
    }


    private IEnumerator TutorialCor_0()
    {
        yield return new WaitForSeconds(2f);

        speechBubble.SetActive(true);

        yield return new WaitUntil(() => Input.anyKeyDown);

        speechBubble.SetActive(false);
        Enemy1.SetActive(true);

        yield return new WaitForSeconds(2f);

        StartCoroutine(TutorialCor_1(false));
    }

    private IEnumerator TutorialCor_1(bool isWrong)
    {
        tutorialIndex = 0;

        blackPanel.SetActive(true);
        speechBubble.SetActive(true);

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
        if (isWrong == true)
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

        yield return new WaitForSeconds(3.7f);

        speechBubble.SetActive(true);
        talkText.text = "장애물이 충분히 가까워지면 버튼을 눌러 회피 할 수 있다";

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
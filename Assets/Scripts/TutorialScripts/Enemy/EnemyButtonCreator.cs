using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonCreator : MonoBehaviour
{
    public Sprite[] buttonsprefabs;
    public GameObject x_prefab;
    public GameObject o_prefab;
    // public EnemyControl enemyControl;
    private int randNum;
    public bool right;
    public bool pressedKey;
    // [SerializeField] Manager manager;
    GameObject enemyControl;

    private void Awake()
    {
        Debug.Log(buttonsprefabs.Length);
    }
    void Start()
    {
        // 여기서 manager 가져온다음에 접근하면 될듯 ㅇㅋ 임시 코드
        // manager = GameObject.Find("Canvas").GetComponent<Manager>();
        GameObject enemyControl = GameObject.Find("Enemy1").GetComponent<GameObject>();
        // x_prefab = GameObject.Find("O-Sheet_0").GetComponent<GameObject>();
        // o_prefab = GameObject.Find("X-Sheet_0").GetComponent<GameObject>();
        randNum = Random.Range(0, buttonsprefabs.Length);
        Debug.Log(randNum);

        x_prefab.gameObject.SetActive(false);
        o_prefab.gameObject.SetActive(false);

        try
        {
            GetComponent<SpriteRenderer>().sprite = buttonsprefabs[randNum];
        }
        catch (System.IndexOutOfRangeException exception)
        {
            Debug.Log(exception.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (randNum == 0)
        {
            Debug.Log("buttonsprefabs[0]");
            if (Input.GetKeyDown(KeyCode.A))
            {
                DeleteButton();
                right = true;
                Debug.Log("buttonsprefabs[0] " + right);
                o_prefab.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                right = false;
                x_prefab.gameObject.SetActive(true);
            }
        }
        else if (randNum == 1)
        {
            Debug.Log("buttonsprefabs[1]");
            if (Input.GetKeyDown(KeyCode.D))
            {
                DeleteButton();
                right = true;
                Debug.Log("buttonsprefabs[1] " + right);
                o_prefab.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
            {
                right = false;
                x_prefab.gameObject.SetActive(true);
            }
        }
        else if (randNum == 2)
        {
            Debug.Log("buttonsprefabs[2]");
            if (Input.GetKeyDown(KeyCode.S))
            {
                DeleteButton();
                right = true;
                Debug.Log("buttonsprefabs[2] " + right);
                o_prefab.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                right = false;
                x_prefab.gameObject.SetActive(true);
            }
        }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log("you pressed space key");
        //     // if (enemyControl.isPause)
        //     // {
        //     // }
        // }
    }

    public void ChangeButtons()
    {
        Vector3 EnemyPosition = GameObject.Find("Enemy1").GetComponent<Transform>().position;
        Vector3 FixedPosition = EnemyPosition;
        FixedPosition.y = EnemyPosition.y + 170f;

        randNum = Random.Range(0, buttonsprefabs.Length);
        GetComponent<SpriteRenderer>().sprite = buttonsprefabs[randNum];
    }

    void DeleteButton()
    {
        Destroy(gameObject);
    }
}

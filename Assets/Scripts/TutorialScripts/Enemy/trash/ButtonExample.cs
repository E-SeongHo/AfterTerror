using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExample : MonoBehaviour
{
    public Sprite[] buttonsprefabs;
    public EnemyControl enemyControl;
    private int randNum;
    public bool right;
    public bool pressedKey;
    void Start()
    {
        enemyControl = GameObject.Find("Enemy1").GetComponent<EnemyControl>();
        randNum = Random.Range(0, buttonsprefabs.Length);
        GetComponent<SpriteRenderer>().sprite = buttonsprefabs[randNum];
    }

    // Update is called once per frame
    void Update()
    {
        if (randNum == 0)
        {
            Debug.Log("buttonsprefabs[0]");
            if (Input.GetKeyDown(KeyCode.A))
            {
                ChangeButtons();
                right = true;
                Debug.Log("buttonsprefabs[0]" + right);
            }
            else
            {
                right = false;
            }
        }
        if (randNum == 1)
        {
            Debug.Log("buttonsprefabs[1]");
            if (Input.GetKeyDown(KeyCode.D))
            {
                ChangeButtons();
                right = true;
                Debug.Log("buttonsprefabs[1]" + right);
            }
            else
            {
                right = false;
            }
        }
        if (randNum == 2)
        {
            Debug.Log("buttonsprefabs[2]");
            if (Input.GetKeyDown(KeyCode.S))
            {
                ChangeButtons();
                right = true;
                Debug.Log("buttonsprefabs[2]" + right);
            }
            else
            {
                right = false;
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
        Vector3 position = enemyControl.transform.position;
        Vector3 FixedPosition = position;
        FixedPosition.y = position.y + 170f;

        randNum = Random.Range(0, buttonsprefabs.Length);
        GetComponent<SpriteRenderer>().sprite = buttonsprefabs[randNum];
    }
}

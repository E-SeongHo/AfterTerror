using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    private float enemySpeed = 200f;
    public bool isPause = false;
    public GameObject Enemy;
    public Image Black;
    private Rigidbody2D rb = null;
    private GameObject myButton = null;
    // private Rigidbody2D rb;
    Manager manager;

    // private void Awake()
    // {
    // }

    private void OnEnable()
    {
        Debug.Log("켜짐");
    }
    void Start()
    {
        // Black = GetComponent<Image>();
        Black = GameObject.Find("Black").GetComponent<Image>();
        // Vector3 pos;
        // pos = transform.position;
        // Debug.Log(pos.x);
        Debug.Log("Enemy off");
        //Enemy.SetActive(false);
        // manager = GameObject.Find("Canvas").GetComponent<Manager>();
    }

    void Update()
    {
        Vector3 pos = transform.position;
        transform.Translate(-1 * enemySpeed * Time.deltaTime, 0, 0);

        if (pos.x < 878)
        // 위치가 같아지는 곳이 878
        {
            isPause = true;
            if (isPause)
            {
                Debug.Log("isPause is " + isPause);
                Time.timeScale = 0;
                // isPause = false;

                // Color color = Black.color;
                // if (color.a < 1 && color.a <= 0.355f)
                // {
                //     color.a += Time.deltaTime;
                // }
                // Black.color = color;
            }
        }
        if (manager.enemyOff)
        {
            Enemy.SetActive(true);
        }
    }

    public void GenerateButtons(GameObject button)
    {
        Vector2 position = rb.position + Vector2.up * 70f + Vector2.left * 65f;
        GameObject newButton = Instantiate(button, position, Quaternion.identity);
        myButton = newButton;
        myButton.transform.parent = gameObject.transform;
    }
}

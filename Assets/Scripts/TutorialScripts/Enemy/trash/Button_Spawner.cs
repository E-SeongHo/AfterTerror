using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Spawner : MonoBehaviour
{
    private int randNum;
    public Sprite[] button_prefab;
    void Start()
    {
        GameObject buttonSpawner = GameObject.Find("buttonSpawner").GetComponent<GameObject>();
        randNum = Random.Range(0, button_prefab.Length);
        GetComponent<SpriteRenderer>().sprite = button_prefab[randNum];
        buttonSpawner.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeSprite();
        }
    }

    void ChangeSprite()
    {
        randNum = Random.Range(0, button_prefab.Length);
        GetComponent<SpriteRenderer>().sprite = button_prefab[randNum];
    }
}

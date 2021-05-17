using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject Sprite;
    KeyCode[] keyCodes = { KeyCode.A, KeyCode.S, KeyCode.D };
    KeyCode currentKey;
    [SerializeField] Sprite[] buttonImages;
    [SerializeField] float spawnRate = 0f;
    [SerializeField] SpriteRenderer spriteRenderer;
    //버튼 이미지 삽입
    private int SpawnNum;
    private int beforeSpawnNum = 99;
    public float maxHealth = 3;
    [SerializeField] float health = 0;
    GameObject bullet;
    float time = 0;
    [SerializeField] int AttackCount = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (Sprite == null)
            Sprite = spriteRenderer.transform.gameObject;
    }
    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputCurrentKey())
        {
            SetSprite();
            AttackCount++;
            time = 0f;
        }


        if (time >= spawnRate)
        {
            SetSprite();
            time = 0f;
        }
        time += Time.deltaTime;
    }
    void Init()
    {
        SetSprite();
    }

    bool inputCurrentKey()
    {
        bool answer = false;
        switch (SpawnNum)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.A))
                    answer = true;
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.S))
                    answer = true;
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.D))
                    answer = true;
                break;
        }
        return answer;
    }

    void SetSprite()
    {
        SpawnNum = Random.Range(0, 3);
        while (beforeSpawnNum == SpawnNum)
        {
            SpawnNum = Random.Range(0, 3);
        }
        currentKey = keyCodes[SpawnNum];
        Sprite.SetActive(true);
        spriteRenderer.sprite = buttonImages[SpawnNum];
    }
}

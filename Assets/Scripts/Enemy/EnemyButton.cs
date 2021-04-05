using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySprites : MonoBehaviour
{
    // EnemyMoving em = new EnemySprites();
    [SerializeField] GameObject Sprite;
    KeyCode[] keyCodes = { KeyCode.A, KeyCode.S, KeyCode.D };
    KeyCode currentKey;
    [SerializeField] SpriteRenderer spriteRenderer;
    //버튼 이미지 삽입
    [SerializeField] Sprite[] sprites;
    //public GameObject aSprite, sSprite, dSprite;
    [SerializeField] float spawnRate = 3f; // spawn once per 3 seconds
    private int beforeSpawnNum = 99;
    private int SpawnNum;
    float time = 0;

    //접근제한자 한정지어서 사용안하면 나중에 의도하지 않게 수정됨 앵간해서 private로 하고 외부에서 접근가능한 함수나
    //get,set이용해서 변경할것.
    //인스펙터에서 입력 받고싶은 경우 serializeField사용

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

    void Init()
    {
        //나오자 마자 설정해줌.
        SetSprite();
        // Invoke("SetSprite", spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputCurrentKey())
        {
            SetSprite();
            time = 0f;
        }


        if (time >= spawnRate)
        {
            SetSprite();
            time = 0f;
        }
        time += Time.deltaTime;
    }
    bool inputCurrentKey()
    {
        //안되면 밑에 switch
        if (Input.GetKeyDown(currentKey))
        {
            beforeSpawnNum = SpawnNum;
            return true;
        }
        return false;

        // switch (SpawnNum)
        // {
        //     case 1:
        //         if (Input.GetKeyDown(KeyCode.A))
        //             answer = true;
        //         break;
        //     case 2:
        //         if (Input.GetKeyDown(KeyCode.S))
        //             answer = true;
        //         break;
        //     case 3:
        //         if (Input.GetKeyDown(KeyCode.D))
        //             answer = true;
        //         break;
        // }
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
        // 직접적으로 수정함.
        spriteRenderer.sprite = sprites[SpawnNum];
    }


}
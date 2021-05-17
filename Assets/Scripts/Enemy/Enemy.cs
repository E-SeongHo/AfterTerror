using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject healthBarUI;
    private GameObject canvas;
    private Rigidbody2D rb;
    RectTransform hpBar;
    public float height = 1.7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hpBar = Instantiate(healthBarUI, canvas.transform).GetComponent<RectTransform>();
        // 체력바를 생성하되 canvas의 자식으로 생성하고, 체력바의 위치변경을 쉽게 하기 위해서 hpBar에 저장한다.
        // instantiate는 게임 오브젝트를 생성하는 함수. 2번째 파라미터에 transform을 주면 그 transform 안으로 들어감.
        // 

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _hpBarPos =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        // WorldToScreenPoint = 월드좌표를 스크린좌표로 바꿔주는 함수.


    }
}

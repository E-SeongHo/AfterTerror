using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // 플레이어가 위치한 다음 블록 x가 900됐을 때 다음 블록 가져오면 될듯

    [SerializeField] private GameObject front_noEnemy;
    // 플레이어가 위치한 블록
    private GameObject nowFront;
    private GameObject nowMiddle;
    // 다음 블록
    private GameObject nextFront;
    private GameObject nextMiddle;
    // 생성 블록
    private GameObject newFront;
    private GameObject newMiddle;

    // Used Map Blocks Buffer
    private Queue<GameObject> usedFrontBuffer = new Queue<GameObject>();
    private Queue<GameObject> usedMiddleBuffer = new Queue<GameObject>();

    private int bufferLimit = 3;

    private void Start() // MapBlockSpawner의 pool에 객체가 들어선 후 호출되어야 한다.
    {
        /*nowFront = MapBlockSpawner.Instance.AllocateFrontBlock();
        nextFront = MapBlockSpawner.Instance.AllocateFrontBlock();
        SetBlockPosition(nowFront, nextFront);
        nowFront.SetActive(true);
        nextFront.SetActive(true);*/
        nowFront = Instantiate(front_noEnemy);
        nowFront.SetActive(false);
        nextFront = Instantiate(front_noEnemy);
        nextFront.SetActive(false);
        SetBlockPosition(nowFront, nextFront);
        nowFront.SetActive(true);
        nextFront.SetActive(true);

        nowMiddle = MapBlockSpawner.Instance.AllocateMiddleBlock();
        nextMiddle = MapBlockSpawner.Instance.AllocateMiddleBlock();
        SetBlockPosition(nowMiddle, nextMiddle);
        nowMiddle.SetActive(true);
        nextMiddle.SetActive(true);
    }
    private void Update()
    {
        if(nextFront.transform.position.x <= 1160) // 960 + 200(여유분)
        {
            newFront = MapBlockSpawner.Instance.AllocateFrontBlock();
            SetBlockPosition(nextFront, newFront);
            newFront.SetActive(true);
            usedFrontBuffer.Enqueue(nowFront);
            // 변수 Swap
            nowFront = nextFront;
            nextFront = newFront;
        }
        /*if(nowFront.transform.position.x <= -2120) // 1920 + 200(여유분)
        {
            
        }*/
        if(usedFrontBuffer.Count >= bufferLimit)
        {
            ClearFrontBuffer();
        }
        if (nextMiddle.transform.position.x <= 1160) // 960 + 200(여유분)
        {
            newMiddle = MapBlockSpawner.Instance.AllocateMiddleBlock();
            SetBlockPosition(nextMiddle, newMiddle);
            newMiddle.SetActive(true);
            usedMiddleBuffer.Enqueue(nowMiddle);
            // 변수 Swap
            nowMiddle = nextMiddle;
            nextMiddle = newMiddle;
        }
        /*if(nowFront.transform.position.x <= -2120) // 1920 + 200(여유분)
        {
            
        }*/
        if (usedMiddleBuffer.Count >= bufferLimit)
        {
            ClearMiddleBuffer();
        }
    }
    private void ClearFrontBuffer()
    {
        for(int i = 0; i < usedFrontBuffer.Count-1; i++) // now는 제외하고 제거
        {
            Destroy(usedFrontBuffer.Peek());
            usedFrontBuffer.Dequeue();
        }
    }
    private void ClearMiddleBuffer()
    {
        for (int i = 0; i < usedMiddleBuffer.Count - 1; i++) // now는 제외하고 제거
        {
            Destroy(usedMiddleBuffer.Peek());
            usedMiddleBuffer.Dequeue();
        }
    }
    // 다음 위치 잡기
    private void SetBlockPosition(GameObject now, GameObject next)
    {
        // 바닥 1개 x축 720픽셀
        // 한 블록당 바닥 3개 -> 2160
        Vector3 pos = now.transform.position;
        pos += new Vector3(2160f, 0, 0);
        next.transform.Translate(pos);
    }

    public void StopMapMove()
    {
        nowFront.SetActive(false);
        nextFront.SetActive(false);
        newFront.SetActive(false);
    }
} 


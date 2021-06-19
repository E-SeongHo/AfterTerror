using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // 플레이어가 위치한 다음 블록 x가 900됐을 때 다음 블록 가져오면 될듯
    

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
    private bool instantiate = false;
    private bool swapped = false;

    private void Start() // MapBlockSpawner의 pool에 객체가 들어선 후 호출되어야 한다.
    {
        nowFront = MapBlockSpawner.Instance.AllocateFrontBlock();
        nextFront = MapBlockSpawner.Instance.AllocateFrontBlock();
        SetBlockPosition(nowFront, nextFront);
        nowFront.SetActive(true);
        nextFront.SetActive(true);
    }
    private void Update()
    {
        if(nextFront.transform.position.x <= 1160 && !instantiate) // 960 + 200(여유분)
        {
            instantiate = true;
            newFront = MapBlockSpawner.Instance.AllocateFrontBlock();
            SetBlockPosition(nextFront, newFront);
            newFront.SetActive(true);
            usedFrontBuffer.Enqueue(nowFront);
            // 변수 Swap
            nowFront = nextFront;
            nextFront = newFront;

            instantiate = false;
        }
        /*if(nowFront.transform.position.x <= -2120) // 1920 + 200(여유분)
        {
            
        }*/
        if(usedFrontBuffer.Count >= bufferLimit)
        {
            ClearFrontBuffer();
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
    // 다음 위치 잡기
    private void SetBlockPosition(GameObject now, GameObject next)
    {
        // 바닥 1개 x축 722픽셀
        // 한 블록당 바닥 3개 -> 2166
        Vector3 pos = now.transform.position;
        pos += new Vector3(2166f, 0, 0);
        next.transform.Translate(pos);
    }

} 


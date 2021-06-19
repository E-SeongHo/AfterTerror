using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // �÷��̾ ��ġ�� ���� ��� x�� 900���� �� ���� ��� �������� �ɵ�
    

    // �÷��̾ ��ġ�� ���
    private GameObject nowFront;
    private GameObject nowMiddle;
    // ���� ���
    private GameObject nextFront;
    private GameObject nextMiddle;
    // ���� ���
    private GameObject newFront;
    private GameObject newMiddle;

    // Used Map Blocks Buffer
    private Queue<GameObject> usedFrontBuffer = new Queue<GameObject>();
    private Queue<GameObject> usedMiddleBuffer = new Queue<GameObject>();

    private int bufferLimit = 3;
    private bool instantiate = false;
    private bool swapped = false;

    private void Start() // MapBlockSpawner�� pool�� ��ü�� �� �� ȣ��Ǿ�� �Ѵ�.
    {
        nowFront = MapBlockSpawner.Instance.AllocateFrontBlock();
        nextFront = MapBlockSpawner.Instance.AllocateFrontBlock();
        SetBlockPosition(nowFront, nextFront);
        nowFront.SetActive(true);
        nextFront.SetActive(true);
    }
    private void Update()
    {
        if(nextFront.transform.position.x <= 1160 && !instantiate) // 960 + 200(������)
        {
            instantiate = true;
            newFront = MapBlockSpawner.Instance.AllocateFrontBlock();
            SetBlockPosition(nextFront, newFront);
            newFront.SetActive(true);
            usedFrontBuffer.Enqueue(nowFront);
            // ���� Swap
            nowFront = nextFront;
            nextFront = newFront;

            instantiate = false;
        }
        /*if(nowFront.transform.position.x <= -2120) // 1920 + 200(������)
        {
            
        }*/
        if(usedFrontBuffer.Count >= bufferLimit)
        {
            ClearFrontBuffer();
        }
    }
    private void ClearFrontBuffer()
    {
        for(int i = 0; i < usedFrontBuffer.Count-1; i++) // now�� �����ϰ� ����
        {
            Destroy(usedFrontBuffer.Peek());
            usedFrontBuffer.Dequeue();
        }
    }
    // ���� ��ġ ���
    private void SetBlockPosition(GameObject now, GameObject next)
    {
        // �ٴ� 1�� x�� 722�ȼ�
        // �� ��ϴ� �ٴ� 3�� -> 2166
        Vector3 pos = now.transform.position;
        pos += new Vector3(2166f, 0, 0);
        next.transform.Translate(pos);
    }

} 


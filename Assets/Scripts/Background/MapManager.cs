using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // �÷��̾ ��ġ�� ���� ��� x�� 900���� �� ���� ��� �������� �ɵ�

    [SerializeField] private GameObject front_noEnemy;
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

    private void Start() // MapBlockSpawner�� pool�� ��ü�� �� �� ȣ��Ǿ�� �Ѵ�.
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
        if(nextFront.transform.position.x <= 1160) // 960 + 200(������)
        {
            newFront = MapBlockSpawner.Instance.AllocateFrontBlock();
            SetBlockPosition(nextFront, newFront);
            newFront.SetActive(true);
            usedFrontBuffer.Enqueue(nowFront);
            // ���� Swap
            nowFront = nextFront;
            nextFront = newFront;
        }
        /*if(nowFront.transform.position.x <= -2120) // 1920 + 200(������)
        {
            
        }*/
        if(usedFrontBuffer.Count >= bufferLimit)
        {
            ClearFrontBuffer();
        }
        if (nextMiddle.transform.position.x <= 1160) // 960 + 200(������)
        {
            newMiddle = MapBlockSpawner.Instance.AllocateMiddleBlock();
            SetBlockPosition(nextMiddle, newMiddle);
            newMiddle.SetActive(true);
            usedMiddleBuffer.Enqueue(nowMiddle);
            // ���� Swap
            nowMiddle = nextMiddle;
            nextMiddle = newMiddle;
        }
        /*if(nowFront.transform.position.x <= -2120) // 1920 + 200(������)
        {
            
        }*/
        if (usedMiddleBuffer.Count >= bufferLimit)
        {
            ClearMiddleBuffer();
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
    private void ClearMiddleBuffer()
    {
        for (int i = 0; i < usedMiddleBuffer.Count - 1; i++) // now�� �����ϰ� ����
        {
            Destroy(usedMiddleBuffer.Peek());
            usedMiddleBuffer.Dequeue();
        }
    }
    // ���� ��ġ ���
    private void SetBlockPosition(GameObject now, GameObject next)
    {
        // �ٴ� 1�� x�� 720�ȼ�
        // �� ��ϴ� �ٴ� 3�� -> 2160
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


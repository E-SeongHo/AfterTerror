using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantViewMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    private float startXpos;
    private float endXpos;

    private void Start()
    {
        startXpos = transform.position.x;
        endXpos = startXpos - 100f; // �̹��� ũ�� ??
    }
    private void Update()
    {
        transform.Translate(speed * -1 * Time.deltaTime, 0, 0);
        if(transform.position.x <= endXpos)
        {
            transform.Translate(-1 * (endXpos - startXpos), 0, 0); 
        }
    }
}

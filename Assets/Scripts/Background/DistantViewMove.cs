using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantViewMove : MonoBehaviour
{
    private float speed = 50f;
    private float startXpos;
    private float endXpos;

    private void Start()
    {
        startXpos = transform.position.x;
        endXpos = startXpos - 3200f; // 이미지 크기 ??
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

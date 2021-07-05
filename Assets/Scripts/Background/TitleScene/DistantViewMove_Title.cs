using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantViewMove_Title : MonoBehaviour
{
    // car speed
    private float speed = 100f;
    private float startXpos;
    private float endXpos;

    private void Start()
    {
        startXpos = transform.position.x;
        endXpos = startXpos + 3200f;
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position.x >= endXpos)
        {
            transform.Translate(-1 * (endXpos - startXpos), 0, 0);
        }
    }
}

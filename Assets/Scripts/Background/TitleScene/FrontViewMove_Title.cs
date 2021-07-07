using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontViewMove_Title : MonoBehaviour
{
    
    private float speed = 600f;
    private float startXpos;
    private float endXpos;

    private void Start()
    {
        startXpos = transform.position.x;
        endXpos = startXpos + 2160f;
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

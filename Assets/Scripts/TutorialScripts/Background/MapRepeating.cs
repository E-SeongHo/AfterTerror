using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRepeating : MonoBehaviour
{
    private float speed = 300f;
    private float startXpos;
    private float endXpos;

    private void Start()
    {
        startXpos = transform.position.x;
        endXpos = startXpos + 1440f;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        
        if(transform.position.x >= endXpos) 
        {
            transform.Translate(-1 * (endXpos - startXpos), 0, 0);
        }
    }

}

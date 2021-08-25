using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRepeating : MonoBehaviour
{
    private float speed = 200f;
    private float startXpos; // -189
    private float endXpos; // -1629

    private void Start()
    {
        startXpos = transform.position.x;
        endXpos = startXpos - 1440f;
        Debug.Log(startXpos + "과" + endXpos);
    }

    private void Update()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        // Debug.Log(transform.position.x);
        // Debug.Log("map repeating is working");
        // Debug.Log(startXpos);

        if (transform.position.x <= endXpos)
        {
            transform.Translate(-1 * (endXpos - startXpos), 0, 0);
            Debug.Log("map repeated");
        }
    }

}

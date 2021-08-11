using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleViewMove : MonoBehaviour
{
    private float speed = 200f;

    private void Update()
    {
        transform.Translate(speed * -1 * Time.deltaTime, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontViewMove : MonoBehaviour
{
    private float speed = 300f;

    public float GetFrontViewSpeed() { return speed; }
    private void Update()
    {
        transform.Translate(speed * -1 * Time.deltaTime, 0, 0);
    }
}

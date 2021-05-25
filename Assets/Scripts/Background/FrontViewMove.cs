using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontViewMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;

    private void Update()
    {
        transform.Translate(speed * -1 * Time.deltaTime, 0, 0);
    }
}

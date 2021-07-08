using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public Transform trans;

    public void Awake()
    {
        Instance = this;
        trans = gameObject.transform;
    }

    public void MovePosition(float speed)
    {
        trans.Translate(speed * Time.deltaTime, 0, 0);
    }
}

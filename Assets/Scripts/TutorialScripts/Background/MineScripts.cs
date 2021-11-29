using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScripts : MonoBehaviour
{
    public Vector3 startPos;

    private float moveX;

    MapRepeating map;



    private void Awake()
    {
        map = FindObjectOfType<MapRepeating>();
    }
    private void Start()
    {
        transform.localPosition = startPos;
    }

    private void Update()
    {
        moveX = -1 * map.speed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_projectileManager : MonoBehaviour
{
    public Vector3 startPos;

    private float bulletSpeed = 600f;
    private float moveX;



	private void Start()
	{
        transform.localPosition = startPos;
    }

	private void Update()
    {
        moveX = -1 * bulletSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);
    }
}

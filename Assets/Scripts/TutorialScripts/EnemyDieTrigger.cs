using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieTrigger : MonoBehaviour
{
	public bool isDie;

	MapRepeating map;



	private void Awake()
	{
		map = FindObjectOfType<MapRepeating>();
	}
	private void Update()
    {
		if (isDie == true)
		{
			transform.Translate(-1 * map.speed * Time.deltaTime, 0, 0);
		}
    }
}
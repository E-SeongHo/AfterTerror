using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image image;
    private EnemyControl enemyControl;
    [SerializeField] GameObject Enemy;
    private void Start()
    {
        image = GetComponent<Image>();
        enemyControl = Enemy.gameObject.GetComponent<EnemyControl>();
        // Debug.Log("FadeScript's isPause is: " + enemyControl.isPause);
    }

    private void Update()
    {
        // Debug.Log("FadeScript's isPause is " + enemyControl.isPause);
        if (enemyControl.isPause)
        {
            Color color = image.color;
            color.a = 0.355f;
            // if (color.a < 1 && color.a <= 0.355f)
            // {
            //     color.a += Time.deltaTime;
            // }

            image.color = color;
        }
    }
}


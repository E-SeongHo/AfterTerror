using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] alpha_buttons = new GameObject[3]; // 0: A, 1: S, 2: D
    private GameObject target;
    List<GameObject> existEnemy = new List<GameObject>();
    EnemyController enemyController;
    void Update()
    {

    }

    bool findEnemy()
    {
        bool isOn = false;
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("soldier");
        if (enemies == null) { return false; }
        else
        {
            foreach (GameObject Enemy in enemies)
            {
                if (!Enemy.GetComponent<EnemyController>().buttonActive)
                {
                    existEnemy.Add(Enemy);
                    isOn = true;
                }
            }
            return isOn;
        }
    }

    private void GiveButtonToNewEnemy()
    {
        int random;
        foreach (GameObject Enemy in existEnemy)
        {
            enemyController = Enemy.GetComponent<EnemyController>();
            random = Random.Range(0, 3);
            if (!enemyController.buttonActive)
            {
                // enemyController.GenerateEnemy();
                // alpha_buttons[random], random
            }
        }
    }
}

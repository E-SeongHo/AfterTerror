using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyAnimation : MonoBehaviour
{
    EnemyButtonCreator enemyButtonCreator;
    Animator anim;
    void Start()
    {
        enemyButtonCreator = GetComponent<EnemyButtonCreator>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (enemyButtonCreator.buttonSpawner.activeSelf == false)
        // {
        //     anim.SetBool("isDie", true);
        // }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isDie", true);
        }

    }
}

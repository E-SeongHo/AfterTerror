using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimScript : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            anim.SetBool("isDie", true);
        }

        if (Input.GetKey(KeyCode.O))
        {
            anim.SetBool("runAway", true);
        }

        if (Input.GetKey(KeyCode.M))
        {
            anim.SetTrigger("on");

        }
    }
}

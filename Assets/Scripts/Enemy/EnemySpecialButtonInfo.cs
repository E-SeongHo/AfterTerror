using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialButtonInfo : MonoBehaviour
{
    // 0 : A, 1 : S, 2 : D
    [SerializeField] private GameObject[] button_prefabs = new GameObject[3];

    public EnemyController core;

    private Queue<SpecialButton>

    private Rigidbody2D rb;

    private GameObject xSheet;
    private Animator xSheet_anim;

    private void Start()
    {
        core = gameObject.GetComponent<EnemyController>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        xSheet = gameObject.transform.GetChild(0).gameObject;
        xSheet_anim = xSheet.GetComponent<Animator>();
    }

}

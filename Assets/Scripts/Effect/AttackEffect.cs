using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    private Animator animator;
    private Transform originParent;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        originParent = gameObject.transform.parent;
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (animator != null)
            animator.SetTrigger("generate");
    }
    private void ActiveOff()
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(originParent, false);
    }
}

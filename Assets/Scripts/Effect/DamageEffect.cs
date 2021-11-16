using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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
    }
}

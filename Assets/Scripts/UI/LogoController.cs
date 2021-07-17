using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        FallFromSky();
    }
    private void OnEnable()
    {
        
    }
    private void FallFromSky()
    {
        animator.SetBool("on", true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        animator.SetBool("mouseON", true);
    }
    private void OnMouseExit()
    {
        animator.SetBool("mouseON", false);
    }
    private void OnMouseDown()
    {
        // game start !!!
         SceneManager.LoadScene("Stage1");
    }
}

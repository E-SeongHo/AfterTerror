using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EffectManage.cs -> SuccessShield()
/// 1. 이펙트가 보여져야 할 때 해당 위치로 옮긴 후 SetActive(true) 시킨다.
/// 2. OnEnable()에 의해 애니메이션 재생
/// 3. 이후 caller가 ActiveOff() 호출하여 다시 SetActive(false) 
///  3 -> Animation event에 의해 처리하도록 수정하였음.
/// </summary>
public class ShieldEffect : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(animator != null)
            animator.SetTrigger("generate");
    }
    private void ActiveOff()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManage : MonoBehaviour
{
    private GameObject shieldEffect;
    private GameObject attackEffect;
    private GameObject damageEffect;

    private void Start()
    {
        shieldEffect = gameObject.transform.GetChild(0).gameObject;
        attackEffect = gameObject.transform.GetChild(1).gameObject;
        damageEffect = gameObject.transform.GetChild(2).gameObject;
    }

    public void SuccessShield()
    {
        Vector3 genPos = new Vector3(47, -58);
        shieldEffect.transform.position = ShieldmanController.Instance.gameObject.transform.position + genPos;
        shieldEffect.SetActive(true);
        // effect object(shieldEffect) will be SetActive(false) by animation event
    }
    public void SuccessAttack(GameObject target)
    {
        if(target!=null)
        {
            attackEffect.transform.SetParent(target.transform, false);
            attackEffect.transform.localPosition = new Vector3(-80, -60);
            attackEffect.SetActive(true);
            // effect object will be SetActive(false) & SetParent to origin parent by animation event
        }
    }
    public void PlayerHit()
    {
        damageEffect.transform.position = ShieldmanController.Instance.gameObject.transform.position + new Vector3(0, -50, 0);
        damageEffect.SetActive(true);
        // effect object will be SetActive(false) by animation event
    }
}

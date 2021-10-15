using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManage : MonoBehaviour
{
    private GameObject shield_effect;

    private void Start()
    {
        shield_effect = gameObject.transform.GetChild(0).gameObject;
    }

    public void SuccessShield()
    {
        Vector3 gen_pos = new Vector3(47, -58);
        shield_effect.transform.position = ShieldmanController.Instance.gameObject.transform.position + gen_pos;
        shield_effect.SetActive(true);
        // effect object(shield_effect) will be SetActive(false) by animation event
    }
}

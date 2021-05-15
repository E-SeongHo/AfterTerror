using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInfo : MonoBehaviour
{
    private bool addedToManageList; // 투사체를 관리하는 
    public bool isAdded
    {
        get{return addedToManageList;}
    }
}

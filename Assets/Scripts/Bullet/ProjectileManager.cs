using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임에 존재하는 투사체 관리 : 메인 케릭터와 가장 가까운 투사체는 메인케릭터와 상호작용
public class ProjectileManager : MonoBehaviour
{
    private List<GameObject> existProjectiles; // 현재 존재하는 투사체 리스트

    private bool FindProjectiles() // 현재 리스트의 내용이 추가되면 true
    {
        // 이것도 리스트로 현재 존재하는 투사체 관리 현재 있는거면 추가 안하고 없는거면 추가하는 식
        bool addNew = false;
        GameObject[] Projectiles;
        Projectiles = GameObject.FindGameObjectsWithTag("Projectile"); //현재 모든 투사체
        if (Projectiles == null) return false;
        else 
        {
            foreach(GameObject Projectile in Projectiles)
            {
                //투사체의 종류는 여러가지 있을 수 있음.. GetComponent어떻게?
                //이미 리스트에 존재하는 투사체임을 판별할수 있는 방법??
                //근데 이걸 Update문에서 계속 돌게하면 비용이 너무 클거같다.
                //오브젝트 풀링 이용 나중에 바꿔
                if(!Projectile.GetComponent<ProjectileInfo>().isAdded)
                {
                    existProjectiles.Add(Projectile);
                    addNew = true;    
                }   
            }
        }
        return addNew; 
    }

}

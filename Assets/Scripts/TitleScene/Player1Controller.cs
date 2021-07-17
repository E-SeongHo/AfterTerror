using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    [SerializeField] private GameObject banner_prefab;
    [SerializeField] private GameObject choose_prefab;
    [SerializeField] private GameObject start_prefab;

    private GameObject banner = null;
    private GameObject choose = null;
    private GameObject start = null;

    private Animator my_animator;
    private Animator banner_animator = null;
    
    private Vector2 banner_pos = new Vector2(700f, 145f); // 0, 145 / camera pos : (700,0)
    private Vector2 start_pos = new Vector2(1050f, -50f); // 350, -50

    private void Start()
    {
        my_animator = gameObject.GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if(banner == null 
            && my_animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle"))
        {
            StartCoroutine("BannerInit");
        }    
    }

    IEnumerator BannerInit()
    {
        Vector2 choose_pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 100f);
        choose = Instantiate(choose_prefab, choose_pos, Quaternion.identity);
        banner = Instantiate(banner_prefab, banner_pos, Quaternion.identity);
        banner_animator = banner.GetComponent<Animator>();
        yield return new WaitForSeconds(1.0f);
        start = Instantiate(start_prefab, start_pos, Quaternion.identity);
    }
}

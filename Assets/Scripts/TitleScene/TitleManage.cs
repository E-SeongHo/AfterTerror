using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManage : MonoBehaviour
{
    private GameObject distant_view;
    private GameObject front_view;

    private GameObject car;
    private GameObject logo;

    private Animator logo_animator;
    private Animator car_animator;
    private Animator player1_animator;

    public Text press_any_key;
    public GameObject player1;

    // player 생성 위치 (Car 로부터 거리)
    private float player_xdiff = 1202.6f;
    private float player_ydiff = -163f;
    

    private void Start()
    {
        // find objects
        distant_view = GameObject.Find("Title_Distant");
        front_view = GameObject.Find("Title_Front");
        car = GameObject.Find("Title_Car");
        logo = GameObject.Find("Title_Logo");
        
        // set active false
        press_any_key.gameObject.SetActive(false);
        player1.SetActive(false);

        // get animator component
        logo_animator = logo.GetComponent<Animator>();
        car_animator = car.GetComponent<Animator>();
        player1_animator = player1.GetComponent<Animator>();

        // play logo
        logo_animator.SetBool("play", true);
    }

    private void Update()
    {
        if (logo_animator.GetCurrentAnimatorStateInfo(0).IsName("Logo_Shining"))
        {
            // press_any_key 생성
            // + Coroutine으로 깜빡거리게 구현
            press_any_key.gameObject.SetActive(true);

            // if any key pressed
            if (Input.anyKeyDown)
            {
                // erase logo
                logo.SetActive(false);
                press_any_key.gameObject.SetActive(false);

                StartCoroutine("StopCar"); // when coroutine end, start camera move in LateUpdate 

                // stop background moving
                distant_view.GetComponent<DistantViewMove_Title>().enabled = false;
                front_view.GetComponent<FrontViewMove_Title>().enabled = false;   
            }
        }

        if (car_animator.GetCurrentAnimatorStateInfo(0).IsName("Car_Off")
            && car_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f
            && car_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f) 
        {
            Vector3 pos = new Vector3(car.transform.position.x + player_xdiff, car.transform.position.y + player_ydiff, 0);
            player1.transform.position = pos;
            player1.SetActive(true);

            StartCoroutine("MovePlayer1");
        }
        
    }
    // Camera Move
    private void LateUpdate()
    {
        if (car_animator.GetCurrentAnimatorStateInfo(0).IsName("Car_Stop")
            && car_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            StartCoroutine("MoveCamera");
        }
    }

    IEnumerator StopCar()
    {
        float speed = 1200f;
        while(car.transform.position.x > -950)
        {
            car.transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
            yield return new WaitForFixedUpdate();
        }

        // stop animation
        car_animator.SetBool("stop", true);

        yield return null; // end of coroutine
    }

    IEnumerator MoveCamera()
    {
        float speed = 50f;
        while(CameraController.Instance.trans.position.x < 700)
        {
            CameraController.Instance.MovePosition(speed);
            yield return new WaitForSeconds(0.2f);
        }

        // open animation
        car_animator.SetBool("open", true);
       
        yield return null;
    }

    IEnumerator MovePlayer1()
    {
        float speed = 10f;
        while(player1.transform.position.x < 1050) // 700(camera) + 350(적정위치)
        {
            player1.transform.Translate(speed * Time.deltaTime, 0, 0);
            yield return new WaitForFixedUpdate();
        }

        // idle animation
        player1_animator.SetBool("stop", true);

        yield return null;
    }
}

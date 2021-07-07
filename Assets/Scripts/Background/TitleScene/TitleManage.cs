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

    //public Image logo;
    public Text press_any_key;
    //private bool show_logo;

    private void Start()
    {
        distant_view = GameObject.Find("Title_Distant");
        front_view = GameObject.Find("Title_Front");
        car = GameObject.Find("Title_Car");
        logo = GameObject.Find("Title_Logo");
        
        press_any_key.gameObject.SetActive(false);
        // get animator component
        logo_animator = logo.GetComponent<Animator>();
        car_animator = car.GetComponent<Animator>();
        // logo play
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // erase logo
                logo.SetActive(false);
                press_any_key.gameObject.SetActive(false);

                StartCoroutine("StopCar");

                // stop background moving
                distant_view.GetComponent<DistantViewMove_Title>().enabled = false;
                front_view.GetComponent<FrontViewMove_Title>().enabled = false;   
            }
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

        // create player
        
        yield return null;
    }

}

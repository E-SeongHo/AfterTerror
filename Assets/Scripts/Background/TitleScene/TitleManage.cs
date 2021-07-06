using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManage : MonoBehaviour
{
    private GameObject car;
    private GameObject logo;
    private Animator logo_animator;
    //public Image logo;
    public Text press_any_key;
    //private bool show_logo;

    private void Start()
    {
        car = GameObject.Find("Title_Car");
        logo = GameObject.Find("Title_Logo");
        logo_animator = logo.GetComponent<Animator>();
        logo_animator.SetBool("play", true);
    }

    private void FixedUpdate()
    {
        if (logo_animator.GetCurrentAnimatorStateInfo(0).IsName("Logo_Shining"))
        {
            // press_any_key »ý¼º
            Debug.Log("time");
        }
    }
    private void Update()
    {

        // if any key pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("StopCar");

            // background move stop
        }

    }

    IEnumerator StopCar()
    {
        float speed = 600f;
        while(car.transform.position.x > -1230)
        {
            car.transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
            yield return new WaitForFixedUpdate();
        }

        // stop animation

        yield return null; // end of coroutine
    }
    /*IEnumerator PlayLogoAnimation()
    {

    }*/

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManage : MonoBehaviour
{
    private GameObject car;
    public Image logo;
    public Text press_any_key;
    private bool show_logo = true;

    private void Start()
    {
        car = GameObject.Find("Title_Car");    
    }

    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        // if any key pressed
        if (Input.GetKeyDown(KeyCode.Space) && show_logo)
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

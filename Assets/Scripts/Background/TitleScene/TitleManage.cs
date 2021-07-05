using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManage : MonoBehaviour
{
    private GameObject car;

    private void Start()
    {
        car = GameObject.Find("Title_Car");
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

        yield return null;

    }

}

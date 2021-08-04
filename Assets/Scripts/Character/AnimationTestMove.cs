using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(gameObject.transform.position + new Vector3(2f, 0, 0));
        }
    }
}

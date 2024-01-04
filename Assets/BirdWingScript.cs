using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdWingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // transform.Rotate(0.0f, 0.0f, 45.0f);
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 10);
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            transform.rotation = Quaternion.identity;

        }


    }
}

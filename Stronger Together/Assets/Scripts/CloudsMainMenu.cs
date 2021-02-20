using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsMainMenu : MonoBehaviour
{
    public float minXPosition;
    public float maxXPosition;
    public float cloudSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * cloudSpeed);

        if (transform.position.x <= minXPosition)
        {
            transform.position = new Vector3(maxXPosition, 0, 0);
        }
    }
}

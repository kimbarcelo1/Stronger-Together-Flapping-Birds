using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float minXPosition;
    public float maxXPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameControl.instance.gameOver)
        {
            transform.Translate(Vector3.right * Time.deltaTime * GameControl.instance.scrollSpeed);
        }

        if (transform.position.x <= minXPosition)
        {
            transform.position = new Vector3(maxXPosition, 0, 0);
        }
    }
}

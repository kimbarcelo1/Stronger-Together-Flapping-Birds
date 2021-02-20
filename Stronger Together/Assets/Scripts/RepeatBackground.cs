using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 originalPosition;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameControl.instance.gameOver)
        {
            transform.Translate(Vector3.right * Time.deltaTime * GameControl.instance.scrollSpeed);
        }

        // ang original position is 45 - (112.8 / 2) = 
        if (transform.position.x < originalPosition.x - repeatWidth)
        {
            transform.position = originalPosition;
        }
    }
}

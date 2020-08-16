using UnityEngine;
using System.Collections;

public class ScrollControl : MonoBehaviour {

    public float speed = -1f;
    float width;
    public Transform left, right;

    // Use this for initialization
    void Start () {
        // set the vertical speed of the background
        GetComponent<Rigidbody2D>().velocity = new Vector3 (speed, 0f, 0f);
        // find out how big our image is
        width = (right.position.x - left.position.x)/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            // moving up
            // if we get past the height of one image, move both of them down to restart the cycle.
            if (transform.position.x >= width)
                transform.position = new Vector3(-width, transform.position.y, transform.position.z);

        }
        else 
        {
            // moving down
            // if we get past minus the height of one image, move both of them up to restart the cycle.
            if (transform.position.x <= -width)
                transform.position = new Vector3(width, transform.position.y, transform.position.z);

        }
    }
}
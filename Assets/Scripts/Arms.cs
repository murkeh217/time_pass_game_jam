using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    private int speed = 300;

    public Rigidbody2D rb;

    public Camera cam;

    //public KeyCode button;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,
            cam.ScreenToWorldPoint(Input.mousePosition).y,0f);
        Vector3 difference = playerPos - transform.position;
        float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;

        if (Input.GetMouseButton(0))
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.fixedDeltaTime));
        }
    }
}

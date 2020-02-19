using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_1 : MonoBehaviour
{
    public float r_wheel_sp = 0.1f, l_wheel_sp = 0.1f;
    private float speed = 0.1f;
 
    //private float rotation_speed = 0.03f;
    //private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mean = (r_wheel_sp + l_wheel_sp) / 2.0f;
        float diff = - r_wheel_sp + l_wheel_sp;

        //l_wheel_sp += Input.GetAxis("Vertical") * 0.01f;
        //r_wheel_sp += Input.GetAxis("Horizontal") * 0.01f;
        //l_wheel_sp = Mathf.Clamp(l_wheel_sp, 0, 1);
        //r_wheel_sp = Mathf.Clamp(r_wheel_sp, 0, 1);

        transform.position += speed * (mean * ( - transform.right ) + diff * ( - transform.forward) ) ;

        //rotation.SetFromToRotation(transform.forward, (mean * transform.forward + diff * transform.right));
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.time * rotation_speed);
    }
}

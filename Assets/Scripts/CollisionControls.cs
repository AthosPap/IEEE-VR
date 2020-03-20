using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControls : MonoBehaviour
{
    public Camera myCam;
    public bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "moveable")
        {
            isColliding = true;
            other.GetComponent<MeshRenderer>().material = myCam.GetComponent<GUIControl>().ghostMaterial2;
        }
        else if (other.tag == "checkpoint")
        {
            
            GetComponent<RespawnControl>().dir = other.GetComponent<CheckPointBehaviour>().direction;
            GetComponent<RespawnControl>().pos = other.GetComponent<CheckPointBehaviour>().position;
            other.GetComponent<CheckPointBehaviour>().activated = true;
   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "moveable")
        {
            isColliding = false;
            other.GetComponent<MeshRenderer>().material = myCam.GetComponent<GUIControl>().ghostMaterial;
        }
    }

}

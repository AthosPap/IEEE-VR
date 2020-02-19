using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBehaviour : MonoBehaviour
{
    public Quaternion direction;
    public Vector3 position;
    public int credits;
    public bool activated;
    private GameObject player;
    public Camera cam;
    private GUIControl gui;
    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>().position;
        position = new Vector3(position.x, 1.2f, position.z);
        player = GameObject.Find("Wheelchair");
        gui = cam.GetComponent<GUIControl>();
        credits = 400;
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (activated)
        {
            activated = false;
            TriggerCheckpoint();
        }
        
    }
    
    void TriggerCheckpoint()
    {
        gui.moni += credits;
        Destroy(this.gameObject);
    }

}

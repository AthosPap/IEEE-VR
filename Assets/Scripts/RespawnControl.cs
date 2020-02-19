using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControl : MonoBehaviour
{
    public DeathMenu dm;

    private Transform tr;
    private float transition = 0.0f;
    public bool dead = false;
    public Vector3 pos;
    public Quaternion dir;
    private bool isRespawning = false;
    public GameObject LastCheckPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        pos = tr.position;
        dir = tr.rotation;
    }

    // Update is called once per frame  
    private void Update()
    {

        if (((tr.rotation.eulerAngles.x >= 45.0f) && (tr.rotation.eulerAngles.x <= 315.0f)) ||
           ((tr.rotation.eulerAngles.z >= 45.0f) && (tr.rotation.eulerAngles.z <= 315.0f))) dead = true;
        
        if (dead)
        {
            if (!isRespawning) OnDeath();
            if (dm.transitionDone)
            {
                Respawn();
                dm.UntoggleEndMenu();
                isRespawning = false;
                dead = false;
                dm.transitionDone = false;
            }
        }
    }

    void OnDeath()
    {
        isRespawning = true;
        dm.ToggleEndMenu();
    }
    void Respawn()
    { 
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        tr.SetPositionAndRotation(pos, dir);
    }
}

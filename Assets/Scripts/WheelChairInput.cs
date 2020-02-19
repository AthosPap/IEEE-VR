using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WheelChairInput : MonoBehaviour
{

    public bool leftGrabbing = false, rightGrabbing = false;
    private Vector3 rightThrustMean = Vector3.zero, leftThrustMean = Vector3.zero;
    private int counterR = 0, counterL = 0;
    public int counterInterval = 10;

    public float thrust = 1.0f;
    public Rigidbody rightWheelRB, leftWheelRB, chairRB;
    public Transform leftWheel, rightWheel, leftController, rightController;
    private Vector3 lastPosLeft, lastPosRight;
    private Vector3 leftWForce = Vector3.zero, rightWForce = Vector3.zero;
    private float wheelRadius = 0.65f;

    public SteamVR_Action_Boolean leftGrab, rightGrab;

    // Start is called before the first frame update
    void Start()
    {
        chairRB = GetComponent<Rigidbody>();
        leftGrab.AddOnChangeListener(OnLeftGrab, SteamVR_Input_Sources.LeftHand);
        rightGrab.AddOnChangeListener(OnRightGrab, SteamVR_Input_Sources.RightHand);
    }

    private void OnLeftGrab(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        leftGrabbing = newState;
        lastPosLeft = leftController.localPosition;
    }

    private void OnRightGrab(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        rightGrabbing = newState;
        lastPosRight = rightController.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 leftMovement = Vector3.zero;
        Vector3 rightMovement = Vector3.zero;
        if (leftGrabbing)
        {
            var leftDiff = lastPosLeft - leftController.localPosition;
            leftMovement = Mathf.Sign(leftDiff.z) * leftDiff.magnitude * transform.right;
            leftWheelRB.velocity = 3.0f * leftMovement / Time.deltaTime;
            lastPosLeft = leftController.localPosition;
        }

        if (rightGrabbing)
        {
            var rightDiff = lastPosRight - rightController.localPosition;
            rightMovement = Mathf.Sign(rightDiff.z) * rightDiff.magnitude * transform.right;
            rightWheelRB.velocity = 3.0f * rightMovement / Time.deltaTime;
            lastPosRight = rightController.localPosition;
        }
    }

    //public void GetRightControllerPose(SteamVR_Behaviour_Pose pose, SteamVR_Input_Sources source)
    //{
        
    //    if (rightGrabbing)
    //    {
    //        //Debug.Log(pose.GetVelocity());
    //        pose
    //        rightThrustMean += pose.GetVelocity();
    //        counterR++;
    //        if (counterR >= counterInterval)
    //        {
    //            rightThrustMean = new Vector3(rightThrustMean.x / counterR, rightThrustMean.y / counterR, rightThrustMean.z / counterR);
    //            //Debug.Log(rightThrustMean);
    //            ApplyMovement(rightThrustMean, 1);
    //            counterR = 0;
    //            rightThrustMean = Vector3.zero;
    //        }

    //    }
        
    //}

    //public void GetLeftControllerPose(SteamVR_Behaviour_Pose pose, SteamVR_Input_Sources source)
    //{
    //    if (leftGrabbing)
    //    {
    //        leftThrustMean += pose.GetVelocity();
    //        counterL++;
    //        if (counterL >= counterInterval)
    //        {
    //            leftThrustMean = new Vector3(leftThrustMean.x / counterL, leftThrustMean.y / counterL, leftThrustMean.z / counterL);
    //            //Debug.Log(leftThrustMean);
    //            //Debug.Log(counterL);
    //            ApplyMovement(leftThrustMean, 0);
    //            counterL = 0;
    //            leftThrustMean = Vector3.zero;
    //        }
    //    }
    //}


    //private void ApplyMovement(Vector3 input, int flag)
    //{
    //    var inputN = input.normalized;
    //    if(Mathf.Abs(inputN.x) <= 0.5f &&  Mathf.Abs(inputN.y) <= 0.5f && Mathf.Abs(inputN.z) > 0.6f && flag==0)
    //    {
    //        leftWForce = -transform.right * Mathf.Sign(input.z) * input.magnitude * thrust;
    //        Debug.Log($"Left: {leftWForce}");
    //        rb.AddForceAtPosition(leftWForce, leftWheel.position, ForceMode.Impulse);

    //    }
    //    if (Mathf.Abs(inputN.x) <= 0.5f && Mathf.Abs(inputN.y) <= 0.5f && Mathf.Abs(inputN.z) > 0.6f && flag == 1)
    //    {
    //        rightWForce = -transform.right * Mathf.Sign(input.z) * input.magnitude * thrust;
    //        Debug.Log($"Right: {rightWForce}");
    //        rb.AddForceAtPosition(rightWForce, rightWheel.position, ForceMode.Impulse);
    //    }
    //}
}

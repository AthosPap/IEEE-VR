using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;


public class DeathMenu : MonoBehaviour
{
    private float transition = 0.0f;
    public Image bgImg;
    public bool transitionDone = false;
    public int phase = -1;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (phase == 0)
        {

            transition += Time.deltaTime;
            bgImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
            if (bgImg.color == Color.black)
            {
                transitionDone = true;
                phase = 3;
            }
        }
        else if (phase == 1)
        {
            transition += Time.deltaTime;
            bgImg.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), transition);
            if (bgImg.color == new Color(0, 0, 0, 0) && transitionDone)
            {
                gameObject.SetActive(false);
                transitionDone = false;
            }
        }

    }

    public void UntoggleEndMenu()
    {
        transition = 0.0f;
        phase = 1;
        VRfromBlack();
        gameObject.SetActive(false);
    }


    public void ToggleEndMenu()
    {
        //for the vr
        VRtoBlack();
        //for the display
        transition = 0.0f;
        gameObject.SetActive(true);
        phase = 0;


    }
    void VRtoBlack()
    {
        SteamVR_Fade.Start(Color.clear, 0);
        SteamVR_Fade.Start(Color.black, 3);
    }

    void VRfromBlack()
    {
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, 4);
    }
}



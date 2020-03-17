using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BtnCtrl : MonoBehaviour
{
    public Camera myCamera;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite locked;
    public bool isUnlocked = false;
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        btn = GetComponent<Button>();
        btn.image.overrideSprite = locked;
    }

    // Update is called once per frame
    void Update()
    { 
        bool ismov = myCamera.GetComponent<GUIControl>().currentlyMoving;
            
        if (ismov && isUnlocked) { btn.image.overrideSprite = sprite2; }
        else if (isUnlocked) { btn.image.overrideSprite = sprite1; }   
    }


}

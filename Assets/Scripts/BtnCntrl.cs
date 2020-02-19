using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BtnCntrl : MonoBehaviour
{
    public Camera myCamera;
    public Sprite sprite1;
    public Sprite sprite2;
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        btn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ismov = myCamera.GetComponent<GUIControl>().currentlyMoving;
        
        if (ismov) { btn.image.overrideSprite = sprite2; }
        else { btn.image.overrideSprite = sprite1; }
    }
}

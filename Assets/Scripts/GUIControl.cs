using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                // Mmmpies added


public class GUIControl : MonoBehaviour
{
    public Text monitxt;
    public int moni = 5000;
    public bool nextCheckpoint = false;
    private int currentUnlock = 0;
    public GameObject myGUI;
    public List<Button> myBtn;
    private Button[] temp;
    public GameObject Wheelchair;


    public GameObject[] Buildings;
    public string[] BuildingTags;
    private GameObject[] CurrentGhosts = { };
    public bool isHitting;
    public Material ghostMaterial;
    public Material ghostMaterial2;
    public int[] BuildingCosts; //TO ADD: Cost for each function



    public LayerMask acceptableLayerBuild;//Where we can build
    public LayerMask acceptableLayerMove;//Where we can move
    public LayerMask defaultLayer;//Layers we want to move should be placed in the default layer tab
    public LayerMask GhostLayerMask;//Ghosts that the user can not collide with

    private bool currentlyDestroying = false; //test function to destroy generic obstacles

    private int OldLayer;
    private int GhostLayer = 11;

    public Camera myCamera;
    private int buildingIndex; //which building will be placed
    private bool isUnlocked;
    private Material mat;

    private Vector3 originalScale; // ghost original scale
    private string currentTag; //the tag for current building so that ghosts appear
    private bool currentlyBuilding = false; //declare if we are currently in build mode
    public bool currentlyMoving = false;
    private bool isBeingMoved = false;
    private Vector3 position; //Vector to store building position if we are to use hover mode     
    private Vector3 originalPos;
    private GameObject currentBuilding; //what is currently being built 
    private GameObject newGO;
    private GameObject currentMove;
    private bool currentMoveExists = false;


    void Start()
    {
        originalPos = new Vector3(-100, -100, -100);
        GUI_Init();
        LoadGUI();
    }




    void Update()
    {
        monitxt.text = "" + moni.ToString();
        if (nextCheckpoint) NextCP();
        //Move Function
        if (currentlyMoving)
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (!isBeingMoved)
            {   //Since the object changes Layers when it is moveable, we must check both layermasks
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, (defaultLayer | GhostLayerMask)) && hit.transform.gameObject.tag == "moveable" && !currentMoveExists)
                {
                    //Used to initialise currentMove the first time user hovers a moveable obj. If works only the first time.
                    currentMove = hit.transform.gameObject;
                    if (!isHitting) mat = currentMove.GetComponent<MeshRenderer>().material;
                    isHitting = true;
                    currentMove.GetComponent<MeshRenderer>().material = ghostMaterial;
                    currentMoveExists = true;

                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity, (defaultLayer | GhostLayerMask)) && hit.transform.gameObject.tag == "moveable" && currentMoveExists)
                {
                    //Used the rest of the time.
                    if (hit.transform.gameObject.name != currentMove.name)
                    {
                        currentMove.GetComponent<MeshRenderer>().material = mat;
                        mat = hit.transform.gameObject.GetComponent<MeshRenderer>().material;
                    }
                    currentMove = hit.transform.gameObject;
                    if (!isHitting) mat = currentMove.GetComponent<MeshRenderer>().material;
                    isHitting = true;
                    currentMove.GetComponent<MeshRenderer>().material = ghostMaterial;
                }
                else
                {   //Not hovering a moveable
                    if (currentMove != null) currentMove.GetComponent<MeshRenderer>().material = mat;
                    isHitting = false;
                }
            }
            else
            {//if we are moving

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, acceptableLayerMove))
                {
                    position = hit.point;
                    currentMove.GetComponent<Transform>().position = position;
                }

            }
            if (Input.GetMouseButtonDown(0)) OnMouseDown(); //On Click
            if (Input.GetMouseButtonDown(1) && isBeingMoved) CancelMove();//RightClick cancels move.
        }

        //Build Function
        if (currentlyBuilding)
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, acceptableLayerBuild) && hit.transform.gameObject.tag == currentTag)
            {
                isHitting = true;
                newGO = hit.transform.gameObject;
                newGO.GetComponent<MeshRenderer>().material = newGO.GetComponent<GhostBehaviour>().OnHoverMaterial;

            }
            else
            {
                isHitting = false;
                if (newGO != null) newGO.GetComponent<MeshRenderer>().material = ghostMaterial;
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown();
            }
        }
        //Destroy Function
        if (currentlyDestroying)
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, acceptableLayerBuild) && hit.transform.gameObject.tag == currentTag)
            {
                isHitting = true;
                newGO = hit.transform.gameObject;
                newGO.GetComponent<MeshRenderer>().material = newGO.GetComponent<GhostBehaviour>().OnHoverMaterial;

            }
            else
            {
                isHitting = false;
                if (newGO != null) newGO.GetComponent<MeshRenderer>().material = ghostMaterial;
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown();
            }
        }


    }
    void OnMouseDown()
    {
        if (isHitting) //if we are raycasting on an object of interest when we click
        {
            if (currentlyBuilding) BuildObj();

            if (isBeingMoved) PlaceObj();

            if (currentlyMoving && !isBeingMoved) MoveObj();

            if (currentlyDestroying) DestroyObj();
        }

    }

    void BuildObj()
    {
        Transform tr = newGO.GetComponent<Transform>();
        newGO.SetActive(false);
        GameObject Building = (GameObject)Instantiate(currentBuilding,
                                                   tr.position,
                                                   tr.rotation);
        Building.transform.localScale = tr.localScale;
        Building.name = "NewBuilding";
        Building.SetActive(true);
        moni -= 500;
    }

    void PlaceObj()
    {
        bool check = CheckCollision();
        if (!check)
        {
            moni -= 300;
            currentMove.GetComponent<MeshRenderer>().material = mat;
            currentMove.layer = OldLayer;
            isBeingMoved = false;
            currentlyMoving = false;
            originalPos = new Vector3(-100, -100, -100);
        }
        else
        {
            CancelMove();
        }

    }


    void MoveObj()
    {
        isBeingMoved = true;
        originalPos = currentMove.GetComponent<Transform>().position;
        OldLayer = currentMove.layer;
        currentMove.layer = GhostLayer;
    }


    void DestroyObj()
    {
        moni -= 600;
        Destroy(newGO);
    }


    bool CheckCollision()
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 12))) return true;
        else return false;
    }

    public void CancelMove()
    {
        if (currentMove != null && originalPos != new Vector3(-100, -100, -100))
        {
            currentMove.GetComponent<Transform>().position = originalPos;
            currentMove.GetComponent<MeshRenderer>().material = mat;
            currentMove.layer = OldLayer;
        }
        isBeingMoved = false;
        currentlyMoving = false;
        originalPos = new Vector3(-100, -100, -100);
    }

    public void SetMoveMode() //Start Move mode
    {
        if (!currentlyMoving)
        {
            currentlyMoving = true;
        }
        else CancelMove();
        if (currentlyBuilding)
        {
            DisableGhosts();
            currentlyBuilding = false;
        }

    }

    public void ButtonHandler(int Btn)
    {
        switch (Btn)
        {
            case 1:

                if (currentUnlock > 0) AssignBuilding(Btn);
                break;
            case 2:
                if (currentUnlock > 1) SetMoveMode();
                break;
            case 3:
                if (currentUnlock > 2) AssignBuilding(Btn);
                break;
        }
    }


    public void AssignBuilding(int BuildingIndex) //Enable Building mode for specific object.
    {
        if (currentlyMoving)
        {
            currentlyMoving = false;
            CancelMove();
        }
        if (!currentlyBuilding)
        {
            DisableGhosts();
            currentBuilding = Buildings[BuildingIndex - 1];
            currentTag = BuildingTags[BuildingIndex - 1];
            currentlyBuilding = true;
            EnableGhosts();

        }
        else
        {
            DisableGhosts();
            currentlyBuilding = false;
        }
    }
    public void EnableGhosts()
    {
        CurrentGhosts = GameObject.FindGameObjectsWithTag(currentTag);

        foreach (GameObject ghost in CurrentGhosts)
        {

            originalScale = ghost.GetComponent<GhostBehaviour>().originalScale;
            ghost.transform.localScale = originalScale;
        }
    }
    public void DisableGhosts()
    {
        if (CurrentGhosts.Length != 0)
        {
            foreach (GameObject ghost in CurrentGhosts)
            {
                ghost.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

    //CheckPoint Functions
    public void NextCP()
    {
        currentUnlock += 1;
        LoadGUI();
        nextCheckpoint = false;
    }

    private void LoadGUI()
    {
        for (int i = 0; i < currentUnlock; i++)
        {
            Button btn = myBtn[i].GetComponent<Button>();
            btn.GetComponent<BtnCtrl>().isUnlocked = true;
        }
    }

    private void GUI_Init()
    {
        foreach (Transform child in myGUI.transform) { 
            temp = child.gameObject.GetComponentsInChildren<Button>();
            if (temp[0]) myBtn.Add(temp[0]);
            
        }   
    }
}
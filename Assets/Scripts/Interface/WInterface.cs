using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WInterface : MonoBehaviour
{
    private GameObject _focusObj; // the object the mouse is controlling
    private Resource _objResourceData;
    private GameObject newResoucePrefab; // the type of Resource we are setting down
    public GameObject[] allResources;
    private Vector3 _goalPos = Vector3.zero; //position we want to put the gameObject
    [SerializeField] private Transform hospital;
    [SerializeField] private NavMeshSurface _surface;
    
    private Vector3 _clickOffset = Vector3.zero;
    private bool _offsetCalc = false;
    private bool _deleteResource = false; 

    private void Update()
    {
        
        //On first Clic
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            _offsetCalc = false;
            _clickOffset=Vector3.zero;
            
            //Shoot ray to mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray.origin, ray.direction, out hit))
                return;
            //If You hit something check if it's a resource
            if(hit.transform.GetComponent<Resource>()!=null)
            {
                //if it's a resource assign it to focus object and remove it from the world's object list
                _focusObj = hit.transform.gameObject;
                _objResourceData = _focusObj.GetComponent<Resource>();
                GWorld.Instance.GetResourceQueue(_objResourceData.resourceData.resourceQueue.ToString()).RemoveResourse(_focusObj);
                GWorld.Instance.GetWorld().ModifyState(_objResourceData.resourceData.resourceState.ToString(),-1);
            }
            else if(newResoucePrefab!=null)
            {
                //if you hit a non resource object (AKA ground, instantiate an object there)
                _goalPos = hit.point;
                _focusObj = Instantiate(newResoucePrefab, _goalPos, newResoucePrefab.transform.rotation);
                _objResourceData = _focusObj.GetComponent<Resource>();
            }
            if(_focusObj)
                _focusObj.GetComponent<Collider>().enabled = false;
            // Debug.Log("Collider Disabled");
        }
        //Dropping the object on the world
        else if (_focusObj && Input.GetMouseButtonUp(0))
        {
            if (_deleteResource)
            {
                DestroyResource(_focusObj); // Destroys the object when hovering the trashcan
            }
            else
            { //dropped on the world
                
                _focusObj.transform.parent = hospital.transform; // parent to the hospital and add to the world
                GWorld.Instance.GetResourceQueue(_objResourceData.resourceData.resourceQueue.ToString()).AddResourse(_focusObj); 
                GWorld.Instance.GetWorld().ModifyState(_objResourceData.resourceData.resourceState.ToString(),1);
            }
            _focusObj.GetComponent<Collider>().enabled = true;
            // if(_focusObj.GetComponent<Collider>().enabled)                 Debug.Log("Collider Enabled");
            //rebuild navmesh and remove reference
            _surface.BuildNavMesh();
            _focusObj = null;
        }
        // Move the Object
        else if (_focusObj && Input.GetMouseButton(0))
        {
            //Shoot a Ray from the mouse position
            int layerMask = 1 << 8;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
                return;
            if (!_offsetCalc) // calculate offset from hitpoint to objects centerpoint
            {
                _clickOffset = hit.point - _focusObj.transform.position;
                _offsetCalc = true;
            }
            _goalPos = hit.point-_clickOffset; // assign new position for the object
            _focusObj.transform.position = _goalPos;
        }
        //Rotate Object
        if (_focusObj && Input.GetButtonDown("Horizontal"))
        {
            _focusObj.transform.Rotate(0,90*Input.GetAxis("Horizontal"),0);
        }
    }

    //Helper Methods to call from TrashCan event trigger to check if mouse is hovering it.
    public void onHoverTrashCan()
    {
        _deleteResource = true;
    }
    public void onHoverTrashCanExit()
    {
        _deleteResource = false;
    }

    public void SelectPrefab(int index)
    {
        newResoucePrefab = allResources[index];
    }
    private void DestroyResource(GameObject obj)
    {
        // GWorld.Instance.GetResourceQueue(_objResourceData.resourceData.resourceQueue.ToString()).RemoveResourse(obj);
        // GWorld.Instance.GetWorld().ModifyState(_objResourceData.resourceData.resourceState.ToString(),-1);
        Destroy(obj.gameObject);
    }
}

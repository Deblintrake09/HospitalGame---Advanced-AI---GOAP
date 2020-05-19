using UnityEngine;
using UnityEngine.AI;

public class WInterface : MonoBehaviour
{
    private GameObject _focusObj; // the object the mouse is controlling
    private Resource objResource;
    public GameObject newResoucePrefab; // the type of Resource we are setting down
    private Vector3 _goalPos = Vector3.zero; //position we want to put the gameObject
    [SerializeField] private Transform hospital;
    [SerializeField] private NavMeshSurface _surface;
    
    private Vector3 _clickOffset = Vector3.zero;
    private bool _offsetCalc = false;
    private bool _deleteResource = false; 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _offsetCalc = false;
            _clickOffset=Vector3.zero;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray.origin, ray.direction, out hit))
                return;
            if(hit.transform.GetComponent<Resource>()!=null)
            {
                _focusObj = hit.transform.gameObject;
            }
            else
            {
                _goalPos = hit.point;
                _focusObj = Instantiate(newResoucePrefab, _goalPos, newResoucePrefab.transform.rotation);
            }
            _focusObj.GetComponent<Collider>().enabled = false;
        }
        else if (_focusObj && Input.GetMouseButtonUp(0))
        {
            objResource = _focusObj.GetComponent<Resource>();
            if (_deleteResource)
            {
                DestroyResource(_focusObj);
            }
            else
            {
                _focusObj.transform.parent = hospital.transform;
                GWorld.Instance.GetResourceQueue(objResource.resourceData.resourceQueue.ToString()).AddResourse(_focusObj);
                GWorld.Instance.GetWorld().ModifyState(objResource.resourceData.resourceState.ToString(),1);
                _focusObj.GetComponent<Collider>().enabled = true;
            }
            _surface.BuildNavMesh();
            _focusObj = null;
        }
        
        else if (_focusObj && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray.origin, ray.direction, out hit))
                return;
            if (!_offsetCalc)
            {
                _clickOffset = hit.point - _focusObj.transform.position;
                _offsetCalc = true;
            }
            _goalPos = hit.point-_clickOffset;
            _focusObj.transform.position = _goalPos;
        }
        
        if (_focusObj && Input.GetButtonDown("Horizontal"))
        {
            _focusObj.transform.Rotate(0,90*Input.GetAxis("Horizontal"),0);
        }
    }

    public void onHoverTrashCan()
    {
        _deleteResource = true;
    }
    public void onHoverTrashCanExit()
    {
        _deleteResource = false;
    }

    private void DestroyResource(GameObject obj)
    {
        GWorld.Instance.GetResourceQueue(objResource.resourceData.resourceQueue.ToString()).RemoveResourse(obj);
        GWorld.Instance.GetWorld().ModifyState(objResource.resourceData.resourceState.ToString(),-1);
        Destroy(obj.gameObject);
    }
}

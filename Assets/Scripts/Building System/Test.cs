using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Grid grid; // Kéo và thả Grid vào trường này trong Inspector
    public LayerMask layerMask;
    public GameObject referenceObject;

    //public PlaceableObjectListSO placeableObjectListSO;
    public List<PlaceableObject> gameObjects;
    public int listIndex = -1;

    private void Start()
    {
        referenceObject = Instantiate(gameObjects[listIndex].gameObject);
    }
    void Update()
    {
        if(Input.mousePosition != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3Int lastPosition = new Vector3Int();
            mousePosition.z = Camera.main.nearClipPlane;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y), Mathf.RoundToInt(mousePosition.z)));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                lastPosition = new Vector3Int(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));
            }

            Vector3Int cellPosition = grid.WorldToCell(lastPosition);
            Vector3 cellCenterWorldPosition = grid.CellToWorld(cellPosition);
            Vector3Int cellCenterWorldPositionInt = new Vector3Int(Mathf.RoundToInt(cellCenterWorldPosition.x), Mathf.RoundToInt(cellCenterWorldPosition.y), Mathf.RoundToInt(cellCenterWorldPosition.z));
            if (Input.GetMouseButtonDown(0))
            {   
                referenceObject.GetComponent<PlaceableObject>().PlaceObject();
            }

            referenceObject.transform.position = cellCenterWorldPositionInt;
        }
        
    }

    public void OnChangeListIndex()
    {
        Destroy(referenceObject);

        referenceObject = Instantiate(gameObjects[listIndex].gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystemInputController : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private BuildingSystem buildingSystem;

    [SerializeField]
    private GameObject choicedObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Pressed !");
            buildingSystem.IncreasePlaceableObjectListIndex();
        }
        if (Input.mousePosition != null)
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
                buildingSystem.PlaceStructure(cellCenterWorldPositionInt);
                Debug.Log("Grid Pos : " + cellPosition);
            }

            choicedObject.transform.position = cellCenterWorldPositionInt;
        }
    }

    public void OnChangeChoiceObject(GameObject newObject)
    {
        Destroy(choicedObject);
        choicedObject = Instantiate(newObject);
        
        choicedObject.GetComponent<Collider>().enabled = false;
    }
}

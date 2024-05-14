using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> placedPositions;

    [SerializeField]
    private PlaceableObjectListSO placeableObjectListSO;
    [SerializeField]
    private List<PlaceableObject> placeableObjectList;
    [SerializeField]
    private int placeableObjectListIndex = -1;


    [SerializeField]
    private BuildingSystemInputController inputController;

    private void Start()
    {
        placeableObjectList = placeableObjectListSO.placeableOjects;
        inputController.OnChangeChoiceObject(placeableObjectList[placeableObjectListIndex].prefab);
    }

    public void AddToPlacedPositions(Vector3 position) {  placedPositions.Add(position); }

    public void PlaceStructure(Vector3 position)
    {
        PlaceableObject placeableObject = placeableObjectList[placeableObjectListIndex];

        float x = placeableObject.size.x / 2;
        float z = placeableObject.size.y / 2;

        if (placeableObject.size.x % 2 == 0)
        {
            x++;
        }
        if (placeableObject.size.y % 2 == 0)
        {
            z++;
        }

        int xOffSet = Mathf.CeilToInt(x) - 1;
        int zOffSet = Mathf.CeilToInt(z) - 1;

        Debug.Log("xOS = " + xOffSet + ", zOS = " + zOffSet);

        for (int i = - xOffSet; i <= xOffSet; i++)
        {
            for(int j = - zOffSet; j <= zOffSet; j++) 
            {
                Vector3 placePosition = new Vector3(position.x + i, 0, position.z + j) ;
                placedPositions.Add(placePosition);
            }
        }

        Instantiate(placeableObject.prefab, position, Quaternion.identity);
    }

    public bool CheckPlacedPosition(Vector3 position)
    {
        PlaceableObject placeableObject = placeableObjectList[placeableObjectListIndex];

        float x = placeableObject.size.x / 2;
        float z = placeableObject.size.y / 2;

        int xOffSet = Mathf.CeilToInt(x);
        int zOffSet = Mathf.CeilToInt(z);

        Debug.Log("xOS = " + xOffSet + ", zOS = " + zOffSet);

        for (int i = -xOffSet; i <= xOffSet; i++)
        {
            for (int j = -zOffSet; j <= zOffSet; j++)
            {
                Vector3 placePosition = new Vector3(position.x + i, 0, position.z + j);
                if (placedPositions.Contains(placePosition))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void IncreasePlaceableObjectListIndex()
    {
        placeableObjectListIndex++;

        if(placeableObjectListIndex == placeableObjectList.Count)
        {
            placeableObjectListIndex = 0;
        }

        OnChangePlaceableObjectListIndex();
    }

    private void OnChangePlaceableObjectListIndex()
    {
        inputController.OnChangeChoiceObject(placeableObjectList[placeableObjectListIndex].prefab);
    }
}

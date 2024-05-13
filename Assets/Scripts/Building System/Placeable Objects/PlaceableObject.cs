using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField]
    private PlaceableObjectType placeableObjectType;
    [SerializeField]
    private int id;
    [SerializeField]
    private GameObject mainObject;

    [SerializeField]
    private bool onPlaceTesting = true;

    [SerializeField]
    private bool placeable = true;

    [SerializeField]
    private List<Collider> colliders = new List<Collider>();

    private void Awake()
    {
        mainObject.GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!onPlaceTesting)
        {
            return;
        }

        if (placeableObjectType == PlaceableObjectType.structure)
        {
           
            if (other.CompareTag("Structure"))
            {
                placeable = false;
                colliders.Add(other);
            }
        }
        
        if(other.gameObject.tag == "Path")
        {
            if (other.gameObject.tag == "Environment")
            {
                placeable = false;
                colliders.Add(other);
            }
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!onPlaceTesting)
        {
            return;
        }

        colliders.Remove(other);
        if(colliders.Count == 0)
        {
            placeable = true;
        }
    }

    public void PlaceObject()
    {
        if (placeable)
        {
            GameObject go = Instantiate(mainObject, this.transform.position, Quaternion.identity);
            go.GetComponent<Collider>().enabled = true;
        }
        else
        {
            Debug.Log("Unplaceable");
        }
    }
}

public enum PlaceableObjectType
{
    structure,
    path,
}
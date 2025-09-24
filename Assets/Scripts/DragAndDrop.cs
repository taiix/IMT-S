using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    //Attach to the container

    private Transform objectToDrag;
    private Vector3 originalPosition;

    private bool dragging = false;

    private GameObject humanObject;
    private RectTransform[] placingPositions;

    [SerializeField] private float snapDistance = 300f;

    private Transform currentPlace;

    private void Start()
    {
        if (transform.GetChild(0) == null)
        {
            Debug.LogError("No child object found to drag!");
            return;
        }

        objectToDrag = transform.GetChild(0);
        originalPosition = objectToDrag.position;

        PlacingObjectsInit();
    }

    private void PlacingObjectsInit()
    {
        humanObject = GameObject.FindGameObjectWithTag("Human");

        if (humanObject == null)
        {
            Debug.LogWarning("No GameObject with tag 'Human' found.");
            return;
        }

        RectTransform[] allObjs = humanObject.GetComponentsInChildren<RectTransform>();
        RectTransform parentRect = humanObject.transform as RectTransform;

        int c = 0;

        for (int i = 0; i < allObjs.Length; i++)
        {
            if (allObjs[i] != parentRect) c++;
        }

        placingPositions = new RectTransform[c];
        int index = 0;
        for (int i = 0; i < allObjs.Length; i++)
        {
            if (allObjs[i] != parentRect)
            {
                placingPositions[index] = allObjs[i];
                index++;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        objectToDrag.position = eventData.position;
        dragging = true;
    }

    public void OnPointerDown(PointerEventData eventData) { }


    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;

        if (currentPlace == null)
            objectToDrag.position = originalPosition;
        else
            objectToDrag.position = currentPlace.position;
    }

    private void Update()
    {
        if (!dragging) return;

        GetClosestPlace();
    }

    private void GetClosestPlace()
    {
        Transform potentialPlace = null;
        float maxDist = Mathf.Infinity;

        for (int i = 0; i < placingPositions.Length; i++)
        {
            Transform place = placingPositions[i];
            if (place == null) continue;

            float dist = Vector2.Distance(objectToDrag.position, place.position);

            if (dist <= maxDist)
            {
                maxDist = dist;
                potentialPlace = place;
            }
        }

        currentPlace = potentialPlace;

        if (currentPlace != null)
        {
            if (Vector2.Distance(objectToDrag.position, currentPlace.position) <= snapDistance)
                Debug.DrawLine(objectToDrag.position, currentPlace.position);
            else 
                currentPlace = null;
        }
    }
}

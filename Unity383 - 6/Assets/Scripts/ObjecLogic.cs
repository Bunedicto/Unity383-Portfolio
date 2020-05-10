using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]

public class ObjecLogic : MonoBehaviour
{
    public Text message;
    public GameObject markerPrefab;

    private ARTrackedImageManager arTrackedImageManager;
    
    void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs _args)
    {
        message.text = "Image changed\n";
        foreach (var addedImage in _args.removed)
        {
            message.text += "Rem " + addedImage.referenceImage.name + "\n";
        }

        foreach (var updated in _args.updated)
        {
            //message.text += "Upd " + updated.referenceImage.name;
            if (updated.referenceImage.name.Equals ("Marker"))
            {
                GameObject g = Instantiate(markerPrefab);
                g.transform.position = updated.transform.position;
            }
        }
    }
    
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}

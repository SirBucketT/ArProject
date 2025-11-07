using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using UnityEngine.UI;
using TMPro;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] arPrefabs;

    private Dictionary<ARTrackedImage, GameObject> arObjectMap = new Dictionary<ARTrackedImage, GameObject>();

    public TMP_Text infoBox;


    void Update()
    {
        infoBox.text = "Tracking Data: \n";

        foreach (var trackedImage in arObjectMap.Keys)
        {
            infoBox.text += "Image: " + trackedImage.referenceImage.name + " " + trackedImage.trackingState + "\n";
        }
    }

    void OutputTracking()
    {

        infoBox.text = "Tracking Data: \n";

        int i = 0;
        foreach (var trackedImage in trackedImages.trackables)
        {         
            infoBox.text += "Image: " + trackedImage.referenceImage.name + " " + trackedImage.trackingState + "\n";

            if (trackedImage.trackingState == TrackingState.Limited)
            {
                arPrefabs[i].SetActive(false);
            }
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                arPrefabs[i].SetActive(true);
            }
            i++;

        }
           
    }

    void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Create object based on image tracked
        foreach (var trackedImage in eventArgs.added)
        {
            foreach (var arPrefab in arPrefabs)
            {
                if (trackedImage.referenceImage.name == arPrefab.name)
                {
                    var newPrefab = Instantiate(arPrefab, trackedImage.transform);
                    arObjectMap.Add(trackedImage, newPrefab); // Store mapping!
                }
            }
        }

        // Update tracking position and state
        foreach (var trackedImage in eventArgs.updated)
        {
            // Simply look up the corresponding object using the dictionary
            if (arObjectMap.TryGetValue(trackedImage, out GameObject arObject))
            {
                // Update the object's active state based on tracking state
                arObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            
                // Note: The position/rotation is automatically handled because the prefab 
                // was instantiated as a child of trackedImage.transform!
            }
        }
    
        // Clean up objects that are no longer tracked
        foreach (var trackedImage in eventArgs.removed)
        {
            if (arObjectMap.TryGetValue(trackedImage, out GameObject arObject))
            {
                Destroy(arObject);
                arObjectMap.Remove(trackedImage);
            }
        }
    }
}

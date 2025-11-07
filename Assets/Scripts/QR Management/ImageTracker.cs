using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] arPrefabs;
    private Dictionary<ARTrackedImage, GameObject> arObjectMap = new();
    public TMP_Text infoBox;

    void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        // ✅ Subscribe to event (don’t assign!)
        trackedImages.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    void OnDisable()
    {
        trackedImages.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            foreach (var prefab in arPrefabs)
            {
                if (prefab.name == trackedImage.referenceImage.name)
                {
                    GameObject spawned = Instantiate(prefab, trackedImage.transform);
                    arObjectMap[trackedImage] = spawned;
                    break;
                }
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (arObjectMap.TryGetValue(trackedImage, out var obj))
            {
                obj.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }
        
        foreach (var kvp in eventArgs.removed)
        {
            var trackedImage = kvp.Value;
            if (trackedImage != null && arObjectMap.TryGetValue(trackedImage, out var obj))
            {
                Destroy(obj);
                arObjectMap.Remove(trackedImage);
            }
        }
    }

    void Update()
    {
        infoBox.text = "Tracking Data:\n";
        foreach (var trackedImage in new List<ARTrackedImage>(arObjectMap.Keys))
        {
            infoBox.text += $"{trackedImage.referenceImage.name} - {trackedImage.trackingState}\n";
        }
    }
}

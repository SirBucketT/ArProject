using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

public class QRCodeContentController : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager m_TrackedImageManager;
    
    [Header("AR Content Mapping")]
    public ARContentMapping[] contentMappings;
    
    private Dictionary<string, GameObject> m_InstantiatedContent = new Dictionary<string, GameObject>();
    
    [System.Serializable]
    public struct ARContentMapping
    {
        public string qrCodeName; 
        public GameObject contentPrefab; 
    }
    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.updated)
        {
            string imageName = trackedImage.referenceImage.name; 

            if (string.IsNullOrEmpty(imageName))
            {
                continue; 
            }
            
            if (m_InstantiatedContent.TryGetValue(imageName, out GameObject content)) 
            {
                content.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            string imageName = trackedImage.referenceImage.name; 

            if (string.IsNullOrEmpty(imageName))
            {
                continue; 
            }

            if (m_InstantiatedContent.TryGetValue(imageName, out GameObject contentToRemove))
            {
            }
        }
    }

    private GameObject GetPrefabForName(string name)
    {
        foreach (var mapping in contentMappings)
        {
            if (mapping.qrCodeName == name)
            {
                return mapping.contentPrefab;
            }
        }
        return null;
    }
}
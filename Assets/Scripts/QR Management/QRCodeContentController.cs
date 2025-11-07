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
        foreach (var trackedImage in eventArgs.added)
        {
            string imageName = trackedImage.referenceImage.name;
            
            GameObject prefabToSpawn = GetPrefabForName(imageName);
            
            if (prefabToSpawn != null && !m_InstantiatedContent.ContainsKey(imageName))
            {
                GameObject newARContent = Instantiate(prefabToSpawn, trackedImage.transform);
                m_InstantiatedContent.Add(imageName, newARContent);
            }
        }
        
        foreach (var trackedImage in eventArgs.updated)
        {
            if (m_InstantiatedContent.TryGetValue(trackedImage.referenceImage.name, out GameObject content))
            {
                content.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }
        
        foreach (var trackedImage in eventArgs.removed)
        {
            string imageName = trackedImage.referenceImage.name;

            if (m_InstantiatedContent.TryGetValue(imageName, out GameObject contentToRemove))
            {
                Destroy(contentToRemove);
                m_InstantiatedContent.Remove(imageName);
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
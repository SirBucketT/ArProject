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
        // FIX: Use AddListener for the UnityEvent type.
        m_TrackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    void OnDisable()
    {
        // FIX: Use RemoveListener for the UnityEvent type.
        m_TrackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        // --- ADDED Images ---
        foreach (var entry in eventArgs.added)
        {
            // 🚨 FINAL FIX: Explicitly cast the entry to the required type. 
            // We are using 'var' for the entry, but casting it to ARTrackedImage.
            ARTrackedImage trackedImage = (ARTrackedImage)entry;

            // Now, use the ARTrackedImage.name property.
            string imageName = trackedImage.name; 
            
            GameObject prefabToSpawn = GetPrefabForName(imageName);
            
            if (prefabToSpawn != null && !m_InstantiatedContent.ContainsKey(imageName))
            {
                GameObject newARContent = Instantiate(prefabToSpawn, trackedImage.transform);
                m_InstantiatedContent.Add(imageName, newARContent);
            }
        }
        
        // --- UPDATED Images ---
        foreach (var entry in eventArgs.updated)
        {
            ARTrackedImage trackedImage = (ARTrackedImage)entry;
            
            if (m_InstantiatedContent.TryGetValue(trackedImage.name, out GameObject content))
            {
                content.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }

        // --- REMOVED Images ---
        foreach (var entry in eventArgs.removed)
        {
            ARTrackedImage trackedImage = (ARTrackedImage)entry;
            
            string imageName = trackedImage.name;

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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ImageTracker : MonoBehaviour
{
    [Header("AR Foundation Components")]
    [SerializeField] private ARTrackedImageManager trackedImages;

    [System.Serializable]
    public struct ImagePrefabPair
    {
        public string imageName;     
        public GameObject prefab;    
    }

    [SerializeField] private ImagePrefabPair[] imagePrefabPairs;

    [Header("UI Debug Output (optional)")]
    [SerializeField] private TMP_Text infoBox;

    private readonly Dictionary<string, GameObject> prefabMap = new();
    private readonly Dictionary<string, GameObject> spawnedPrefabs = new();

    void Awake()
    {
        foreach (var pair in imagePrefabPairs)
        {
            if (pair.imageName != null && pair.prefab != null)
            {
                prefabMap[pair.imageName] = pair.prefab;
            }
        }
    }

    void OnEnable()
    {
        trackedImages.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    void OnDisable()
    {
        trackedImages.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        // --- Added ---
        foreach (var trackedImage in eventArgs.added)
        {
            string imageName = trackedImage.referenceImage.name;

            if (prefabMap.TryGetValue(imageName, out var prefab))
            {
                if (!spawnedPrefabs.ContainsKey(imageName))
                {
                    GameObject spawned = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    spawnedPrefabs[imageName] = spawned;
                }
            }
            else
            {
                Debug.LogWarning($"No prefab mapped for reference image '{imageName}'");
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            string imageName = trackedImage.referenceImage.name;

            if (spawnedPrefabs.TryGetValue(imageName, out var spawned))
            {
                if (trackedImage.trackingState == TrackingState.Tracking)
                {
                    spawned.SetActive(true);
                    spawned.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
                }
                else if (trackedImage.trackingState == TrackingState.Limited)
                {
                    spawned.SetActive(true);
                }
                else 
                {
                    spawned.SetActive(false);
                }
            }
        }

        foreach (var kvp in eventArgs.removed)
        {
            var trackedImage = kvp.Value;
            if (trackedImage == null) continue;

            string imageName = trackedImage.referenceImage.name;
            if (spawnedPrefabs.TryGetValue(imageName, out var spawned))
            {
                Destroy(spawned);
                spawnedPrefabs.Remove(imageName);
            }
        }
    }

    void Update()
    {
        if (infoBox == null) return;

        infoBox.text = "Tracking Data:\n";
        foreach (var trackedImage in trackedImages.trackables)
        {
            infoBox.text += $"{trackedImage.referenceImage.name} - {trackedImage.trackingState}\n";
        }
    }
}

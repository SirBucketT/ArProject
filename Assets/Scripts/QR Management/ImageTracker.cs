using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class PersistentImageTracker : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImages;

    [System.Serializable]
    public struct ImagePrefabPair
    {
        public string imageName; 
        public GameObject prefab;
    }

    [SerializeField] private ImagePrefabPair[] imagePrefabPairs;
    [SerializeField] private TMP_Text infoBox;

    private readonly Dictionary<string, GameObject> prefabMap = new();

    private readonly Dictionary<string, GameObject> spawnedPrefabs = new();

    void Awake()
    {
        foreach (var pair in imagePrefabPairs)
        {
            if (pair.prefab != null && !string.IsNullOrEmpty(pair.imageName))
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
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnOrUpdate(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            SpawnOrUpdate(trackedImage);
        }
    }

    private void SpawnOrUpdate(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (!prefabMap.TryGetValue(imageName, out var prefab))
            return;

        if (!spawnedPrefabs.ContainsKey(imageName))
        {
            GameObject spawned = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedPrefabs[imageName] = spawned;
        }
        else
        {
            var spawned = spawnedPrefabs[imageName];
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                spawned.SetActive(true);
                spawned.transform.position = trackedImage.transform.position;
                spawned.transform.rotation = trackedImage.transform.rotation;
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ImageTracker : MonoBehaviour
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
    private readonly Dictionary<ARTrackedImage, GameObject> spawnedPrefabs = new();

    void Awake()
    {
        foreach (var pair in imagePrefabPairs)
        {
            prefabMap[pair.imageName] = pair.prefab;
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
            string imageName = trackedImage.referenceImage.name;

            if (prefabMap.TryGetValue(imageName, out var prefab))
            {
                if (!spawnedPrefabs.ContainsKey(trackedImage))
                {
                    GameObject spawned = Instantiate(prefab, trackedImage.transform);
                    spawned.transform.localPosition = Vector3.zero;
                    spawned.transform.localRotation = Quaternion.identity;

                    spawnedPrefabs[trackedImage] = spawned;
                }
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (spawnedPrefabs.TryGetValue(trackedImage, out var spawned))
            {
                spawned.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }

        foreach (var kvp in eventArgs.removed)
        {
            var trackedImage = kvp.Value;
            if (trackedImage == null) continue;

            if (spawnedPrefabs.TryGetValue(trackedImage, out var spawned))
            {
                Destroy(spawned);
                spawnedPrefabs.Remove(trackedImage);
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

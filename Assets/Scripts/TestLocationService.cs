using UnityEngine;
using System.Collections;
using TMPro;

public class TestLocationService : MonoBehaviour
{
    public static TestLocationService instance;
    
    public string location;
    
    [SerializeField] TMP_Text locationText;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
            Debug.Log("Location not enabled on device or app does not have permission to access location");
        
        float desiredAccuracyInMeters = 10f;
        float updateDistanceInMeters = 10f;
        
        Input.location.Start(desiredAccuracyInMeters, updateDistanceInMeters);
        
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            location = "Initialization timed out.";
            locationText.text = location;
            yield break;
        }
        
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            location = "Enable location service!";
            locationText.text = location;
            yield break;
        }
        else
        {
            LocationInfo lastData = Input.location.lastData;
            
            Debug.Log($"Location: {lastData.latitude} {lastData.longitude} {lastData.altitude}");
            
            location = $"{lastData.latitude:F6}, {lastData.longitude:F6}";
            
            locationText.text = "Location Service Enabled";
        }
        
        Input.location.Stop();
    }
}
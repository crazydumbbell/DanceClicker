using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobInitializer : MonoBehaviour
{
    void Start()
    {
        MobileAds.Initialize(initStatus => {
            string deviceId = SystemInfo.deviceUniqueIdentifier;
            Debug.Log("Your Test Device ID: " + deviceId);
        });
    }
}

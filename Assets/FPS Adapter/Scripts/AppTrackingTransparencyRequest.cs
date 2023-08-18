#if UNITY_IOS && EM_ATT
using EasyMobile;
#endif
using UnityEngine;

public class AppTrackingTransparencyRequest : MonoBehaviour
{
    private void Start()
    {
#if UNITY_IOS && EM_ATT
        var previousStatus = Privacy.AppTrackingManager.TrackingAuthorizationStatus;

        if (previousStatus == AppTrackingAuthorizationStatus.ATTrackingManagerAuthorizationStatusNotDetermined)
        {
            Privacy.AppTrackingManager.RequestTrackingAuthorization(status =>
            {
                Debug.Log("App Tracking transparency status: " + status);
                
                // If the user opts out of targeted advertising:
                var gdprMetaData = new MetaData("gdpr");
                
                gdprMetaData.Set("consent", 
                    status == AppTrackingAuthorizationStatus.ATTrackingManagerAuthorizationStatusAuthorized 
                        ? "true" : "false");
                Advertisement.SetMetaData(gdprMetaData);
            });
        }
#endif
    }
}
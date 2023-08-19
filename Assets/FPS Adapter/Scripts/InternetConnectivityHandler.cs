using UnityEngine;
using DTT.Networking.ConnectionStatus;
using HFPS.Systems;

namespace CC
{
    public class InternetConnectivityHandler : MonoBehaviour
    {
        private InternetStatus Status;
        private HFPS_GameManager GameManager;

        private void Start()
        {
            GameManager = HFPS_GameManager.Instance;
            InvokeRepeating(nameof(HandleInternetConnectivity), 5, 5);
        }

        void HandleInternetConnectivity()
        {
            if (GameManager.isPaused) return;

            Debug.Log("HandleInternetConnectivity called");

            if (InternetStatusManager.DefaultTarget.CurrentStatus != InternetStatus.ONLINE)
            {
                GameManager.OnDialog();
                MenuController.Instance.ShowPanel("NoInternetPanel");
            }
        }
    }
}
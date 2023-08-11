using System.Collections;
using HFPS.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CC
{
    public class FreeModeButtonController : MonoBehaviour
    {
        [SerializeField]
        private string appID, packageName, freeModeSceneName, sceneLoaderName = "SceneLoader";
        [SerializeField]
        private GameObject buyFreeModeButton, playFreeModeButton;
        
        void Start()
        {
#if UNITY_EDITOR
            Prefs.IsMyGameAlreadyRated = false;
#endif

            UpdateButtons();
        }

        private void UpdateButtons()
        {
#if EM_UIAP
            buyFreeModeButton.SetActive(true);
#else
            buyFreeModeButton.SetActive(false);
#endif

            if (Prefs.IsMyGameAlreadyRated || Prefs.HasBoughtFreeMode)
            {
                buyFreeModeButton.SetActive(false);
                playFreeModeButton.transform.GetChild(0).GetComponent<Text>().text = "Play";
            }

            if (FreeRewardChecker.IsApproving && buyFreeModeButton.activeSelf)
            {
                playFreeModeButton.SetActive(false);
            }
            else if (buyFreeModeButton.activeSelf == false && FreeRewardChecker.IsApproving)
            {
                playFreeModeButton.transform.GetChild(0).GetComponent<Text>().text = "Play";
            }
        }

        // If user has bought ghost mode or rated the game, just show play button
        // If it is original user then show normal dialog
        public void OnClickPlay()
        {
            UpdateButtons();

            if (Prefs.IsMyGameAlreadyRated || Prefs.HasBoughtFreeMode || FreeRewardChecker.IsApproving)
            {
                FreeMode();
            }
            else
            {
                MenuController.Instance.ShowPanel("RateUs");
            }
        }

        public void OnClickBuyFreeMode()
        {
#if EM_UIAP
            EasyMobile.InAppPurchasing.Purchase(EasyMobile.EM_IAPConstants.Product_Zombie_mode);
#endif

            UpdateButtons();
        }

        public void OnClickRateUs()
        {
            string url = "";
#if UNITY_ANDROID
        url = "market://details?id=" + packageName;
#elif UNITY_IPHONE
        url = "https://itunes.apple.com/app/id" + appID + "?action=write-review";
#endif
            Application.OpenURL(url);

            StartCoroutine(UnlockFreeLevel());
        }

        IEnumerator UnlockFreeLevel()
        {
            yield return new WaitForSeconds(3);

            Prefs.IsMyGameAlreadyRated = true;

            UpdateButtons();
        }

        private void FreeMode()
        {
            if (!string.IsNullOrEmpty(freeModeSceneName))
            {
                Prefs.Game_LoadState(0);
                Prefs.Game_SaveName(string.Empty);
                Prefs.Game_LevelName(freeModeSceneName);

                SceneManager.LoadScene(sceneLoaderName);
            }
            else
            {
                Debug.LogError("Free Mode Scene is empty!");
            }
        }
    }
}
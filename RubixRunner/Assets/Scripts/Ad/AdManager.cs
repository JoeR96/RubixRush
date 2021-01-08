using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.Scripts
{
    public class AdManager : MonoBehaviour
    {
        public static AdManager Instance { get; private set; }
        private string gameid = "3915263";
        private void Awake()
        {
            Advertisement.Initialize(gameid);
            Instance = this;

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                }
            }
        }

        public void PlayAd()
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
    }
}

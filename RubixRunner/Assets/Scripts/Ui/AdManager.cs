using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.Scripts
{
    public class AdManager : MonoBehaviour
    {
        private string gameid = "3915263";
        private void Awake()
        {
            Advertisement.Initialize(gameid);
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

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Code.Scripts.Manager.Cameras;

namespace Code.Scripts.UI
{
    public class CameraView : MonoBehaviour
    {
        public Button[] CameraIcons;
        public string[] CameraIdentifiers;

        private void Start()
        {
            for (int i = 0; i < CameraIcons.Length; i++)
            {
                int index = i;
                CameraIcons[index].onClick.AddListener(() => OnCameraIconClick(index));
            }
        }

        private void OnCameraIconClick(int index)
        {
            if (index >= 0 && index < CameraIdentifiers.Length)
            {
                var cameraIdentifier = CameraIdentifiers[index];
                CameraManager.SwitchCamera(cameraIdentifier);
            }
        }
    }
}
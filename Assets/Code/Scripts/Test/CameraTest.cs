using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Code.Scripts.Data;
using Code.Scripts.Manager.Cameras;

namespace Code.Scripts.Test
{
    public class CameraTest : MonoBehaviour
    {
        public CinemachineVirtualCamera[] VirtualCameras;
        public CameraData[] CameraDatas;

        private void Start()
        {
            for (int i = 0; i < VirtualCameras.Length; i++)
            {
                CameraManager.Register(CameraDatas[i], VirtualCameras[i]);
            }
        }
    }
}
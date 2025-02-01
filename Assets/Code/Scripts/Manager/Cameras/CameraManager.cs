using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Code.Scripts.Data;

namespace Code.Scripts.Manager.Cameras
{

    public static class CameraManager
    {
        private static readonly Dictionary<string, CinemachineVirtualCamera> CameraDict = new();
        private static readonly Dictionary<string, int> DefaultPriorities = new();

        public static void Register(CameraData data, CinemachineVirtualCamera virtualCamera)
        {
            if (!CameraDict.ContainsKey(data.index))
            {
                CameraDict.Add(data.index, virtualCamera);
                DefaultPriorities.Add(data.index, data.priority);
            }
        }

        public static void SwitchCamera(string index)
        {
            if (CameraDict.ContainsKey(index))
            {
                foreach (var cameraEntry in CameraDict)
                {
                    if (cameraEntry.Key == index)
                    {
                        cameraEntry.Value.Priority = 1;
                    }
                    else
                    {
                        cameraEntry.Value.Priority = DefaultPriorities[cameraEntry.Key];
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Camera with index {index} not found.");
            }
        }
    }
}
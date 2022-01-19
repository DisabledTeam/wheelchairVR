using System;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Create SceneNames", fileName = "SceneNames", order = 0)]
    public class SceneNames : ScriptableObject
    {
        public string ManagerScene = "ManagerScene";
        public string GunScene = "GunScene";
        public string WheelsScene = "WheelsScene";
        public string WheelsShootScene = "WheelsShootScene";
        public string WheelsShootNightScene = "WheelsShootNightScene";
        public string WheelsSwapDemonstrationScene = "WheelsSwapDemonstrationScene";

        public string GetSceneName(Scenes scenes)
        {
            return scenes switch
            {
                Scenes.ManagerScene => ManagerScene,
                Scenes.GunScene => GunScene,
                Scenes.WheelsMovementScene => WheelsScene,
                Scenes.WheelsShootScene => WheelsShootScene,
                Scenes.WheelsShootNightScene => WheelsShootNightScene,
                Scenes.WheelsSwapDemonstrationScene => WheelsSwapDemonstrationScene,
                _ => throw new ArgumentOutOfRangeException(nameof(scenes), scenes, null)
            };
        }
    }
}
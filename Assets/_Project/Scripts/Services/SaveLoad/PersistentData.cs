using System;
using System.Collections.Generic;
using _Project.Scripts.MinedResources;

namespace _Project.Scripts.Services.SaveLoad
{
    [Serializable]
    public class PersistentData
    {
        private const string FirstLevelName = "Level 1";

        public string CurrentLevelName;
        public Dictionary<ResourceType, int> PlayerResources;

        public PersistentData()
        {
            CurrentLevelName = FirstLevelName;
            PlayerResources = new Dictionary<ResourceType, int>();
        }
    }
}
using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts.Services.Path
{
    public class PersistentPathProvider : IPathProvider
    {
        private readonly StaticData _staticData;

        public PersistentPathProvider(StaticData staticData) =>
            _staticData = staticData;

        public string GetDataPath() =>
            System.IO.Path.Combine(Application.persistentDataPath, _staticData.SaveFileName);
    }
}
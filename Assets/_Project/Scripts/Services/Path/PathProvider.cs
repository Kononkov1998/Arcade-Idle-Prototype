using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts.Services.Path
{
    public class PathProvider : IPathProvider
    {
        private readonly StaticData _staticData;

        public PathProvider(StaticData staticData) =>
            _staticData = staticData;

        public string GetDataPath() =>
            System.IO.Path.Combine(Application.dataPath, _staticData.SaveFileName);
    }
}
using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class UniqueId : MonoBehaviour
    {
        public string _id;
        public string Id => _id;

        public void GenerateId() =>
            _id = $"{gameObject.scene.name.Replace(' ', '_')}_{Guid.NewGuid().ToString()}";
    }
}
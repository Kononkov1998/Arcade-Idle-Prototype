using System;
using _Project.Scripts.Extensions;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Tutorial
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float _showHideDuration = 0.2f;
        public bool Visible { get; private set; }

        public void AttachTo(Transform target)
        {
            transform.SetParent(target);
            transform.localPosition = VectorFactory.CreateY(Vector3.zero, transform.position.y);
        }

        public void Show()
        {
            Visible = true;
            transform.DOScale(Vector3.one, _showHideDuration);
        }

        public void Hide(Action onComplete = null)
        {
            Visible = false;
            transform.DOScale(Vector3.zero, _showHideDuration)
                .OnComplete(() => onComplete?.Invoke());
        }
    }
}
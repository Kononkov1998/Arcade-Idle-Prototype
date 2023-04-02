using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Player
{
    public class PlayerInteractorProgress : MonoBehaviour
    {
        [SerializeField] private PlayerInteractor _interactor;
        [SerializeField] private Image _image;
        [SerializeField] private Image _fill;

        private void Start() =>
            _interactor.InteractionProgress.Subscribe(OnStayingTimeChanged);

        private void OnStayingTimeChanged(float stayingTime)
        {
            if (stayingTime == 0f)
            {
                _image.gameObject.SetActive(false);
                return;
            }

            if (!_image.gameObject.activeSelf)
                _image.gameObject.SetActive(true);

            _fill.fillAmount = stayingTime / _interactor.TimeToHit;
        }
    }
}
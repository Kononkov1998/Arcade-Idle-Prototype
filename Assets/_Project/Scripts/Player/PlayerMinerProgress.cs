using _Project.Scripts.Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMinerProgress : MonoBehaviour
{
    [SerializeField] private PlayerMiner _miner;
    [SerializeField] private Image _image;
    [SerializeField] private Image _fill;

    private void Start()
    {
        _miner.StayingTime.Subscribe(OnStayingTimeChanged);
    }

    private void OnStayingTimeChanged(float stayingTime)
    {
        if (stayingTime == 0f)
        {
            _image.gameObject.SetActive(false);
            return;
        }

        if (!_image.gameObject.activeSelf)
            _image.gameObject.SetActive(true);

        _fill.fillAmount = stayingTime / _miner.TimeToHit;
    }
}

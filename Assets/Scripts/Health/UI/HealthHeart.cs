using System;
using UnityEngine;
using UnityEngine.UI;

namespace Health.UI
{
    public class HealthHeart : MonoBehaviour
    {
        //Sprites
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _halfHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetHeartImage(HeartStatus status)
        {
            switch (status)
            {
                case HeartStatus.Empty:
                    _image.sprite = _emptyHeartSprite;
                    break;
                case HeartStatus.Half:
                    _image.sprite = _halfHeartSprite;
                    break;
                case HeartStatus.Full:
                    _image.sprite = _fullHeartSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }

    public enum HeartStatus
    {
        Empty = 0, Half = 1, Full = 2
    }
}

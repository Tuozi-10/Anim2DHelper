using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField]
        private Slider m_slider;

        private void Awake()
        {
            m_slider.onValueChanged.AddListener(UpdateGravity);
        }

        private void UpdateGravity(float value)
        {
            Physics2D.gravity = new Vector2(0, -value);
        }
    }
}
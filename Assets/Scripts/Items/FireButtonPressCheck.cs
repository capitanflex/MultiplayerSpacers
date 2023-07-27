using UnityEngine;
using UnityEngine.EventSystems;

namespace Items
{
    public class FireButtonPressCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static FireButtonPressCheck FireButton;
        public bool isButtonPressed;

        private void Awake()
        {
            FireButton = this;
        }

        public void OnPointerDown(PointerEventData eventData)
        {      
            isButtonPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isButtonPressed = false;
        }
    
    }
}


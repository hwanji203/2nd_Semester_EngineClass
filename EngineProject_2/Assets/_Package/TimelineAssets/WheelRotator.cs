using UnityEngine;

namespace _Scripts.TimelineAssets
{
    public class WheelRotator : MonoBehaviour
    {
        public float rotateValue = 360f;

        private void Update()
        {
            transform.Rotate(rotateValue * Time.deltaTime, 0, 0);
        }
    }
}
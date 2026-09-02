using System;
using UnityEngine.Playables;

namespace _Scripts.TimelineAssets
{
    [Serializable]
    public class SlowMotionBehaviour : PlayableBehaviour
    {
        public float timeScaleValue = 0.3f;
    }
}
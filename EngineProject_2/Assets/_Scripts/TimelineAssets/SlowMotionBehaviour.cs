using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Playables;
using Application = UnityEngine.Device.Application;

namespace _Scripts.TimelineAssets
{
    public class SlowMotionBehaviour : PlayableBehaviour
    {
        public float timeScaleValue = 0.3f;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (Application.isPlaying)
                Time.timeScale = timeScaleValue;
        }
    }
}
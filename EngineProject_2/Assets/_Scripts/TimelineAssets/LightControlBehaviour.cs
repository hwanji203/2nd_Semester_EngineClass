using UnityEngine;
using UnityEngine.Playables;

namespace _Scripts.TimelineAssets
{
    public class LightControlBehaviour : PlayableBehaviour
    {
        public Color color = Color.white;
        public float intensity = 1.0f;

        public override void ProcessFrame(Playable playable, FrameData info, object playableData)
        {
            Light light = playableData as Light;
            if (light != null)
            {
                light.color = color;
                light.intensity = intensity;
            }
        }
    }
}
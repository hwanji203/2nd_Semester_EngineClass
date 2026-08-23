using UnityEngine;
using UnityEngine.Playables;

namespace _Scripts.TimelineAssets
{
    public class LightControlAsset : PlayableAsset
    {
        public ExposedReference<Light> targetLight;
        public Color color = Color.white;
        public float intensity = 1.0f;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<LightControlBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();

            // behaviour.light = targetLight.Resolve(graph.GetResolver());
            behaviour.color = color;
            behaviour.intensity = intensity;
            
            return playable;
        }
    }
}
using UnityEngine;
using UnityEngine.Playables;

namespace _Scripts.TimelineAssets
{
    public class SlowMotionAsset : PlayableAsset
    {
        public float timeScaleValue = 1f;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<SlowMotionBehaviour> playable = ScriptPlayable<SlowMotionBehaviour>.Create(graph);

            SlowMotionBehaviour behaviour = playable.GetBehaviour();
            behaviour.timeScaleValue = timeScaleValue;
            
            return playable;
        }
    }
}
using UnityEngine;
using UnityEngine.Playables;

namespace _Scripts.TimelineAssets
{
    public class SlowMotionAsset : PlayableAsset
    {
        public SlowMotionBehaviour template;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<SlowMotionBehaviour> playable = ScriptPlayable<SlowMotionBehaviour>.Create(graph, template);
            
            return playable;
        }
    }
}
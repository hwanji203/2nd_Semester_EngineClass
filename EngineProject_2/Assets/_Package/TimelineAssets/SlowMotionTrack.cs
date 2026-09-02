using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace _Scripts.TimelineAssets
{
    [TrackClipType(typeof(SlowMotionAsset))]
    public class SlowMotionTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<SlowMotionMixer>.Create(graph, inputCount);
        }
    }
}

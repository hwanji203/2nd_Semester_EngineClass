using UnityEngine;
using UnityEngine.Playables;

namespace _Scripts.TimelineAssets
{
    public class SlowMotionMixer : PlayableBehaviour
    {
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            int inputCount = playable.GetInputCount(); //겹쳐있는 에셋 수

            float finalTime = 0;
            
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                var inputPlayable = (ScriptPlayable<SlowMotionBehaviour>)playable.GetInput(i);
                SlowMotionBehaviour behaviour = inputPlayable.GetBehaviour();
                
                finalTime += behaviour.timeScaleValue * inputWeight;
            }
            Debug.Log(finalTime);
            Time.timeScale = finalTime;
        }
    }
}
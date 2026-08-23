using UnityEngine;
using UnityEngine.Playables;

namespace _Scripts.TimelineAssets
{
    public class LightControlMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Light targetBindingLight = playerData as Light;
            float finalIntensity = 0f;
            Color finalColor = Color.black;

            if (!targetBindingLight)
                return;
            
            int inputCount = playable.GetInputCount(); //겹쳐있는 에셋 수

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                var inputPlayable = (ScriptPlayable<LightControlBehaviour>)playable.GetInput(i);
                LightControlBehaviour behaviour = inputPlayable.GetBehaviour();
                
                finalIntensity += behaviour.intensity * inputWeight;
                finalColor += behaviour.color * inputWeight;
            }
            
            targetBindingLight.intensity = finalIntensity;
            targetBindingLight.color = finalColor;
        }
    }
}
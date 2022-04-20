#if UNITY_TIMELINE_ENABLED
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Vox.Blend;


namespace Vox.Blend.Timeline
{

    public class BlendShapeControllerPlayableMixerBehaviour : PlayableBehaviour
    {
        // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            BlendShapeController trackBinding = playerData as BlendShapeController;

            if (!trackBinding)
                return;

            var inputCount = playable.GetInputCount();

            var blendValue01 = 0f;
            var blendValue02 = 0f;
            var blendValue03 = 0f;
            var blendValue04 = 0f;
            var blendValue05 = 0f;
            var blendValue06 = 0f;
            var blendValue07 = 0f;
            var blendValue08 = 0f;

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<BlendShapeControllerPlayableBehaviour> inputPlayable =
                    (ScriptPlayable<BlendShapeControllerPlayableBehaviour>)playable.GetInput(i);
                BlendShapeControllerPlayableBehaviour input = inputPlayable.GetBehaviour();

                // Use the above variables to process each frame of this playable.

                blendValue01 += inputWeight * input.bindValue01;
                blendValue02 += inputWeight * input.bindValue02;
                blendValue03 += inputWeight * input.bindValue03;
                blendValue04 += inputWeight * input.bindValue04;
                blendValue05 += inputWeight * input.bindValue05;
                blendValue06 += inputWeight * input.bindValue06;
                blendValue07 += inputWeight * input.bindValue07;
                blendValue08 += inputWeight * input.bindValue08;
            }

            trackBinding.Value01 = blendValue01;
            trackBinding.Value02 = blendValue02;
            trackBinding.Value03 = blendValue03;
            trackBinding.Value04 = blendValue04;
            trackBinding.Value05 = blendValue05;
            trackBinding.Value06 = blendValue06;
            trackBinding.Value07 = blendValue07;
            trackBinding.Value08 = blendValue08;
        }
    }
}

#endif
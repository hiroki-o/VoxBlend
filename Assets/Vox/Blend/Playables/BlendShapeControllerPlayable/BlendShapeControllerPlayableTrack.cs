using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Vox.Blend;

[TrackColor(1f, 0.4f, 0f)]
[TrackClipType(typeof(BlendShapeControllerPlayableClip))]
[TrackBindingType(typeof(BlendShapeController))]
public class BlendShapeControllerPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<BlendShapeControllerPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}

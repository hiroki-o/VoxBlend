using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Vox.Blend;

[Serializable]
public class BlendShapeControllerPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public BlendShapeControllerPlayableBehaviour template = new BlendShapeControllerPlayableBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.All; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<BlendShapeControllerPlayableBehaviour>.Create (graph, template);
    }
}

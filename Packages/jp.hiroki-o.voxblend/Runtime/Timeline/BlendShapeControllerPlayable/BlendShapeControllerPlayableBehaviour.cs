#if UNITY_TIMELINE_ENABLED
using System;
using UnityEngine.Playables;

namespace Vox.Blend.Timeline
{
    [Serializable]
    public class BlendShapeControllerPlayableBehaviour : PlayableBehaviour
    {
        public float bindValue01;
        public float bindValue02;
        public float bindValue03;
        public float bindValue04;
        public float bindValue05;
        public float bindValue06;
        public float bindValue07;
        public float bindValue08;
    }
}

#endif
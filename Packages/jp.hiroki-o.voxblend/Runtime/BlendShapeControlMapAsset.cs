using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vox.Blend
{
    /*
     * Asset stores key control config.
     */
    [CreateAssetMenu(fileName = "New BlendShapeControlMap", menuName = "BlendShape Control Map", order = 650)]
    public class BlendShapeControlMapAsset : ScriptableObject
    {
        [Serializable]
        public class BlendShapeControlSet
        {
            [SerializeField] private string m_name;
            [SerializeField] private List<BlendShapeControl> m_controls;

            public BlendShapeControlSet()
            {
                m_controls = new List<BlendShapeControl>();
            }

            public string Name
            {
                get { return m_name; }
                set { m_name = value; }
            }

            public List<BlendShapeControl> Controls
            {
                get { return m_controls; }
                set { m_controls = value; }
            }

            public BlendShapeControlSet(string name)
            {
                m_name = name;
                m_controls = new List<BlendShapeControl>();
            }
        }

        [Serializable]
        public class BlendShapeControl
        {
            [SerializeField] private string m_meshName;
            [SerializeField] private string m_blendShapeName;
            [SerializeField] private Vector2 m_range;
            [SerializeField] private float m_defaultValue;

            public string MeshName
            {
                get { return m_meshName; }
                set { m_meshName = value; }
            }

            public string BlendShapeName
            {
                get { return m_blendShapeName; }
                set { m_blendShapeName = value; }
            }

            public Vector2 Range
            {
                get { return m_range; }
                set { m_range = value; }
            }

            public float DefaultValue
            {
                get => m_defaultValue;
                set => m_defaultValue = value;
            }

            public BlendShapeControl(string meshName, string blendShapeName, Vector2 r)
            {
                m_meshName = meshName;
                m_blendShapeName = blendShapeName;
                m_range = r;
            }
        }

        [SerializeField] private GameObject m_targetModel = null;
        [SerializeField] private List<BlendShapeControlSet> controlSets = null;

        // Properties (Get)
        public List<BlendShapeControlSet> ControlSets => controlSets;
        public GameObject TargetModel => m_targetModel;

        public void Reset(GameObject target)
        {
            m_targetModel = target;
        }
    }
}
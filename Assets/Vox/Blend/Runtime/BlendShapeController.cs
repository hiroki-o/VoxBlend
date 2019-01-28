using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Vox.Blend
{
    /*
     * Asset stores key control config.
     */
    [ExecuteInEditMode]
    public class BlendShapeController : MonoBehaviour
    {
        [Serializable]
        public struct BlendShapeMapBinding
        {
            public bool enabled;
            public int setIndex;
            public float value;

            public BlendShapeMapBinding(bool e, int index = -1, float v = 0f)
            {
                enabled = e;
                setIndex = index;
                value = v;
            }
        } 
        
        [SerializeField] private BlendShapeControlMapAsset m_controlMap = null;
        [SerializeField] private bool m_ensureMeshNameMatch = true;

        [SerializeField] private BlendShapeMapBinding m_binding01 = new BlendShapeMapBinding();
        [SerializeField] private BlendShapeMapBinding m_binding02 = new BlendShapeMapBinding();
        [SerializeField] private BlendShapeMapBinding m_binding03 = new BlendShapeMapBinding();
        [SerializeField] private BlendShapeMapBinding m_binding04 = new BlendShapeMapBinding();
        [SerializeField] private BlendShapeMapBinding m_binding05 = new BlendShapeMapBinding();
        [SerializeField] private BlendShapeMapBinding m_binding06 = new BlendShapeMapBinding();
        [SerializeField] private BlendShapeMapBinding m_binding07 = new BlendShapeMapBinding();
        [SerializeField] private BlendShapeMapBinding m_binding08 = new BlendShapeMapBinding();

        private BlendShapeRuntimeControl m_runtimeControl;

        public float Value01
        {
            set => m_binding01.value = value;
        }
        public float Value02
        {
            set => m_binding02.value = value;
        }
        public float Value03
        {
            set => m_binding03.value = value;
        }
        public float Value04
        {
            set => m_binding04.value = value;
        }
        public float Value05
        {
            set => m_binding05.value = value;
        }
        public float Value06
        {
            set => m_binding06.value = value;
        }
        public float Value07
        {
            set => m_binding07.value = value;
        }
        public float Value08
        {
            set => m_binding08.value = value;
        }

        private void Start()
        {
            m_runtimeControl = new BlendShapeRuntimeControl(gameObject, m_controlMap, m_ensureMeshNameMatch);
        }

        private void Update()
        {
            if (m_binding01.enabled) m_runtimeControl.Set[m_binding01.setIndex].axisValue = m_binding01.value;
            if (m_binding02.enabled) m_runtimeControl.Set[m_binding02.setIndex].axisValue = m_binding02.value;
            if (m_binding03.enabled) m_runtimeControl.Set[m_binding03.setIndex].axisValue = m_binding03.value;
            if (m_binding04.enabled) m_runtimeControl.Set[m_binding04.setIndex].axisValue = m_binding04.value;
            if (m_binding05.enabled) m_runtimeControl.Set[m_binding05.setIndex].axisValue = m_binding05.value;
            if (m_binding06.enabled) m_runtimeControl.Set[m_binding06.setIndex].axisValue = m_binding06.value;
            if (m_binding07.enabled) m_runtimeControl.Set[m_binding07.setIndex].axisValue = m_binding07.value;
            if (m_binding08.enabled) m_runtimeControl.Set[m_binding08.setIndex].axisValue = m_binding08.value;
        }

        public void InitializeRuntimeControl()
        {
            m_runtimeControl = new BlendShapeRuntimeControl(gameObject, m_controlMap, m_ensureMeshNameMatch);
        }

        private void LateUpdate()
        {
            m_runtimeControl?.ApplyBlendShapeControlWeights();
        }
    }
}
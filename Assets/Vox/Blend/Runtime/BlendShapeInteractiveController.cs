using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vox.Blend
{
    public class BlendShapeInteractiveController : MonoBehaviour
    {
        [Serializable]
        public struct BlendShapeMapBinding
        {
            public int axisIndex;
            public string inputAxis;

            public BlendShapeMapBinding(int index = -1, string input = "")
            {
                axisIndex = index;
                inputAxis = input;
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
        private List<BlendShapeMapBinding> m_bindings;

        private void Start()
        {
            m_runtimeControl = new BlendShapeRuntimeControl(gameObject, m_controlMap, m_ensureMeshNameMatch);

            m_bindings = new List<BlendShapeMapBinding>();
            if (m_binding01.axisIndex >= 0 && !string.IsNullOrEmpty(m_binding01.inputAxis)) m_bindings.Add(m_binding01);
            if (m_binding02.axisIndex >= 0 && !string.IsNullOrEmpty(m_binding02.inputAxis)) m_bindings.Add(m_binding02);
            if (m_binding03.axisIndex >= 0 && !string.IsNullOrEmpty(m_binding03.inputAxis)) m_bindings.Add(m_binding03);
            if (m_binding04.axisIndex >= 0 && !string.IsNullOrEmpty(m_binding04.inputAxis)) m_bindings.Add(m_binding04);
            if (m_binding05.axisIndex >= 0 && !string.IsNullOrEmpty(m_binding05.inputAxis)) m_bindings.Add(m_binding05);
            if (m_binding06.axisIndex >= 0 && !string.IsNullOrEmpty(m_binding06.inputAxis)) m_bindings.Add(m_binding06);
            if (m_binding07.axisIndex >= 0 && !string.IsNullOrEmpty(m_binding07.inputAxis)) m_bindings.Add(m_binding07);
            if (m_binding08.axisIndex >= 0 && !string.IsNullOrEmpty(m_binding08.inputAxis)) m_bindings.Add(m_binding08);
        }

        private void Update()
        {
            foreach (var b in m_bindings)
            {
                m_runtimeControl.Set[b.axisIndex].axisValue = Input.GetAxis(b.inputAxis);
            }
        }

        private void LateUpdate()
        {
            m_runtimeControl.ApplyBlendShapeControlWeights();
        }
    }
}
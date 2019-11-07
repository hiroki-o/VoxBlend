using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Vox.Blend
{
    public class BlendShapeInteractiveController : MonoBehaviour
    {
        [Serializable]
        public struct BlendShapeMapBinding
        {
            public bool enabled;
            public int setIndex;
            public string inputAxis;

            public BlendShapeMapBinding(bool e = false, int index = -1, string input = "")
            {
                enabled = e;
                setIndex = index;
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

        private void Start()
        {
            m_runtimeControl = new BlendShapeRuntimeControl(gameObject, m_controlMap, m_ensureMeshNameMatch);
        }

        private void Update()
        {
            if (m_runtimeControl == null)
            {
                return;
            }
            if (m_binding01.enabled) m_runtimeControl.Set[m_binding01.setIndex].axisValue = Input.GetAxis(m_binding01.inputAxis);
            if (m_binding02.enabled) m_runtimeControl.Set[m_binding02.setIndex].axisValue = Input.GetAxis(m_binding02.inputAxis);
            if (m_binding03.enabled) m_runtimeControl.Set[m_binding03.setIndex].axisValue = Input.GetAxis(m_binding03.inputAxis);
            if (m_binding04.enabled) m_runtimeControl.Set[m_binding04.setIndex].axisValue = Input.GetAxis(m_binding04.inputAxis);
            if (m_binding05.enabled) m_runtimeControl.Set[m_binding05.setIndex].axisValue = Input.GetAxis(m_binding05.inputAxis);
            if (m_binding06.enabled) m_runtimeControl.Set[m_binding06.setIndex].axisValue = Input.GetAxis(m_binding06.inputAxis);
            if (m_binding07.enabled) m_runtimeControl.Set[m_binding07.setIndex].axisValue = Input.GetAxis(m_binding07.inputAxis);
            if (m_binding08.enabled) m_runtimeControl.Set[m_binding08.setIndex].axisValue = Input.GetAxis(m_binding08.inputAxis);
        }

        private void LateUpdate()
        {
            m_runtimeControl?.ApplyBlendShapeControlWeights();
        }
        
        public void InitializeRuntimeControl()
        {
            if (m_controlMap != null)
            {
                m_runtimeControl = new BlendShapeRuntimeControl(gameObject, m_controlMap, m_ensureMeshNameMatch);
            }
        }
    }
}
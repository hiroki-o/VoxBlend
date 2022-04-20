#if UNITY_INPUTSYSTEM_ENABLED
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Vox.Blend
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInput))]
    public class BlendShapeInteractiveController : MonoBehaviour
    {
        
        [Serializable]
        public struct BlendShapeMapBinding
        {
            public bool enabled;
            public int setIndex;
            public string action;
            public Action<InputAction.CallbackContext> callback;

            public BlendShapeMapBinding(bool e = false, int index = -1, string inputAction = "")
            {
                enabled = e;
                setIndex = index;
                action = inputAction;
                callback = null;
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
        private PlayerInput m_playerInput;

        private void Awake()
        {
            TryGetComponent(out m_playerInput);
        }

        private void OnEnable()
        {
            if (m_binding01.enabled)
            {
                m_binding01.callback = context =>
                {
                    if (m_binding01.enabled && m_runtimeControl != null)
                        m_runtimeControl.Set[m_binding01.setIndex].axisValue = context.ReadValue<float>();
                };
                m_playerInput.actions[m_binding01.action].performed += m_binding01.callback;
            } 
        }

        private void OnDisable()
        {
            m_playerInput.actions[m_binding01.action].performed -= m_binding01.callback;
        }

        private void Start()
        {
            m_runtimeControl = new BlendShapeRuntimeControl(gameObject, m_controlMap, m_ensureMeshNameMatch);
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
#endif

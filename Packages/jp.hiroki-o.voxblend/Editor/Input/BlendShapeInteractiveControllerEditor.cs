#if UNITY_INPUTSYSTEM_ENABLED
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Xml.Schema;
using UnityEngine.InputSystem;

namespace Vox.Blend {

	[CustomEditor(typeof(BlendShapeInteractiveController))]
	public class BlendShapeInteractiveControllerEditor : Editor {

		private BlendShapeInteractiveController m_currentTarget;
		private BlendShapeControlMapAsset m_currentAsset;
		private string[] m_setNames;
		private string[] m_actionNames;
		private GUIContent m_empty;

		private SerializedProperty m_prop_controlMap = null;
		private SerializedProperty m_prop_ensureMeshNameMatch = null;
		private SerializedProperty m_prop_b01_setIndex = null;
		private SerializedProperty m_prop_b02_setIndex = null;
		private SerializedProperty m_prop_b03_setIndex = null;
		private SerializedProperty m_prop_b04_setIndex = null;
		private SerializedProperty m_prop_b05_setIndex = null;
		private SerializedProperty m_prop_b06_setIndex = null;
		private SerializedProperty m_prop_b07_setIndex = null;
		private SerializedProperty m_prop_b08_setIndex = null;
		private SerializedProperty m_prop_b01_action = null;
		private SerializedProperty m_prop_b02_action = null;
		private SerializedProperty m_prop_b03_action = null;
		private SerializedProperty m_prop_b04_action = null;
		private SerializedProperty m_prop_b05_action = null;
		private SerializedProperty m_prop_b06_action = null;
		private SerializedProperty m_prop_b07_action = null;
		private SerializedProperty m_prop_b08_action = null;
		private SerializedProperty m_prop_b01_enabled = null;
		private SerializedProperty m_prop_b02_enabled = null;
		private SerializedProperty m_prop_b03_enabled = null;
		private SerializedProperty m_prop_b04_enabled = null;
		private SerializedProperty m_prop_b05_enabled = null;
		private SerializedProperty m_prop_b06_enabled = null;
		private SerializedProperty m_prop_b07_enabled = null;
		private SerializedProperty m_prop_b08_enabled = null;

		private void ResetEditor()
		{
			m_currentTarget = target as BlendShapeInteractiveController;
			m_actionNames = null;

			m_prop_controlMap = serializedObject.FindProperty("m_controlMap");
			m_prop_ensureMeshNameMatch = serializedObject.FindProperty("m_ensureMeshNameMatch");
			m_prop_b01_setIndex = serializedObject.FindProperty("m_binding01.setIndex");
			m_prop_b02_setIndex = serializedObject.FindProperty("m_binding02.setIndex");
			m_prop_b03_setIndex = serializedObject.FindProperty("m_binding03.setIndex");
			m_prop_b04_setIndex = serializedObject.FindProperty("m_binding04.setIndex");
			m_prop_b05_setIndex = serializedObject.FindProperty("m_binding05.setIndex");
			m_prop_b06_setIndex = serializedObject.FindProperty("m_binding06.setIndex");
			m_prop_b07_setIndex = serializedObject.FindProperty("m_binding07.setIndex");
			m_prop_b08_setIndex = serializedObject.FindProperty("m_binding08.setIndex");
			m_prop_b01_action = serializedObject.FindProperty("m_binding01.action");
			m_prop_b02_action = serializedObject.FindProperty("m_binding02.action");
			m_prop_b03_action = serializedObject.FindProperty("m_binding03.action");
			m_prop_b04_action = serializedObject.FindProperty("m_binding04.action");
			m_prop_b05_action = serializedObject.FindProperty("m_binding05.action");
			m_prop_b06_action = serializedObject.FindProperty("m_binding06.action");
			m_prop_b07_action = serializedObject.FindProperty("m_binding07.action");
			m_prop_b08_action = serializedObject.FindProperty("m_binding08.action");
			m_prop_b01_enabled = serializedObject.FindProperty("m_binding01.enabled");
			m_prop_b02_enabled = serializedObject.FindProperty("m_binding02.enabled");
			m_prop_b03_enabled = serializedObject.FindProperty("m_binding03.enabled");
			m_prop_b04_enabled = serializedObject.FindProperty("m_binding04.enabled");
			m_prop_b05_enabled = serializedObject.FindProperty("m_binding05.enabled");
			m_prop_b06_enabled = serializedObject.FindProperty("m_binding06.enabled");
			m_prop_b07_enabled = serializedObject.FindProperty("m_binding07.enabled");
			m_prop_b08_enabled = serializedObject.FindProperty("m_binding08.enabled");

			m_empty = new GUIContent();
		}

		private bool InitializeSetNames()
		{
			var asset = m_prop_controlMap.objectReferenceValue as BlendShapeControlMapAsset;

			if (m_currentAsset != asset)
			{
				m_setNames = null != asset ? 
					asset.ControlSets.Select(c => c.Name).ToArray() : 
					null;
				m_currentAsset = asset;
			}

			return m_setNames != null;
		}

		private bool GetInputActionNames()
		{
			if (m_actionNames != null)
				return true;
			
			if (m_currentTarget.TryGetComponent<PlayerInput>(out var playerInput))
			{
				if (playerInput.actions == null)
				{
					return false;
				}
				
				m_actionNames = playerInput.actions.Select(a => a.name).ToArray();
			}
			else
			{
				m_actionNames = null;
			}

			return m_actionNames != null;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			if (m_currentTarget != target)
			{
				ResetEditor();
			}

			EditorGUILayout.ObjectField(m_prop_controlMap);
			EditorGUILayout.PropertyField(m_prop_ensureMeshNameMatch, new GUIContent("Mesh Name Match"), false);

			if (!GetInputActionNames())
			{
				EditorGUILayout.HelpBox("Input Action Asset needs to be assigned in PlayerInput.", MessageType.Error);
				return;
			}

			if (InitializeSetNames())
			{
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b01_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b01_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b01_setIndex.intValue, m_setNames);
						if (idx != m_prop_b01_setIndex.intValue) m_prop_b01_setIndex.intValue = idx;
						var aIdx = EditorGUILayout.Popup(Array.IndexOf(m_actionNames, m_prop_b01_action.stringValue), m_actionNames);
						if (aIdx >= 0)
						{
							m_prop_b01_action.stringValue = m_actionNames[aIdx];
						}
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b02_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b02_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b02_setIndex.intValue, m_setNames);
						if (idx != m_prop_b02_setIndex.intValue) m_prop_b02_setIndex.intValue = idx;
						var aIdx = EditorGUILayout.Popup(Array.IndexOf(m_actionNames, m_prop_b02_action.stringValue), m_actionNames);
						if (aIdx >= 0)
						{
							m_prop_b02_action.stringValue = m_actionNames[aIdx];
						}
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b03_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b03_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b03_setIndex.intValue, m_setNames);
						if (idx != m_prop_b03_setIndex.intValue) m_prop_b03_setIndex.intValue = idx;
						var aIdx = EditorGUILayout.Popup(Array.IndexOf(m_actionNames, m_prop_b03_action.stringValue), m_actionNames);
						if (aIdx >= 0)
						{
							m_prop_b03_action.stringValue = m_actionNames[aIdx];
						}
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b04_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b04_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b04_setIndex.intValue, m_setNames);
						if (idx != m_prop_b04_setIndex.intValue) m_prop_b04_setIndex.intValue = idx;
						var aIdx = EditorGUILayout.Popup(Array.IndexOf(m_actionNames, m_prop_b04_action.stringValue), m_actionNames);
						if (aIdx >= 0)
						{
							m_prop_b04_action.stringValue = m_actionNames[aIdx];
						}
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b05_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b05_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b05_setIndex.intValue, m_setNames);
						if (idx != m_prop_b05_setIndex.intValue) m_prop_b05_setIndex.intValue = idx;
						var aIdx = EditorGUILayout.Popup(Array.IndexOf(m_actionNames, m_prop_b05_action.stringValue), m_actionNames);
						if (aIdx >= 0)
						{
							m_prop_b05_action.stringValue = m_actionNames[aIdx];
						}
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b06_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b06_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b06_setIndex.intValue, m_setNames);
						if (idx != m_prop_b06_setIndex.intValue) m_prop_b06_setIndex.intValue = idx;
						var aIdx = EditorGUILayout.Popup(Array.IndexOf(m_actionNames, m_prop_b06_action.stringValue), m_actionNames);
						if (aIdx >= 0)
						{
							m_prop_b06_action.stringValue = m_actionNames[aIdx];
						}
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b07_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b07_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b07_setIndex.intValue, m_setNames);
						if (idx != m_prop_b07_setIndex.intValue) m_prop_b07_setIndex.intValue = idx;
						var aIdx = EditorGUILayout.Popup(Array.IndexOf(m_actionNames, m_prop_b07_action.stringValue), m_actionNames);
						if (aIdx >= 0)
						{
							m_prop_b07_action.stringValue = m_actionNames[aIdx];
						}
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b08_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b08_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b08_setIndex.intValue, m_setNames);
						if (idx != m_prop_b08_setIndex.intValue) m_prop_b08_setIndex.intValue = idx;
						var aIdx = EditorGUILayout.Popup(Array.IndexOf(m_actionNames, m_prop_b08_action.stringValue), m_actionNames);
						if (aIdx >= 0)
						{
							m_prop_b08_action.stringValue = m_actionNames[aIdx];
						}
					}
				}
			}

			serializedObject.ApplyModifiedProperties();

			if (GUI.changed)
			{
				m_currentTarget.InitializeRuntimeControl();
			}
		}
	}
}

#endif

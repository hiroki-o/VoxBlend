using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Xml.Schema;

namespace Vox.Blend {

	[CustomEditor(typeof(BlendShapeInteractiveController))]
	public class BlendShapeInteractiveControllerEditor : Editor {

		private BlendShapeInteractiveController m_currentTarget;
		private BlendShapeControlMapAsset m_currentAsset;
		private string[] m_setNames;
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
		private SerializedProperty m_prop_b01_inputAxis = null;
		private SerializedProperty m_prop_b02_inputAxis = null;
		private SerializedProperty m_prop_b03_inputAxis = null;
		private SerializedProperty m_prop_b04_inputAxis = null;
		private SerializedProperty m_prop_b05_inputAxis = null;
		private SerializedProperty m_prop_b06_inputAxis = null;
		private SerializedProperty m_prop_b07_inputAxis = null;
		private SerializedProperty m_prop_b08_inputAxis = null;
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
			m_prop_b01_inputAxis = serializedObject.FindProperty("m_binding01.inputAxis");
			m_prop_b02_inputAxis = serializedObject.FindProperty("m_binding02.inputAxis");
			m_prop_b03_inputAxis = serializedObject.FindProperty("m_binding03.inputAxis");
			m_prop_b04_inputAxis = serializedObject.FindProperty("m_binding04.inputAxis");
			m_prop_b05_inputAxis = serializedObject.FindProperty("m_binding05.inputAxis");
			m_prop_b06_inputAxis = serializedObject.FindProperty("m_binding06.inputAxis");
			m_prop_b07_inputAxis = serializedObject.FindProperty("m_binding07.inputAxis");
			m_prop_b08_inputAxis = serializedObject.FindProperty("m_binding08.inputAxis");
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

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			if (m_currentTarget != target)
			{
				ResetEditor();
			}

			EditorGUILayout.ObjectField(m_prop_controlMap);
			EditorGUILayout.PropertyField(m_prop_ensureMeshNameMatch, new GUIContent("Mesh Name Match"), false);

			if (InitializeSetNames())
			{
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b01_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b01_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b01_setIndex.intValue, m_setNames);
						if (idx != m_prop_b01_setIndex.intValue) m_prop_b01_setIndex.intValue = idx;
						EditorGUILayout.DelayedTextField(m_prop_b01_inputAxis, m_empty);					
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b02_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b02_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b02_setIndex.intValue, m_setNames);
						if (idx != m_prop_b02_setIndex.intValue) m_prop_b02_setIndex.intValue = idx;
						EditorGUILayout.DelayedTextField(m_prop_b02_inputAxis, m_empty);
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b03_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b03_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b03_setIndex.intValue, m_setNames);
						if (idx != m_prop_b03_setIndex.intValue) m_prop_b03_setIndex.intValue = idx;
						EditorGUILayout.DelayedTextField(m_prop_b03_inputAxis, m_empty);
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b04_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b04_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b04_setIndex.intValue, m_setNames);
						if (idx != m_prop_b04_setIndex.intValue) m_prop_b04_setIndex.intValue = idx;
						EditorGUILayout.DelayedTextField(m_prop_b04_inputAxis, m_empty);
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b05_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b05_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b05_setIndex.intValue, m_setNames);
						if (idx != m_prop_b05_setIndex.intValue) m_prop_b05_setIndex.intValue = idx;
						EditorGUILayout.DelayedTextField(m_prop_b05_inputAxis, m_empty);
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b06_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b06_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b06_setIndex.intValue, m_setNames);
						if (idx != m_prop_b06_setIndex.intValue) m_prop_b06_setIndex.intValue = idx;
						EditorGUILayout.DelayedTextField(m_prop_b06_inputAxis, m_empty);
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b07_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b07_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b07_setIndex.intValue, m_setNames);
						if (idx != m_prop_b07_setIndex.intValue) m_prop_b07_setIndex.intValue = idx;
						EditorGUILayout.DelayedTextField(m_prop_b07_inputAxis, m_empty);
					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.PropertyField(m_prop_b08_enabled, m_empty, false, GUILayout.Width(20f));
					using (new EditorGUI.DisabledScope(!m_prop_b08_enabled.boolValue))
					{
						var idx = EditorGUILayout.Popup(m_prop_b08_setIndex.intValue, m_setNames);
						if (idx != m_prop_b08_setIndex.intValue) m_prop_b08_setIndex.intValue = idx;
						EditorGUILayout.DelayedTextField(m_prop_b08_inputAxis, m_empty);
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


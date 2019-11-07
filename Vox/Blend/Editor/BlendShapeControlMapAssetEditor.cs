using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Xml.Schema;

namespace Vox.Blend
{
    [CustomEditor(typeof(BlendShapeControlMapAsset))]
    public class BlendShapeControlMapAssetEditor : Editor
    {
//		private class Styles {
//			public static readonly string kEDITBUTTON_LABEL		= "Open in Graph Editor";
//			public static readonly string kEDITBUTTON_DESCRIPTION	= "Opens graph in editor to modify the graph.";
//			public static readonly GUIContent kEDITBUTTON = new GUIContent(kEDITBUTTON_LABEL, kEDITBUTTON_DESCRIPTION);
//		}

        private BlendShapeControlMapAsset m_currentTarget;
        private SerializedProperty m_prop_targetModel;
        private string[] m_meshNames;
        private List<string[]> m_blendShapeNames;
        private List<bool> m_foldouts;

//		private class Styles
//		{
//			public EditorGUILayout.VerticalScope boxVScope = new EditorGUILayout.VerticalScope(GUI.skin.box);
//			public EditorGUILayout.HorizontalScope hScope = new EditorGUILayout.HorizontalScope();
//		}
//
//		private static Styles s_styles;

        private void ResetEditor(BlendShapeControlMapAsset asset)
        {
            m_currentTarget = asset;
            m_prop_targetModel = serializedObject.FindProperty("m_targetModel");
            
            if (m_prop_targetModel.objectReferenceValue != null)
            {
                var targetModel = m_prop_targetModel.objectReferenceValue as GameObject;
                var renderers = targetModel.GetComponentsInChildren<SkinnedMeshRenderer>();
                m_meshNames = renderers.Where(r => r.sharedMesh.blendShapeCount > 0).Select(r => r.sharedMesh.name)
                    .ToArray();
                m_blendShapeNames = new List<string[]>();
                foreach (var r in renderers.Where(r => r.sharedMesh.blendShapeCount > 0))
                {
                    var names = new List<string>();
                    for (var i = 0; i < r.sharedMesh.blendShapeCount; ++i)
                    {
                        names.Add(r.sharedMesh.GetBlendShapeName(i));
                    }

                    m_blendShapeNames.Add(names.ToArray());
                }
            }
            else
            {
                m_meshNames = null;
                m_blendShapeNames = null;
            }

            m_foldouts = new List<bool>();
            if (asset.ControlSets != null)
            {
                foreach (var s in asset.ControlSets)
                {
                    m_foldouts.Add(false);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var asset = target as BlendShapeControlMapAsset;

            if (m_currentTarget != asset)
            {
                ResetEditor(asset);
            }

            var model = asset.TargetModel;
            EditorGUILayout.PropertyField(m_prop_targetModel, new GUIContent("Target Model"), false);
            serializedObject.ApplyModifiedProperties();
            if (GUI.changed)
            {
                asset.Reset(m_prop_targetModel.objectReferenceValue as GameObject);
                ResetEditor(asset);
            }

            if (m_meshNames == null)
            {
                return;
            }

            BlendShapeControlMapAsset.BlendShapeControlSet removingSet = null;

            for (var i = 0; i < asset.ControlSets.Count; ++i)
            {
                var set = asset.ControlSets[i];
                m_foldouts[i] = EditorGUILayout.Foldout(m_foldouts[i], set.Name);

                if (m_foldouts[i])
                {
                    using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                    {
                        BlendShapeControlMapAsset.BlendShapeControl removingControl = null;

                        using (new EditorGUILayout.HorizontalScope())
                        {
                            set.Name = EditorGUILayout.DelayedTextField("Input Name", set.Name);

                            if (!EditorApplication.isPlaying && GUILayout.Button("-", GUILayout.Width(30f)))
                            {
                                removingSet = set;
                            }
                        }

                        foreach (var c in set.Controls)
                        {
                            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                            {
                                if (!EditorApplication.isPlaying)
                                {
                                    var index = Array.IndexOf(m_meshNames, c.MeshName);
                                    var newIndex = EditorGUILayout.Popup("Mesh", index, m_meshNames);
                                    if (index != newIndex && newIndex >= 0)
                                    {
                                        c.MeshName = m_meshNames[newIndex];
                                        index = newIndex;
                                        EditorUtility.SetDirty(target);
                                    }

                                    var blendIndex = Array.IndexOf(m_blendShapeNames[index], c.BlendShapeName);
                                    var newBlendIndex = EditorGUILayout.Popup("BlendShape", blendIndex,
                                        m_blendShapeNames[index]);
                                    if (blendIndex != newBlendIndex && newBlendIndex >= 0)
                                    {
                                        c.BlendShapeName = m_blendShapeNames[index][newBlendIndex];
                                        EditorUtility.SetDirty(target);
                                    }
                                }
                                else
                                {
                                    EditorGUILayout.LabelField("Mesh", c.MeshName);
                                    EditorGUILayout.LabelField("BlendShape", c.BlendShapeName);
                                }

                                var minValue = c.Range.x;
                                var maxValue = c.Range.y;
                                GUI.changed = false;
                                EditorGUILayout.MinMaxSlider("Range", ref minValue, ref maxValue, 0f, 100f);
                                if (GUI.changed)
                                {
                                    c.Range = new Vector2(minValue, maxValue);
                                    EditorUtility.SetDirty(target);
                                }

                                var newDefault = EditorGUILayout.Slider("Default Value", c.DefaultValue, c.Range.x,
                                    c.Range.y);
                                if (c.DefaultValue != newDefault)
                                {
                                    c.DefaultValue = newDefault;
                                    EditorUtility.SetDirty(target);
                                }

                                using (new EditorGUILayout.HorizontalScope())
                                {
                                    GUILayout.FlexibleSpace();
                                    if (!EditorApplication.isPlaying && GUILayout.Button("-", GUILayout.Width(30f)))
                                    {
                                        removingControl = c;
                                    }
                                }
                            }

                            GUILayout.Space(8f);
                        }

                        if (removingControl != null)
                        {
                            set.Controls.Remove(removingControl);
                            EditorUtility.SetDirty(target);
                        }

                        GUILayout.Space(12f);

                        if (!EditorApplication.isPlaying && GUILayout.Button("Add New Control"))
                        {
                            set.Controls.Add(new BlendShapeControlMapAsset.BlendShapeControl(m_meshNames[0],
                                m_blendShapeNames[0][0], new Vector2(0f, 100f)));
                            EditorUtility.SetDirty(target);
                        }
                    }
                }
            }

            GUILayout.Space(4f);

            if (removingSet != null)
            {
                m_foldouts.RemoveAt(asset.ControlSets.IndexOf(removingSet));
                asset.ControlSets.Remove(removingSet);
                EditorUtility.SetDirty(target);
            }

            GUILayout.Space(12f);

            if (!EditorApplication.isPlaying && GUILayout.Button("Add New Set"))
            {
                asset.ControlSets.Add(new BlendShapeControlMapAsset.BlendShapeControlSet("New Set"));
                m_foldouts.Add(true);
                EditorUtility.SetDirty(target);
            }
        }
    }
}
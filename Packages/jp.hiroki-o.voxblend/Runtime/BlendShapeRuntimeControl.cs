using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vox.Blend
{
    public class BlendShapeRuntimeControl
    {
        private readonly RuntimeControlSet[] m_set;

        public RuntimeControlSet[] Set => m_set;

        public struct RuntimeControlSet
        {
            public float axisValue;
            public RuntimeControl[] controls;
        }
        
        public struct RuntimeControl
        {
            public SkinnedMeshRenderer renderer;
            public int blendShapeIndex;
            public float currentValue;
            public Vector2 range;
            public float defaultValue;
        }

        public BlendShapeRuntimeControl(GameObject rootObject, BlendShapeControlMapAsset asset, bool ensureMeshNameMatch = true)
        {
            var renderers = rootObject.GetComponentsInChildren<SkinnedMeshRenderer>();

            var controlAxisList = asset.ControlSets;
            
            m_set = new RuntimeControlSet[controlAxisList.Count];

            for (var i = 0; i < m_set.Length; ++i)
            {
                var src = controlAxisList[i];
                
                m_set[i] = new RuntimeControlSet
                    {
                        axisValue = 0f,
                        controls = new RuntimeControl[src.Controls.Count]
                    };
                var controls = m_set[i].controls;

                for (var j = 0; j < controls.Length; j++)
                {
                    var r = FindMeshRenderer(renderers, src.Controls[j], ensureMeshNameMatch);
                    var index = r == null ? -1 : r.sharedMesh.GetBlendShapeIndex(src.Controls[j].BlendShapeName);
                    var curVal = 0f;
                    if (index < 0)
                    {
                        Debug.LogErrorFormat("Binding Failed: BlendShape '{0}' not found from {1}.", 
                            src.Controls[j].BlendShapeName, rootObject.name);
                    }
                    else
                    {
                        curVal = r.GetBlendShapeWeight(index);
                    }
                    
                    controls[j] = new RuntimeControl
                    {
                        renderer = r,
                        blendShapeIndex = index,
                        currentValue = curVal,
                        range = src.Controls[j].Range,
                        defaultValue = src.Controls[j].DefaultValue
                    };
                }
            }
        }

        private static SkinnedMeshRenderer FindMeshRenderer(SkinnedMeshRenderer[] renderers, BlendShapeControlMapAsset.BlendShapeControl c, bool ensureMeshNameMatch)
        {
            // find name matching mesh first
            foreach (var r in renderers)
            {
                if(r.sharedMesh.name != c.MeshName) continue;
                if (r.sharedMesh.GetBlendShapeIndex(c.BlendShapeName) != -1)
                {
                    return r;
                }
            }

            return !ensureMeshNameMatch ? 
                renderers.FirstOrDefault(r => r.sharedMesh.GetBlendShapeIndex(c.BlendShapeName) != -1) : 
                null;
        }


        public void ApplyBlendShapeControlWeights()
        {
            foreach (var a in m_set)
            {
                var control = a.controls;
                for (var j = 0; j < control.Length; ++j)
                {
                    if (control[j].blendShapeIndex < 0) continue;

                    var value = a.axisValue <= 0f ? 
                        // axis : -1.0f ~ 0.0f
                        Mathf.Lerp(control[j].range.x, control[j].defaultValue, a.axisValue * -1.0f) : 
                        // axis : 0.0f ~ 1.0f
                        Mathf.Lerp(control[j].defaultValue, control[j].range.y, a.axisValue);
                    
                    control[j].currentValue = value;
                    control[j].renderer.SetBlendShapeWeight(control[j].blendShapeIndex, control[j].currentValue);
                }
            }
        }
    }
}

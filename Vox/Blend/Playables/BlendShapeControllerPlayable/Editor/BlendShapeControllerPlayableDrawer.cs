using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BlendShapeControllerPlayableBehaviour))]
public class BlendShapeControllerPlayableDrawer : PropertyDrawer
{
    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 8;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        var bindValue01Prop = property.FindPropertyRelative("bindValue01");
        var bindValue02Prop = property.FindPropertyRelative("bindValue02");
        var bindValue03Prop = property.FindPropertyRelative("bindValue03");
        var bindValue04Prop = property.FindPropertyRelative("bindValue04");
        var bindValue05Prop = property.FindPropertyRelative("bindValue05");
        var bindValue06Prop = property.FindPropertyRelative("bindValue06");
        var bindValue07Prop = property.FindPropertyRelative("bindValue07");
        var bindValue08Prop = property.FindPropertyRelative("bindValue08");

        var singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, bindValue01Prop);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, bindValue02Prop);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, bindValue03Prop);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, bindValue04Prop);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, bindValue05Prop);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, bindValue06Prop);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, bindValue07Prop);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, bindValue08Prop);
    }
}

using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyDrawers
{
    [PropertyDrawer(typeof(ResizableTextAreaAttribute))]
    public class ResizableTextAreaPropertyDrawer : PropertyDrawer
    {
        public override void DrawProperty(SerializedProperty property)
        {
            EditorDrawUtility.DrawHeader(property);

            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUILayout.LabelField(property.displayName);

                EditorGUI.BeginChangeCheck();

                string textAreaValue = EditorGUILayout.TextArea(property.stringValue, GUILayout.MinHeight(EditorGUIUtility.singleLineHeight * 3f));

                if (EditorGUI.EndChangeCheck())
                {
                    property.stringValue = textAreaValue;
                }
            }
            else
            {
                string warning = PropertyUtility.GetAttribute<ResizableTextAreaAttribute>(property).GetType().Name + " can only be used on string fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, context: PropertyUtility.GetTargetObject(property));

                EditorDrawUtility.DrawPropertyField(property);
            }
        }
    }
}

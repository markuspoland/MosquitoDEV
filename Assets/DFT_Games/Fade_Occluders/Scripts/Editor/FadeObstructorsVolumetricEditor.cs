/*********************************************************************
 * This code is written by and belongs to DFT Games Ltd.
 * *******************************************************************
 * The license governing this asset id the Unity Asset Store EULA,
 * found here: http://unity3d.com/es/legal/as_terms hereby summarised:
 * *******************************************************************
 * Usage is granted to the licensee in conjunction with the
 * package that has been licensed and also in other final products
 * produced by the licensee and aimed to an end user.
 * It's forbidden to use this code as part of packages or assets
 * aimed to be used by developers other than the licensee
 * of the original package.
 * *******************************************************************
 *
 * Copyright 2010-2017 - DFT Games Ltd. - Version 3.0.2 (16 Mar. 2017)
 *
 * *******************************************************************
 */

using UnityEditor;
using UnityEngine;

namespace DFTGames.Tools.EditorTools
{
    [CustomEditor(typeof(FadeObstructorsVolumetric))]
    public class FadeObstructorsVolumetricEditor : Editor
    {
        private FadeObstructorsVolumetric fader;
        private bool isDirty;
        private static Color backColor = Color.cyan;
        private static Color labelColor = Color.yellow;
        private Color backColorOriginal = Color.green;
        private Color labelColorOriginal = Color.green;

        public override void OnInspectorGUI()
        {
            EditorGUI.indentLevel = 0;
            serializedObject.Update();
            fader = (FadeObstructorsVolumetric)target;
            Commons.DrawTexture(ResourceHelper.LogoFadeObstructorsVolumetric);
            backColorOriginal = GUI.backgroundColor;
            labelColorOriginal = GUI.contentColor;
            EditorGUILayout.Separator();
            Commons.SetColors(backColor, labelColor);

            EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);
            Material tmpMat = EditorGUILayout.ObjectField(new GUIContent("Transparent material", "The transparent material to use for the fading"), fader.transparentMaterial, typeof(Material), false) as Material;
            if (tmpMat != fader.transparentMaterial)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Transparent material");
                fader.transparentMaterial = tmpMat;
            }
            // Replicate texture
            bool tmpBool = EditorGUILayout.Toggle(new GUIContent("Replicate texture", "Do we replicate the original texture on the transparent material?"), fader.replicateTexture);
            if (tmpBool != fader.replicateTexture)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Replicate texture");
                fader.replicateTexture = tmpBool;
            }
            // Fading time
            float tmpFloat = EditorGUILayout.Slider(new GUIContent("Fading out time", "The time for fading out the material"), fader.fadingTime, 0.05f, 2f);
            if (tmpFloat != fader.fadingTime)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Fading out time");
                fader.fadingTime = tmpFloat;
            }
            // Fading amount
            tmpFloat = EditorGUILayout.Slider(new GUIContent("Fading amount", "The fading amount"), fader.transparenceValue, 0f, 1f);
            if (tmpFloat != fader.transparenceValue)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Fading amount");
                fader.transparenceValue = tmpFloat;
            }
            tmpBool = EditorGUILayout.Toggle(new GUIContent("Ignore Triggers", "Do not take into account any trigger"), fader.ignoreTriggers);
            if (tmpBool != fader.ignoreTriggers)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Ignore triggers");
                fader.ignoreTriggers = tmpBool;
            }
            Commons.SetColors(backColorOriginal, labelColor);
            Color tmpColor = EditorGUILayout.ColorField(new GUIContent("Fade Color", "Color shade and alpha to use during the fading"), fader.fadingColorToUse);
            if (tmpColor != fader.fadingColorToUse)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Fading Color");
                fader.fadingColorToUse = tmpColor;
            }
            Commons.SetColors(backColor, labelColor);
            LayerMask tmpMask = Commons.LayerMaskField(new GUIContent("Layers to fade", "The layers we want to fade. IF YOU USE MULTIPLE INSTANCES OF THIS SCRIPT ON THE SASME CAMERA MAKE SURE THAT EACH INSTANCE MANAGES ONLY ITS OWN LAYERS!"), fader.layersToFade);
            if (tmpMask != fader.layersToFade)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Layers to fade");
                fader.layersToFade = tmpMask;
            }
            SerializedProperty playerT = serializedObject.FindProperty("playerTransform");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(playerT, new GUIContent("Player Transform", "The Transform of the Player"), true);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Player Transform");
                serializedObject.ApplyModifiedProperties();
            }
            tmpFloat = EditorGUILayout.Slider(new GUIContent("Volume offset", "The distance off the target to avoid fading too much"), fader.offset, -2f, 0f);
            if (tmpFloat != fader.offset)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Volume offset");
                fader.offset = tmpFloat;
            }
            string tmpString = EditorGUILayout.TagField(new GUIContent("Player Tag", "Select the player's tag"), fader.playerTag);
            if (tmpString != fader.playerTag)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Player Tag");
                fader.playerTag = tmpString;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);
            EditorGUILayout.Separator();
            tmpBool = EditorGUILayout.Toggle(new GUIContent("Common Volume", "Use the same volume for all instances of this script on this camera"), FadeObstructorsVolumetric.commonVolume);
            if (tmpBool != fader._commonVolume)
            {
                isDirty = true;
                Undo.RecordObject(fader, "Common Volume");
                fader._commonVolume = tmpBool;
                FadeObstructorsVolumetric.commonVolume = tmpBool;
            }
            EditorGUILayout.Separator();
            if (FadeObstructorsVolumetric.commonVolume)
            {
                tmpFloat = EditorGUILayout.Slider(new GUIContent("Common radius", "The radius of the unique capsule used as volume casting for all the instances if Common Volume is active. This should be fairly small"), FadeObstructorsVolumetric.commonRadius, 0.01f, 1f);
                if (tmpFloat != fader._commonRadius)
                {
                    isDirty = true;
                    Undo.RecordObject(fader, "Raycast offset");
                    fader._commonRadius = tmpFloat;
                    FadeObstructorsVolumetric.commonRadius = tmpFloat;
                }
                EditorGUILayout.Separator();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            if (!FadeObstructorsVolumetric.commonVolume)
            {
                EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);
                EditorGUILayout.Separator();
                tmpFloat = EditorGUILayout.Slider(new GUIContent("Volume radius", "The radius of the capsule used as volume casting. This should be fairly small"), fader.capsuleRadius, 0.01f, 1f);
                if (tmpFloat != fader.capsuleRadius)
                {
                    isDirty = true;
                    Undo.RecordObject(fader, "Raycast offset");
                    fader.capsuleRadius = tmpFloat;
                }
                EditorGUILayout.Separator();
                EditorGUILayout.EndVertical();
            }
            if (GUI.changed || isDirty)
            {
                EditorUtility.SetDirty(target); // or it won't save the data!!
            }
        }
    }
}
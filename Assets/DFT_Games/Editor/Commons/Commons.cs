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

using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace DFTGames.Tools.EditorTools
{
    public static class Commons
    {
        private static List<int> layerNumbers = new List<int>();

        // LayerFieldMask found here
        // http://answers.unity3d.com/questions/42996/how-to-create-layermask-field-in-a-custom-editorwi.html
        // Thanks vexe!
        public static LayerMask LayerMaskField(GUIContent label, LayerMask layerMask)
        {
            string[] layers = InternalEditorUtility.layers;
            layerNumbers.Clear();

            for (int i = 0; i < layers.Length; i++)
                layerNumbers.Add(LayerMask.NameToLayer(layers[i]));

            int maskWithoutEmpty = 0;
            for (int i = 0; i < layerNumbers.Count; i++)
            {
                if (((1 << layerNumbers[i]) & layerMask.value) > 0)
                    maskWithoutEmpty |= (1 << i);
            }

            maskWithoutEmpty = UnityEditor.EditorGUILayout.MaskField(label, maskWithoutEmpty, layers);

            int mask = 0;
            for (int i = 0; i < layerNumbers.Count; i++)
            {
                if ((maskWithoutEmpty & (1 << i)) > 0)
                    mask |= (1 << layerNumbers[i]);
            }
            layerMask.value = mask;

            return layerMask;
        }

        public static void SetColors(Color back, Color label)
        {
            GUI.backgroundColor = back;
            GUI.contentColor = label;
        }

        public static void DrawTexture(Texture texture)
        {
            if (texture == null)
            {
                Debug.Log("The texture is missing");
                return;
            }

            Rect rect = GUILayoutUtility.GetRect(0f, 0f);
            rect.width = texture.width;
            rect.height = texture.height;
            GUILayout.Space(rect.height);
            GUI.DrawTexture(rect, texture);
        }
    }
}
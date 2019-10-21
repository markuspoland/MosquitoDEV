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
using UnityEngine;

namespace DFTGames.Tools
{
    [AddComponentMenu("Camera/Fade obstructors by raycast"),
     RequireComponent(typeof(Camera))]
    public class FadeObstructors : FadeObstructorsBaseClass
    {
        public bool useSpherecast = false;
        public float spherecastRadius = 0.5f;

        private RaycastHit[] hit;

        // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
        private void FixedUpdate()
        {
            if (playerTransform == null) // Do nothing if we have no target
                return;
            // Let's retrieve all the objects in the way of the camera
#if  UNITY_5_0 || UNITY_5_1
            if (!useSpherecast)
            {
                hit = Physics.RaycastAll(myTransform.position, myTransform.forward, (playerTransform.position - myTransform.position).magnitude + offset, layersToFade);
            }
            else
            {
                hit = Physics.SphereCastAll(myTransform.position, spherecastRadius, myTransform.forward, (playerTransform.position - myTransform.position).magnitude + offset, layersToFade);
            }
#else
            if (!useSpherecast)
            {
                hit = Physics.RaycastAll(myTransform.position, myTransform.forward, (playerTransform.position - myTransform.position).magnitude + offset, layersToFade, ignoreTriggers ? QueryTriggerInteraction.Ignore : QueryTriggerInteraction.Collide);
            }
            else
            {
                hit = Physics.SphereCastAll(myTransform.position, spherecastRadius, myTransform.forward, (playerTransform.position - myTransform.position).magnitude + offset, layersToFade, ignoreTriggers ? QueryTriggerInteraction.Ignore : QueryTriggerInteraction.Collide);
            }
#endif
            Debug.DrawLine(myTransform.position, playerTransform.position + (myTransform.forward * offset), fadingColorToUse, Time.fixedDeltaTime);
            List<int> renderersIdsHitInThisFrame = new List<int>();
            if (hit != null)
            {
                // Parse all objects we hit
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider.isTrigger && ignoreTriggers)
                        continue;
                    // Ignore the player :)
                    if (!hit[i].collider.CompareTag(playerTag))
                    {
                        // Retrieve all the renderers
                        Renderer[] rendererWeHit = hit[i].collider.gameObject.GetComponentsInChildren<Renderer>();
                        // Loop through the renderers
                        for (int idx = 0; idx < rendererWeHit.Length; idx++)
                        {
                            if (rendererWeHit[idx] != null) // just to be on the safe side :)
                            {
                                // Store the render's unique Id among those hit during the current frame
                                renderersIdsHitInThisFrame.Add(rendererWeHit[idx].GetInstanceID());
                                // If we changed this already we skip it, otherwise we proceed with
                                // the change
                                if (!modifiedShaders.ContainsKey(rendererWeHit[idx].GetInstanceID()))
                                {
                                    ShaderData shaderData = new ShaderData();
                                    FadingManager fade = rendererWeHit[idx].gameObject.GetComponent<FadingManager>();
                                    if (fade != null)
                                        fade.GoAway();
                                    shaderData.renderer = rendererWeHit[idx];
                                    shaderData.materials = rendererWeHit[idx].materials;
                                    Material[] tmpMats = rendererWeHit[idx].materials;
                                    shaderData.color = new Color[rendererWeHit[idx].materials.Length];
                                    for (int j = 0; j < tmpMats.Length; j++)
                                    {
                                        shaderData.color[j] = tmpMats[j].color;
                                        tmpMats[j] = transparentMaterial;
                                        tmpMats[j].color = fadingColorToUse;
                                        if (replicateTexture)
                                            tmpMats[j].mainTexture = rendererWeHit[idx].materials[j].mainTexture;
                                        else
                                            tmpMats[j].mainTexture = null;
                                    }
                                    rendererWeHit[idx].materials = tmpMats;
                                    // Add the shader to the list of those that have been changed
                                    modifiedShaders.Add(rendererWeHit[idx].GetInstanceID(), shaderData);
                                    fade = rendererWeHit[idx].gameObject.AddComponent<FadingManager>();
                                    fade.fadingTime = fadingTime;
                                    fade.fadingAmount = transparenceValue;
                                }
                            }
                        }
                    }
                }
            }
            // Now let's restore those shaders that we changed but now they are no longer in the way
            List<int> renderersToRestore = new List<int>();
            foreach (KeyValuePair<int, ShaderData> elemento in modifiedShaders)
            {
                if (!renderersIdsHitInThisFrame.Contains(elemento.Key))
                    renderersToRestore.Add(elemento.Key);
            }
            for (int i = 0; i < renderersToRestore.Count; i++)
            {
                ShaderData sd = modifiedShaders[renderersToRestore[i]];
                modifiedShaders.Remove(renderersToRestore[i]);
                for (int m = 0; m < sd.materials.Length; m++)
                {
                    FadingManager fade = sd.renderer.gameObject.GetComponent<FadingManager>();
                    if (fade != null)
                        fade.GoAway();
                    fade = sd.renderer.gameObject.AddComponent<FadingManager>();
                    fade.fadingTime = fadingTime;
                    fade.fadingAmount = transparenceValue;
                    fade.fadeOut = false;
                    fade.matIdx = m;
                    fade.oldMat = sd.materials[m];
                    fade.oldColor = sd.color[m];
                }
            }
            Resources.UnloadUnusedAssets();
        }
    }
}
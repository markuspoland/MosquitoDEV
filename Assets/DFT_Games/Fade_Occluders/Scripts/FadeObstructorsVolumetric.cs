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
    [AddComponentMenu("Camera/Fade obstructors by volume"),
     RequireComponent(typeof(Camera)), RequireComponent(typeof(Rigidbody))]
    public class FadeObstructorsVolumetric : FadeObstructorsBaseClass
    {
        public static bool commonVolume = true;
        public static float commonRadius = 0.05f;
        public float capsuleRadius = 0.05f;
        public float _commonRadius = 0.05f;
        public bool _commonVolume = true;

        private CapsuleCollider capsuleVolume;
        private bool createVolume = false;

        public override void Start()
        {
            base.Start();
            commonVolume = _commonVolume;
            commonRadius = _commonRadius;
            createVolume = (!commonVolume || GetComponent<CapsuleCollider>() == null);
            if (createVolume)
            {
                capsuleVolume = gameObject.AddComponent<CapsuleCollider>();
                capsuleVolume.direction = 2; // Forward
                capsuleVolume.isTrigger = true;
                capsuleVolume.radius = commonVolume ? _commonRadius : capsuleRadius;
            }
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }

        // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
        public void FixedUpdate()
        {
            if (playerTransform == null) // Do nothing if we have no target
                return;
            if (createVolume)
            {
                capsuleVolume.height = ((playerTransform.position) - myTransform.position).magnitude + offset;
                capsuleVolume.center = new Vector3(0f, 0f, capsuleVolume.height * 0.5f);
            }
        }

        // OnTriggerEnter is called when the Collider other enters the trigger
        public void OnTriggerEnter(Collider other)
        {
            int objLayer = 1 << other.gameObject.layer;
            if (other.isTrigger && ignoreTriggers || other.CompareTag(playerTag) || (layersToFade & objLayer) != objLayer)
                return;
            // Retrieve all the renderers
            Renderer[] rendererWeHit = other.gameObject.GetComponentsInChildren<Renderer>();
            // Loop through the renderers
            for (int idx = 0; idx < rendererWeHit.Length; idx++)
            {
                if (rendererWeHit[idx] != null) // just to be on the safe side :)
                {
                    // If we changed this already we skip it, otherwise we proceed with the change
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
                            tmpMats[j] = new Material(transparentMaterial);
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

        // OnTriggerExit is called when the Collider other has stopped touching the trigger
        public void OnTriggerExit(Collider other)
        {
            // Retrieve all the renderers
            Renderer[] rendererWeHit = other.gameObject.GetComponentsInChildren<Renderer>();
            List<int> renderersToRestore = new List<int>();
            for (int idx = 0; idx < rendererWeHit.Length; idx++)
            {
                int key = rendererWeHit[idx].GetInstanceID();
                if (modifiedShaders.ContainsKey(key))
                    renderersToRestore.Add(key);
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
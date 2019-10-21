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

using UnityEngine;
using System.Collections;

namespace DFTGames.Tools
{
    public class FadingManager : MonoBehaviour
    {
        public float fadingTime;
        public float fadingAmount;
        public bool fadeOut = true;
        public Material oldMat;
        public int matIdx;
        public Color oldColor;

        private Renderer myRenderer;
        private Color tmpColor;
        private float delta;
        private bool done = false;
        private Material[] mats;

        void Start()
        {
            myRenderer = GetComponent<Renderer>();
            if (myRenderer == null)
            {
                Debug.LogError("Can't get my renderer! Object: " + gameObject.name);
                Destroy(this);
            }
            mats = myRenderer.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                tmpColor = mats[i].color;
                tmpColor.a = fadeOut ? 1f : fadingAmount;
                mats[i].color = tmpColor;
            }
            myRenderer.materials = mats;
            delta = (1 - fadingAmount) / fadingTime;
        }

        public void GoAway()
        {
            for (int i = 0; i < mats.Length; i++)
            {
                tmpColor = mats[i].color;
                if (fadeOut)
                {
                    tmpColor.a = fadingAmount;
                    mats[i].color = tmpColor;
                }
                else
                {
                    if (i == matIdx)
                    {
                        mats[i] = oldMat;
                        mats[i].color = oldColor;
                    }
                }
            }
            myRenderer.materials = mats;
            done = true;
            Resources.UnloadUnusedAssets();
            Destroy(this);
        }

        private void Update()
        {
            if (done)
            {
                Resources.UnloadUnusedAssets();
                Destroy(this);
            }

            mats = myRenderer.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                tmpColor = mats[i].color;
                if (fadeOut)
                    FadeOut(i);
                else
                    FadeIn(i);
            }
            myRenderer.materials = mats;
        }

        private void FadeOut(int i)
        {
            if (tmpColor.a < fadingAmount)
            {
                tmpColor.a = fadingAmount;
                mats[i].color = tmpColor;
                done = true;
                return;
            }
            tmpColor.a -= delta * Time.deltaTime;
            mats[i].color = tmpColor;
        }
        private void FadeIn(int i)
        {
            if (i != matIdx) return;
            if (tmpColor.a >= 1f)
            {
                mats[i] = oldMat;
                mats[i].color = oldColor;
                done = true;
                return;
            }
            tmpColor.a += delta * Time.deltaTime;
            mats[i].color = tmpColor;
        }
    }
}
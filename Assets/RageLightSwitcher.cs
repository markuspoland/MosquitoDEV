using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageLightSwitcher : MonoBehaviour
{
    RageLight[] rageLights;

    void Start()
    {
        rageLights = FindObjectsOfType<RageLight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RagdollToggle.ragdollEnabled)
        {
            foreach (RageLight script in rageLights)
            {
                script.enabled = false;
            }
        } else
        {
            foreach (RageLight script in rageLights)
            {
                script.enabled = true;
            }
        }
        if (RagdollToggle1.ragdollEnabled)
        {
            foreach (RageLight script in rageLights)
            {
                script.enabled = false;
            }
        }
        else
        {
            foreach (RageLight script in rageLights)
            {
                script.enabled = true;
            }
        }
        if (RagdollToggle2.ragdollEnabled)
        {
            foreach (RageLight script in rageLights)
            {
                script.enabled = false;
            }
        }
        else
        {
            foreach (RageLight script in rageLights)
            {
                script.enabled = true;
            }
        }
    }
}

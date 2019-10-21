using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Assign the prefab in the inspector
    public GameObject PlayerPrefab;
    public GameObject PlayerPrefab1;
    public GameObject PlayerPrefab2;
   

    //Singleton
    private static PlayerPrefabManager m_Instance = null;
    public static PlayerPrefabManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (PlayerPrefabManager)FindObjectOfType(typeof(PlayerPrefabManager));
            }
            return m_Instance;
        }
    }
}

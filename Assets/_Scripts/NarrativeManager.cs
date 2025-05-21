using UnityEngine;
using System.Collections.Generic;

public class NarrativeManager : MonoBehaviour
{
    public static NarrativeManager instance{ get; private set; }

    Dictionary<string, bool> pearls = new Dictionary<string, bool>{
        {"Cofre", false},
        {"Estatua", false},
        {"Runas", false},
        {"Mago", false},
    };

   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    public void SetPearl(string pearlName, bool value)
    {
        if (pearls.ContainsKey(pearlName))
        {
            pearls[pearlName] = value;
        }
        else
        {
            Debug.LogWarning("Pearl not found: " + pearlName);
        }
    }

    public bool GetPearl(string pearlName)
    {
        if (pearls.ContainsKey(pearlName))
        {
            return pearls[pearlName];
        }
        else
        {
            Debug.LogWarning("Pearl not found: " + pearlName);
            return false;
        }
    }
    //Singleton

    //GetKey

    //SetKey
    
}

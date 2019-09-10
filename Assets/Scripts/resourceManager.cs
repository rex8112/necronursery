using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "ScriptableObjects/SpawnResourceManager", order = 1)]
public class resourceManager : ScriptableObject
{
    public List<Resource> resources;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    public class Resource
    {
        public string name;
        public int value;
        public int teethValue;

        public bool Add(int change)
        {
            value += change;
            return true;
        }

        public bool Remove(int change)
        {
            if (value - change >= 0)
            {
                value -= change;
                return true;
            }
            else
                return false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static Dictionary<int, Entity> entityList = new Dictionary<int, Entity>();
    public int entityID;

    private void Awake()
    {
        int id = UnityEngine.Random.Range(1, int.MaxValue);
        while (entityList.ContainsKey(id))
            id = UnityEngine.Random.Range(1, int.MaxValue);
        entityList.Add(id, this);
        entityID = id;
    }
}
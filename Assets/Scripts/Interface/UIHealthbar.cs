using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthbar : MonoBehaviour
{
    public Transform woolHolder;
    public GameObject woolPrefab;

    public float changeTime = 0.5f;

    public List<Transform> woolPoints;

    private void Start()
    {
        woolPoints = new List<Transform>();
        GameEvents.current.onPlayerHealthUpdate += UpdateHealth;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerHealthUpdate -= UpdateHealth;
    }

    void UpdateHealth(int newHealth)
    {
        int difference = newHealth - woolPoints.Count;
        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                if (i == 0)
                    AddWoolPoint();
                else
                    Invoke("AddWoolPoint", (changeTime + changeTime / (Mathf.Abs(difference) - 1)) / Mathf.Abs(difference) * i);
            }
        } else if (difference < 0)
        {
            for (int i = 0; i < Mathf.Abs(difference); i++)
            {
                if (i == 0)
                    RemoveWoolPoint();
                else
                    Invoke("RemoveWoolPoint", (changeTime + changeTime / (Mathf.Abs(difference) - 1)) / Mathf.Abs(difference) * i);
            }
        }
    }


    void RemoveWoolPoint()
    {
        Transform temp = woolPoints[woolPoints.Count - 1];
        woolPoints.RemoveAt(woolPoints.Count - 1);
        Destroy(temp.gameObject);
    }


    void AddWoolPoint()
    {
        Transform newWool = Instantiate(woolPrefab).transform;
        newWool.SetParent(woolHolder);
        woolPoints.Add(newWool);
    }
}

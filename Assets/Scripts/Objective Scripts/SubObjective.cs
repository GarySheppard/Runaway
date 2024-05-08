using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SubObjective", menuName = "Objective/Create New SubObjective")]
public class SubObjective : ScriptableObject
{
    public string description;
    public bool isComplete;
    public ObjectiveType objectiveType;
    public GameObject objectivePrefab;
    public int currValue;
    public int maxValue;
    public Vector3 location;

    public enum ObjectiveType
    {
        Get,
        Find,
        Search,
        Save,
        Complete
    }

    public void CheckStatus()
    {
        if (currValue >= maxValue)
        {
            isComplete = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective", menuName = "Objective/Create New Objective")]
public class Objective: ScriptableObject
{
    public string description;
    public bool isComplete;
    public SubObjective[] subObjectives;

    public void CheckStatus()
    {
        bool allSubsComplete = true;
        foreach (SubObjective sub in subObjectives)
        {
            sub.CheckStatus();
            if (!sub.isComplete)
            {
                allSubsComplete = false;
            }
        }

        if (allSubsComplete)
        {
            isComplete = true;
        }
    }
}

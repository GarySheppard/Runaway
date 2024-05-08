using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectiveComplete
{
    public ObjectiveType objectiveType;

    public bool IsReached() 
    {
        return false;
    }
}

public enum ObjectiveType
{
    GatherSupplies,
    RescueSurvivors,
    RepairGenerator,
    EstablishComms,
    ResearchCure,
    SignalForRescue
}

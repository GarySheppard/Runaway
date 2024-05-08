using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    public PrefabSpawner spawner;

    public GameObject completionScreen;

    public List<Objective> objectives;
    //public Player player;

    public GameObject objectiveWindow;
    public TextMeshProUGUI mainObjText;
    public TextMeshProUGUI subText1;
    public TextMeshProUGUI subText2;
    public TextMeshProUGUI subText3;

    public void Awake()
    {
        Instance = this;
        objectiveWindow.SetActive(true);
    }

    private void Start()
    {
        completionScreen.SetActive(false);
        InitializeObjectives();
        ProcessObjectives();
    }

    private void Update()
    {
        UpdateObjectiveDisplay();
        CheckStatus();
    }

    private void InitializeObjectives()
    {
        
        foreach (Objective obj in objectives)
        {
            foreach (SubObjective sub in obj.subObjectives)
            {
                sub.isComplete = false;
                sub.currValue = 0;
                if ((sub.objectiveType == SubObjective.ObjectiveType.Get)
                    || (sub.objectiveType == SubObjective.ObjectiveType.Find)
                    || (sub.objectiveType == SubObjective.ObjectiveType.Save))
                {
                    sub.maxValue = 1;
                }
            }
            obj.isComplete = false;
        }
        
    }
    
    public void UpdateObjectiveDisplay()
    {
        if (objectives[0] != null)
        {
            if (objectives[0].isComplete)
            {
                objectives.RemoveAt(0);
                ProcessObjectives();
                return;
            }

            mainObjText.text = objectives[0].description;

            subText1.text = objectives[0].subObjectives[0].description;
            if (objectives[0].subObjectives[0].isComplete)
            {
                subText1.fontStyle = FontStyles.Strikethrough;
            }
            else
            {
                subText1.fontStyle = FontStyles.Normal;
            }

            if (objectives[0].subObjectives.Length >= 2)
            {
                subText2.text = objectives[0].subObjectives[1].description;
                if (objectives[0].subObjectives[1].isComplete)
                {
                    subText2.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    subText2.fontStyle = FontStyles.Normal;
                }
            }
            else
            {
                subText2.text = "";
            }

            if (objectives[0].subObjectives.Length >= 3)
            {
                subText3.text = objectives[0].subObjectives[2].description;
                if (objectives[0].subObjectives[2].isComplete)
                {
                    subText3.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    subText3.fontStyle = FontStyles.Normal;
                }
            }
            else
            {
                subText3.text = "";
            }
        }
    }

    private void CheckStatus()
    {
        foreach (Objective obj in objectives)
        {
            obj.CheckStatus();
            if (!obj.isComplete)
            {
                return;
            }
        }
        completionScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ProcessObjectives()
    {
        foreach (SubObjective sub in objectives[0].subObjectives)
        {
            Vector3 objectiveLocation = Level.GetRandomLocation(3.0f);
            sub.location = objectiveLocation;

            Vector3 prefabLocation;
            switch (sub.objectiveType)
            {
                /* Get Objective: Spawns objective prefab at the objective location and spawns zombies near it */
                case SubObjective.ObjectiveType.Get:
                    spawner.SpawnObject(sub.objectivePrefab, objectiveLocation);
                    spawner.SpawnEnemiesNearby(objectiveLocation, 20.0f, 5);
                    break;
                /* Find Objective: Spawns objective prefab at a random location nearby the objective location and spawns zombies in the area */
                case SubObjective.ObjectiveType.Find:
                    prefabLocation = Level.GetRandomNearbyLocation(objectiveLocation, 25.0f);
                    prefabLocation = new Vector3(prefabLocation.x, prefabLocation.y + 1.0f, prefabLocation.z);
                    spawner.SpawnObject(sub.objectivePrefab, prefabLocation);
                    spawner.SpawnEnemiesNearby(objectiveLocation, 40.0f, 4);
                    break;
                /* Search Objective: Spawns [maxValue] copies of the objective prefab at the objective location and spawns zombies in the area */
                case SubObjective.ObjectiveType.Search:
                    for (int i = 0; i < sub.maxValue; i++)
                    {
                        prefabLocation = Level.GetRandomNearbyLocation(objectiveLocation, 15.0f);
                        prefabLocation = new Vector3(prefabLocation.x, prefabLocation.y + 1.0f, prefabLocation.z);
                        spawner.SpawnObject(sub.objectivePrefab, prefabLocation);
                    }
                    spawner.SpawnEnemiesNearby(objectiveLocation, 40.0f, 4);
                    break;
                /* Save Objective: Spawns objective prefab at the objective location and spawns zombies in the area */
                case SubObjective.ObjectiveType.Save:
                    spawner.SpawnObject(sub.objectivePrefab, objectiveLocation);
                    spawner.SpawnEnemiesNearby(objectiveLocation, 15.0f, 6);
                    break;
                /* Complete Objective: Spawns objective prefab at the objective location and spawns zombies in the area */
                case SubObjective.ObjectiveType.Complete:
                    if (sub.objectivePrefab.CompareTag("Generator"))
                    {
                        spawner.SpawnObject(sub.objectivePrefab, objectiveLocation, Quaternion.Euler(-90f, 0f, 0f));
                    }
                    else
                    {
                        spawner.SpawnObject(sub.objectivePrefab, objectiveLocation);
                    }
                    spawner.SpawnEnemiesNearby(objectiveLocation, 50.0f, 5);
                    break;
            }
        }
    }

    //will need to add a reference of the objective onto the player

}
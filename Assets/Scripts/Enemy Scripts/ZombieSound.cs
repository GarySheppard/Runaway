using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ZombieSound : MonoBehaviour
{
     
    enum FSMaterial
    {
        Rubble, Wood, Empty
    }
    private Animator playerAnimator;
    float minTimeBetweenSounds = 10.0f;
    float maxTimeBetweenSounds = 40.0f;

    private void Start()
    {
        //StartCoroutine(ZombieGrunt());
    }
    
    public IEnumerator ZombieGrunt()
    {
        AudioManager.instance.PlaySound("zombie_grunt_"+ Random.Range(1,4), true);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds));

            // Play the sound
            AudioManager.instance.PlaySound("zombie_grunt_"+ Random.Range(1,4), true);
        }
    }

    // Update is called once per frame
    void ZombieAttack()
    {

    }

    void ZombieHurt()
    {
        AudioManager.instance.PlaySound("zombie_hurt_" + Random.Range(1,3));
    }
    private FSMaterial SurfaceSelect()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();

            if(surfaceRenderer)
            {
                surfaceMaterial = surfaceRenderer ? surfaceRenderer.sharedMaterial : null;
                if (surfaceMaterial.name.Contains("Wood") || surfaceMaterial.name.Contains("wood"))
                {
                    return FSMaterial.Wood;
                }
                else if (surfaceMaterial.name.Contains("Rubble") || surfaceMaterial.name.Contains("rubble"))
                {
                    return FSMaterial.Rubble;
                }
                else
                {
                    return FSMaterial.Empty;
                }

            }
        }
        return FSMaterial.Empty;
    }
    void ZombieFootstep()
    {
        FSMaterial surface = SurfaceSelect();
        string name = "";
        switch (surface)
        {
            case FSMaterial.Rubble:
                name = "walking_rubble_" + Random.Range(1,3);
                break;
            case FSMaterial.Wood:
                name = "crouching_wood_" + Random.Range(1,5);                
                break;
            default:
                //name = "walking_rubble_1";
                break;
        }
        //UnityEngine.Debug.Log("walking on "+ surface);

        if (surface != FSMaterial.Empty)
        {
            AudioManager.instance.PlaySound(name, true);
        }
        
    }

    


        
}

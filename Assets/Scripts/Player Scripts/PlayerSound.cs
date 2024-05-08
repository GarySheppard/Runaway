using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    /*
    [Header("Weapon")]
    public AudioClip[] swingSounds;

    [Header("Footsteps")]
    public List<AudioClip> gravelFS;
    public List<AudioClip> woodFS;
    */
    enum FSMaterial
    {
        Rubble, Wood, Empty
    }
    private Animator playerAnimator;
    private PlayerController playerController;
    /*
    private AudioSource weaponSource, footstepSource;
    // Start is called before the first frame update
    void Start()
    {
        weaponSource = GetComponent<AudioSource>();

    }
    */

    // Update is called once per frame
    void PlayerEmptySwing()
    {
        AudioManager.instance.PlaySound("melee_punch");
        //UnityEngine.Debug.Log("Swing!");

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
    void PlayerFootstep()
    {
        FSMaterial surface = SurfaceSelect();
        string name = "";
        switch (surface)
        {
            case FSMaterial.Rubble:
                name = "walking_rubble_" + Random.Range(1,3);
                break;
            case FSMaterial.Wood:
                playerAnimator = GetComponent<Animator>();
                UnityEngine.Debug.Log(playerAnimator.GetBool("isCrouching"));
                if (playerAnimator.GetBool("isCrouching"))
                {
                    name = "crouching_wood_" + Random.Range(1,5);
                }
                else
                {
                    name = "walking_wood_" + Random.Range(1,4);
                }
                
                break;
            default:
                //name = "walking_rubble_1";
                break;
        }
        UnityEngine.Debug.Log(name);

        if (surface != FSMaterial.Empty)
        {
            AudioManager.instance.PlaySound(name, true);
        }
        
    }

    void PlayerCaltropsThrowing()
    {
        playerController = GetComponent<PlayerController>();
        if(playerController.currentWeapon == 2)
        {
            AudioManager.instance.PlaySound("throwing_caltrops_"+Random.Range(1,3));
        }

        if(playerController.currentWeapon == 3)
        {
            AudioManager.instance.PlaySound("explosive_pin_"+Random.Range(1,3));
        }
        

        
    }

    void PlayerGunShooting()
    {
        AudioManager.instance.PlaySound("gunshot_"+Random.Range(1,3));
    }


        
}

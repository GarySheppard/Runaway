using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class ThirdPersonShooter : MonoBehaviour
{
    PlayerController playerController;
    public CinemachineFreeLook normalVirtualCamera;
    public CinemachineFreeLook aimVirtualCamera;
    public LayerMask aimColliderLayerMask = new LayerMask();
    public Transform debugTransform;
    public Transform bulletProjectile;
    public Transform spawnBulletPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePostion = Vector3.zero;

        Vector2 centerPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(centerPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mousePostion = raycastHit.point;
        }

        if (playerController.isAiming)
        {
            normalVirtualCamera.gameObject.SetActive(false);
            aimVirtualCamera.gameObject.SetActive(true);

            Vector3 aimTarget = mousePostion;
            aimTarget.y = transform.position.y;
            Vector3 aimDirection = (aimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            normalVirtualCamera.gameObject.SetActive(true);
            aimVirtualCamera.gameObject.SetActive(false);
        }

        if (playerController.isAttacking == true && playerController.currentWeapon == 1)
        {
            Vector3 aim = (mousePostion - spawnBulletPosition.position).normalized;
            Transform bullet = Instantiate(bulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aim, Vector3.up));
            Bullet bulletScript = bullet.gameObject.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.playerController = playerController;
            }
            playerController.isAttacking = false;
        }
    }
}
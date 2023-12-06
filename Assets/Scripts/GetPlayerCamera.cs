using Cinemachine;
using Fusion;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerCameraRoot;

    public GameObject aimCamera;
    private void Start()
    {
        NetworkObject thisObject = GetComponent<NetworkObject>();

        if (thisObject.HasStateAuthority)
        {
            GameObject virtualCamera = GameObject.Find("PlayerFollowCamera");
            aimCamera = GameObject.Find("PlayerAimCamera");
            virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot;
            aimCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot;

            GetComponent<ThirdPersonController>().enabled = true;
            GetComponent<ShooterController>().enabled = true;

            // also fneed to fixed sounds
        }
    }
}

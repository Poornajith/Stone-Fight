using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

    private Transform debugTransform;

    [SerializeField] private Transform mushroomProjectilePrefab;
    [SerializeField] private Transform spawnMushroomProjectilePosition;
    [SerializeField] private Transform spawnArrowPosition;
    [SerializeField] private Transform arrowPrefab;

    private GameObject aimVirtualCamera;
    private Animator animator;
    //private ThirdPersonController thirdPersonController;

    private int selectedWeapon = 0;

    Vector3 aimDir;
    WeaponSwap weaponSwap;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
       // thirdPersonController = GetComponent<ThirdPersonController>();
        debugTransform = GameObject.Find("AimDebugBall").transform;
    }
    private void Start()
    {
        aimVirtualCamera = GetComponent<GetPlayerCamera>().aimCamera;
        aimVirtualCamera.SetActive(false);
        weaponSwap = GetComponent<WeaponSwap>();

    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        selectedWeapon = weaponSwap.GetSelectedWeapon();

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("selected weapon : " + selectedWeapon);

            if (selectedWeapon != 0)
            {
                //Debug.Log("shooter Aiming");
                aimVirtualCamera.SetActive(true);

                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

                if (selectedWeapon == 1)
                {

                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Bow attack");                       
                        aimDir = (mouseWorldPosition - spawnArrowPosition.position).normalized;
                        Instantiate(arrowPrefab, spawnArrowPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                    }
                    // activate animation layer 2, aiming layer
                    // animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
                    // animator.SetBool("IsBowAiming", true);
                }if (selectedWeapon == 2)
                {
                    Debug.Log("Throwable throw");
                    aimDir = (mouseWorldPosition - spawnMushroomProjectilePosition.position).normalized;  
                    Instantiate(mushroomProjectilePrefab, spawnMushroomProjectilePosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                }

            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Melee attack");
                    animator.Play("MeleeComboAttack");
                }
            }


        }
        if (Input.GetMouseButtonUp(1))
        {
            aimVirtualCamera.SetActive(false);
           // thirdPersonController.SetSensitivity(normalSensitivity);
           // thirdPersonController.SetRotateOnMove(true);
           // animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (Input.GetMouseButtonDown(0))
        {
            

            if (selectedWeapon == 0)
            {
                Debug.Log("Melee attack");
                animator.Play("MeleeComboAttack");
            }

            if (selectedWeapon == 1) // bow
            {
                Debug.Log("bow normal attack");
                aimDir = (mouseWorldPosition - spawnArrowPosition.position).normalized;
                Instantiate(arrowPrefab, spawnArrowPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
            if (selectedWeapon == 2) // throwable
            {
                // play throw animation
                
                // sync animation timing with throw 


                // throw 
                aimDir = (mouseWorldPosition - spawnMushroomProjectilePosition.position).normalized;
                Instantiate(mushroomProjectilePrefab, spawnMushroomProjectilePosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
            
        }


    }
}

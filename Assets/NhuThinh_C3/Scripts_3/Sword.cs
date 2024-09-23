using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController3 playerController;
    private ActiveWeapons activeWeapon;

    private GameObject slashAnim;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController3>();
        activeWeapon = GetComponentInParent<ActiveWeapons>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;  


        Vector3 direction = mousePos - playerController.transform.position;


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        Vector3 originalScale = new Vector3(0.959999979f, 0.790000021f, 0.0399999991f);


        if (playerController.FacingLeft)
        {
            activeWeapon.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {

            activeWeapon.transform.localScale = originalScale;
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}

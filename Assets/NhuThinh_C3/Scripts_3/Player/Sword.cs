using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour,IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private Transform weaponCollider;
    private Animator myAnimator;

    private GameObject slashAnim;

    private void Awake()
    {        
        myAnimator = GetComponent<Animator>();        
    }
    private void Start()
    {
        weaponCollider=PlayerController3.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public void Attack() {
        if (myAnimator != null && myAnimator.isActiveAndEnabled)
        {
            myAnimator.SetTrigger("Attack");
        }

        if (slashAnimSpawnPoint != null)
        {
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
        }
        if (weaponCollider != null)
        {
            weaponCollider.gameObject.SetActive(true);
        }
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController3.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController3.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController3.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);            
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);            
        } else {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);            
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

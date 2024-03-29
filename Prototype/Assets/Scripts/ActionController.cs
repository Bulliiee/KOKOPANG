using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionController : MonoBehaviour
{
    // ���� ������ �ʤäӴ� �Ÿ�
    [SerializeField]
    private float range;

    // ���� ������ �� trye
    private bool pickupActivated = false;

    // �浹ü ���� ����
    private RaycastHit hitInfo;

    // ������ ���̾�� �����ϵ��� ���̾� ����ũ�� ����
    [SerializeField]
    private LayerMask layerMask;

   // �ʿ��� ������Ʈ
    [SerializeField]
    private TMP_Text actionText;
    [SerializeField]
    private Inventory theInventory;

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        ItemPickup itemPickup = hitInfo.transform.GetComponent<ItemPickup>();
        if (itemPickup != null && itemPickup.item != null)
        {
            actionText.text = itemPickup.item.itemName + " ȹ�� " + "<color=yellow>" + "(E)" + "</color>";
        } 
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            ItemPickup itemPickup = hitInfo.transform.GetComponent<ItemPickup>();
            if (itemPickup != null && itemPickup.item != null)
            {
                theInventory.AcquireItem(itemPickup.item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }
}

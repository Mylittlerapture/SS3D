using System;
using Mirror;
using UnityEngine;

[Serializable]
public class Item : NetworkBehaviour
{
    public Sprite Sprite;

    public SlotTypes compatibleSlots = SlotTypes.LeftHand | SlotTypes.RightHand | SlotTypes.Storage;

    [SerializeField]
    private GameObject visualObjectPrefab;

    [SyncVar]
    public bool Held;

    public GameObject visual;
    
    public void CreateVisual(Transform target, GameObject player)
    {
        if (!visual) visual = Instantiate(visualObjectPrefab);
        visual.name = "visual - " + name;

        if (isServer) NetworkServer.Spawn(visual.gameObject);
        //else
        //    NetworkServer.SpawnWithClientAuthority(visual.gameObject, player);
    }

    public void MoveVisual(ItemSlot slot)
    {
        slot.item.transform.localPosition = slot.physicalItemLocation.position;
        slot.item.transform.localRotation = slot.physicalItemLocation.rotation;
        slot.item.transform.localScale = Vector3.one;
    }

    [ClientRpc]
    public void RpcDrop()
    {
        ShowOriginal();

        //transform.position = visual.transform.position;
        //transform.rotation = visual.transform.rotation;
        //visual.transform.SetParent(null);

        Destroy(visual.gameObject);
    }

    public void HideOriginal()
    {
        transform.localScale = Vector3.zero;
    }

    public void ShowOriginal()
    {
        transform.localScale = Vector3.one;
    }
}
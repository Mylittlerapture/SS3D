using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class InventoryUi : NetworkBehaviour //MonoBehaviour
{
    public Inventory Inventory;

    public abstract List<ItemSlot> GetSlots();
}
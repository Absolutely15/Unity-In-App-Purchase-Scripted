using System.Collections.Generic;
using UnityEngine;

namespace NVTT.InAppPurchase
{
    [CreateAssetMenu(menuName = "ScriptableObject/IAP Consumable Data")]
    public class IAPConsumableData : ScriptableObject
    {
        public List<int> diamondsPackData;
    }
}


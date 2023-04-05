using System;
using System.Collections.Generic;
using UnityEngine;

namespace NVTT.InAppPurchase
{
    public static class IAPData
    {
        private const string KeyIAPNonConsumable = "iap_non_consumable";
    
        public static void SerializeIAPNonConsumableSaveData(IAPNonConsumableSaveData iapNonConsumableSaveData)
        {
            var stringSave = JsonUtility.ToJson(iapNonConsumableSaveData);
            PlayerPrefs.SetString(KeyIAPNonConsumable, stringSave);
        }
    
        public static IAPNonConsumableSaveData DeserializeIAPNonConsumableSaveData()
        {
            var temp = PlayerPrefs.GetString(KeyIAPNonConsumable);
            var iapSaveData = JsonUtility.FromJson<IAPNonConsumableSaveData>(temp);
            return iapSaveData;
        }
    }
    public static class IAPNonConsumableBundle
    {
        public const int NoAds = 0;
    }
    
    [Serializable]
    public class IAPNonConsumableSaveData
    {
        public List<bool> isPackageOwned;
    }
}

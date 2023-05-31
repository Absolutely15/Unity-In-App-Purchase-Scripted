using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using NVTT.InAppPurchase;

public class IAPNonConsumableManager : Singleton<IAPNonConsumableManager>
{
    #region Properties
    public IAPNonConsumableSaveData iapNonConsumableSaveData;
    
    [HideInInspector] public UnityEvent<int> onPurchaseComplete = new UnityEvent<int>();

    [SerializeField] private List<string> nonConsumableProductId = new List<string>();

    private Dictionary<string, UnityAction> nonConsumableDictionary;
    #endregion
    
    #region Init
    private void OnValidate()
    {
        if (nonConsumableProductId.Count == 0 || nonConsumableProductId == null)
            nonConsumableProductId = IAPUtilities.GetProductsIDType(ProductType.NonConsumable);
    }

    private void Start()
    {
        Init();
    }
    
    private void Init()
    {
        nonConsumableDictionary = new Dictionary<string, UnityAction>
        {
            { nonConsumableProductId[IAPNonConsumableBundle.NoAds], UnlockNoAdsPackage }
            //Add more package here
        };
        
        // Load data from PlayerPref
        var tempSaveData = IAPData.DeserializeIAPNonConsumableSaveData();
        if (tempSaveData != null)
            iapNonConsumableSaveData = tempSaveData;
    }
    #endregion
    
    #region Purchase
    public void ProcessPurchase(Product product)
    {
        nonConsumableDictionary[product.definition.id].Invoke();
    }
    #endregion
    
    #region Unlock Package
    private void ProductOnCompletePurchase(int id)
    {
        onPurchaseComplete.Invoke(id);
        iapNonConsumableSaveData.isPackageOwned[id] = true;
        IAPData.SerializeIAPNonConsumableSaveData(iapNonConsumableSaveData);
    }

    private void UnlockNoAdsPackage()
    {
        //TODO: Remove ads function
        
        ProductOnCompletePurchase(IAPNonConsumableBundle.NoAds);
    }
    #endregion
}

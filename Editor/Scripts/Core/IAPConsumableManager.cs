using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using System.Collections.Generic;
using NVTT.InAppPurchase;

public class IAPConsumableManager : Singleton<IAPConsumableManager>
{
    #region Properties
    [HideInInspector] public UnityEvent<int> onPurchaseComplete;
    [SerializeField] private IAPConsumableData iapConsumableData;
    [SerializeField] private List<string> consumableProductId;

    private readonly Dictionary<string, UnityAction> consumableDictionary = new Dictionary<string, UnityAction>();
    #endregion

    #region Init
    private void OnValidate()
    {
        if (consumableProductId.Count == 0 || consumableProductId == null)
            consumableProductId = IAPUtilities.GetProductsIDType(ProductType.Consumable);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (var i = 0; i < consumableProductId.Count; i++)
        {
            var i1 = i;
            consumableDictionary.Add(consumableProductId[i], () =>
            {
                AddDiamondAmount(iapConsumableData.diamondsPackData[i1]);
                ProductOnCompletePurchase(i1);
            });
        }
    }
    #endregion

    #region Purchase
    public void ProcessPurchase(Product product)
    {
        consumableDictionary[product.definition.id].Invoke();
    }

    private void ProductOnCompletePurchase(int id)
    {
        onPurchaseComplete.Invoke(id);
    }

    private void AddDiamondAmount(int amount)
    {
        //TODO: Add diamond
    }
    #endregion

}

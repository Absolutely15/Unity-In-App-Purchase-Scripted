using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using NVTT.InAppPurchase;
using UnityEngine.Events;

public class IAPSubscriptionManager : Singleton<IAPSubscriptionManager>
{
    [HideInInspector] public UnityEvent<int> onPurchaseComplete = new UnityEvent<int>();
    
    [SerializeField] private List<string> subscriptionProductId = new List<string>();
    
    private Dictionary<string, UnityAction> subscriptionDictionary;
    
    private void OnValidate()
    {
        if (subscriptionProductId.Count == 0 || subscriptionProductId == null)
            subscriptionProductId = IAPUtilities.GetProductsIDType(ProductType.Subscription);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        subscriptionDictionary = new Dictionary<string, UnityAction>
        {
            {subscriptionProductId[0], MonthlySubscribe}
            //Add more functions here
        };
    }

    public void ProcessPurchase(Product product)
    {
        subscriptionDictionary[product.definition.id].Invoke();
    }
    private void ProductOnCompletePurchase(int id)
    {
        onPurchaseComplete.Invoke(id);
        
        //TODO: Read Unity docs for more information about SubscriptionManager and SubscriptionInfo
    }

    private void MonthlySubscribe()
    {
        //TODO: Write subscribe function here
        
    }
}

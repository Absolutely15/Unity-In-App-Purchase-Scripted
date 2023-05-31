using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using NVTT.InAppPurchase;

public class IAPSubscriptionManager : Singleton<IAPSubscriptionManager>
{
    [HideInInspector] public List<bool> subscriptionProductsPurchased;
    [HideInInspector] public UnityEvent<int> onPurchaseComplete = new UnityEvent<int>();
    
    [SerializeField] private List<string> subscriptionProductId = new List<string>();

    private Dictionary<string, UnityAction> subscriptionDictionary;
    private List<Product> subscriptionPurchased;

    private void OnValidate()
    {
        if (subscriptionProductId.Count == 0 || subscriptionProductId == null)
            subscriptionProductId = IAPUtilities.GetProductsIDType(ProductType.Subscription);
    }

    private void Start()
    {
        Init();
        CheckSubscription();
    }

    private void Init()
    {
        subscriptionPurchased = InAppPurchaseManager.Instance.ProductsPurchased(ProductType.Subscription);
            
        subscriptionDictionary = new Dictionary<string, UnityAction>
        {
            {subscriptionProductId[0], MonthlySubscribe}
            //Add more functions here
        };
    }

    //TODO: Use the bool list to check if the subscription pack is expired so that you can manually update UI and function of the subscription package you need
    private void CheckSubscription()
    {
        for (var i = 0; i < subscriptionPurchased.Count; i++)
        {
            SubscriptionManager p = new SubscriptionManager(subscriptionPurchased[i], null);

            if (p.getSubscriptionInfo().isExpired() != Result.True) continue;
            subscriptionProductsPurchased[i] = false;
        }
    }

    public void ProcessPurchase(Product product)
    {
        subscriptionDictionary[product.definition.id].Invoke();
    }
    private void ProductOnCompletePurchase(int id)
    {
        onPurchaseComplete.Invoke(id);
    }

    private void MonthlySubscribe()
    {
        //TODO: Write subscribe function here
        
    }
}

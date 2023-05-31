using UnityEngine.UI;

public class IAPProductButton : IAPProductText
{
    public Button purchaseBtn;
    
    private void OnValidate()
    {
        if (purchaseBtn == null)
            purchaseBtn = GetComponent<Button>();
    }
    
    protected override void Init()
    {
        base.Init();

        purchaseBtn.onClick.AddListener(PurchaseProduct);
    }

    private void PurchaseProduct() => InAppPurchaseManager.Instance.BuyProductID(productId);
}

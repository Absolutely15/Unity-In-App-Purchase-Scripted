#if UNITY_IOS
using UnityEngine;
using UnityEngine.UI;

public class IAPRestorePurchase : MonoBehaviour
{
    [SerializeField] private Button restoreBtn;

    private void OnValidate()
    {
        if (restoreBtn == null)
            restoreBtn = GetComponent<Button>();
    }

    private void Start()
    {
        restoreBtn.onClick.AddListener(InAppPurchaseManager.Instance.RestorePurchases);
    }
}
#endif

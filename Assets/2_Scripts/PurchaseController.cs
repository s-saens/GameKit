using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseController : MonoBehaviour
{
    public void OnPurchase(Product product)
    {
        Debug.Log("BUY: " + product.definition.id);
        switch (product.definition.type)
        {
            case ProductType.NonConsumable :
                GameData.IAP.non_consumable[product.definition.id].value = true;
                break;
            case ProductType.Consumable :
                GameData.IAP.consumable[product.definition.id].value += (int)(product.definition.payout.quantity);
                break;
            default :
                // none / description
                break;
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("FAILED");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class IAPPurchase : MonoBehaviour, IStoreListener
{
    #region DEBUG
    public InitializationFailureReason mError;
    #endregion

    public int isInit;
    public bool hasAds;
    public float waitTime = 2.0f;
    public bool inError = false;
    public bool isChecked = false;
    
    /* Declarando todos los ids de los productos de la tienda */
    public static string p10gemsPack = "p_10_gems_pack";

    public int purchaseIdentifier = -1;
    private IStoreController mStoreController;
    private IExtensionProvider mStoreExtensionProvider;

    private void OnDisable()
    {
        StopCoroutine(InternetFail());
    }

    private void Awake()
    {
        isInit = -1;

        if (CheckInternet())
        {
            if (mStoreController == null)
            {
                InitPurchasing();
            }
        }
        else
        {
            ConnectionError();
        }
    }

    public void DestroyIAPPurchase()
    {
        Destroy(gameObject);
    }

    #region InternetError
    public void ConnectionError()
    {
        if (!inError)
        {
            inError = true;
            StartCoroutine(InternetFail());
        }
    }

    private IEnumerator InternetFail()
    {
        bool load = false;

        while (!load)
        {
            yield return new WaitForSecondsRealtime(waitTime);

            if (CheckInternet())
            {
                load = true;
                if (mStoreController == null)
                {
                    InitPurchasing();
                }
            }
        }
    }
    #endregion

    #region IAP
    public void InitPurchasing()
    {
        if (IsInit())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(p10gemsPack, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInit()
    {
        return mStoreController != null && mStoreExtensionProvider != null;
    }
    #endregion

    #region IAP_EVENTS
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        isInit = 1;
        mStoreController = controller;
        mStoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        isInit = 0;
        mError = error;
        StopCoroutine(InternetFail());
        StartCoroutine(InternetFail());
    }
    #endregion

    #region ProductosChecks
    public void CheckAds()
    {
        if (isChecked == false)
        {
            isChecked = true;
        }
    }
    #endregion

    #region BUY
    public void Buy10GemsPack()
    {
        BuyProductID(p10gemsPack);
    }

    public void BuyProductID(String productID)
    {
        if (IsInit())
        {
            Product product = mStoreController.products.WithID(productID);
            
            if (product != null && product.availableToPurchase)
            {
                mStoreController.InitiatePurchase(product);
                Debug.Log("Successful " + product.definition.id + " purchase");
            }
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, p10gemsPack, StringComparison.Ordinal))
        {
            Debug.Log("Consumable purchase: " + purchaseEvent.purchasedProduct.definition.id + "    purchaseIdentifer: " + purchaseIdentifier);
            EventPurchaser.BuyConsumableState(true, purchaseIdentifier);
        }
        else
        {
            Debug.Log("Fail purchase");
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Fail on purchase of: " + product.definition.storeSpecificId + " reason: " + failureReason);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Internet
    public bool CheckInternet()
    {
        bool isInternetOn = false;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            isInternetOn = false;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            isInternetOn = true;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            isInternetOn = true;
        }

        return isInternetOn;
    }
    #endregion

    public class EventPurchaser  
    {
        public delegate void OnBuyConsumableState(bool state, int identifier);
        public static event OnBuyConsumableState onBuyConsumableState;

        public static void BuyConsumableState(bool state, int identifier)
        {
            if (onBuyConsumableState != null)
            {
                onBuyConsumableState(state, identifier);
            }
        }
    }
}

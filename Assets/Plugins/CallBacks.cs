using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBInstant;
using UnityEngine.UI;
public class CallBacks : MonoBehaviour
{

     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
     public static void Initialize()
     {
         //if (Application.platform != RuntimePlatform.WebGLPlayer)
         //{
         //    Debug.Log("Do not support " + Application.platform + " platform");
         //    return;
         //}

         GameObject gb = new GameObject("FBInstant_Initializer");
         CallBacks component = gb.AddComponent<CallBacks>();
         DontDestroyOnLoad(gb);
     }



    //payment

    public  void OnPaymentReady_Callback()
    {
       
        Debug.Log("call back recieved");
        if (FBInstantIAP.recieveOnReadyCallback != null)
            FBInstantIAP.recieveOnReadyCallback();
    }

    public void purchaseItem_callback(string purchasedItem)
    {
        FBInstantIAP.item = JsonUtility.FromJson<Purchase>(purchasedItem);
        FBInstantIAP.ConsumeProduct(FBInstantIAP.item.purchaseToken);
        if (FBInstantIAP.purchasedItemCallback != null)
            FBInstantIAP.purchasedItemCallback();
    }


    public void OnConsuming_callback(string purchaseData)
    {
        //FBInstantIAP.consumeItemCallback();
        Debug.Log("consuming callback");
        Debug.Log(purchaseData);
    }

    public void purchasedProduct_callback(string purchaseData)
    {
        Debug.Log(purchaseData);

        Debug.Log("--------------------------------------");

        FBInstantIAP.purchases = ArrayDeserializer.getJsonArray<Purchase>(purchaseData);
    }

}
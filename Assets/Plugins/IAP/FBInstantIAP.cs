using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace FBInstant
{
    public class FBInstantIAP : MonoBehaviour
    {

        public static Purchase[] purchases;
        public static Purchase item;

        public static  Action recieveOnReadyCallback = null;
        public static  Action consumeItemCallback = null;
        public static Action purchasedItemCallback = null;
        

        #region JSLIB methods

        [DllImport("__Internal")]
        private static extern string SupportedAPI();

        [DllImport("__Internal")]
        private static extern void PurchaseItem(string id);

        [DllImport("__Internal")]
        private static extern void PaymentIsReady();

        [DllImport("__Internal")]
        private static extern void ConsumePurchasedItem(string id);

        [DllImport("__Internal")]
        private static extern void GetCatalog();

        [DllImport("__Internal")]
        private static extern void GetPurchasedItem();

        #endregion



        //callback from web if payment is ready
        public static void onPaymentReady(Action recieved)
        {
           
            recieveOnReadyCallback = recieved;
            PaymentIsReady();

        }

        //callback subscriber
        private static void Reciever() { Debug.Log("my reciever"); }


        void purchaseItemCallback() { Debug.Log(item); }

        //show catalog

        void Catalog()
        {
            GetCatalog();
        }


        //purchase the product with product id
        public static void PurchaseProduct(string id , Action callbackForConsuming)
        {
            onPaymentReady(Reciever);

            
            //Debug.Log("supported api is present " + SupportedAPI().Contains("payments.purchaseAsync"));

            if(SupportedAPI().Contains("payments.purchaseAsync"))
            {
                //Debug.Log("purchasing");
                PurchaseItem(id);
                purchasedItemCallback = callbackForConsuming;
                //Debug.Log(item.purchaseToken);
               
            }
               
        }

        //consume item
        public static void ConsumeProduct(string id)
        {
            //Debug.Log(consumeItemCallback);
            ConsumePurchasedItem(id);
        }

        void ShowPurchasedItem()
        {
            GetPurchasedItem();
        }
    }

}



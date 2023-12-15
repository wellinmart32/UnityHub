using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
	public GameObject bt_NoAds;
	public GameObject bannerAnuncio;
	//void OnEnable()
	//{
	//	EventPurchaser.on += EventTienda;
	//}
	//void OnDisable()
	//{
	//	EventPurchaser.onStateCompraNoAds -= EventTienda;
	//}
	//void StoreEvent(bool se)
	//{
	//	if (se)
	//	{
	//		//HAY ANUNCIOS
	//		bt_NoAds.SetActive(true);
	//		bannerAnuncio.SetActive(true);
	//	}
	//	else
	//	{
	//		//NO HAY ANUNCIOS
	//		bt_NoAds.SetActive(false);
	//		bannerAnuncio.SetActive(false);
	//	}
	//}
	//
	public void BuyNonConsumable()
	{
		//IAPPurchase.instance.BuyProductNoAds();			// Modificar para que los noAds sean una configuracion para no consumible.
	}
	public void BuyConsumable()
	{
		Debug.Log("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ");
		//IAPPurchase.instance.Buy10GemsPack();
	}
	void Start()
	{
		////es init!
		//if (IAPPurchase.instance.isInit == 1)
		//{
		//	//if (IAPPurchase.instance.hayAds == false)
		//	//{
		//	//	bt_NoAds.SetActive(false);
		//	//	bannerAnuncio.SetActive(false);
		//	//}
		//	//else
		//	//{
		//	//	bt_NoAds.SetActive(true);
		//	//	bannerAnuncio.SetActive(true);
		//	//}
		//}
		//else
		//{
		//	bt_NoAds.SetActive(false);//ya que la tienda no esta disponible
		//	bannerAnuncio.SetActive(true);
		//}
	}
}

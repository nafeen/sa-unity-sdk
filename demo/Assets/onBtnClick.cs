using UnityEngine;
using System;
using System.Collections;

namespace SuperAwesome {

	public class onBtnClick : MonoBehaviour, SALoaderInterface, SAAdInterface, SAVideoAdInterface, SAParentalGateInterface {

		private SALoader loader1 = null, loader2 = null, loader3 = null;
		private SAAd adBanner = null;
		private SAAd adVideo = null;
		private SAAd adInterstitial = null;

		// Use this for initialization
		void Start () {
			// do nothing
		}
		
		// Update is called once per frame
		void Update () {
			// do nothing
		}
		
		// button actions
		public void loadAds () {

			loader1 = SALoader.createInstance ();
			loader1.loaderDelegate = this;
			loader1.loadAd (40);	// movie

			loader2 = SALoader.createInstance ();
			loader2.loaderDelegate = this;
			loader2.loadAd (44);	// rm interstitial

			loader3 = SALoader.createInstance ();
			loader3.loaderDelegate = this;
			loader3.loadAd (45); // banner
		}

		public void playBanner () {
			if (adBanner != null) {
				SABannerAd bad = SABannerAd.createInstance();
				bad.setAd(adBanner);
				bad.position = SABannerAd.BannerPosition.BOTTOM;
				bad.size = SABannerAd.BannerSize.BANNER_320_50;
				bad.isParentalGateEnabled = true;
				bad.adDelegate = this;
				bad.parentalGateDelegate = this;
				bad.play();
			}
		}

		public void playInterstitial () {
			if (adInterstitial != null) {
				SAInterstitialAd iad = SAInterstitialAd.createInstance();
				iad.setAd(adInterstitial);
				iad.isParentalGateEnabled = true;
				iad.adDelegate = this;
				iad.parentalGateDelegate = this;
				iad.play();
			}
		}

		public void playVideo() {
			if (adVideo != null) {
				SAVideoAd vad = SAVideoAd.createInstance();
				vad.setAd(adVideo);
				vad.isParentalGateEnabled = true;
				vad.shouldShowCloseButton = true;
				vad.shouldAutomaticallyCloseAtEnd = true;
				vad.adDelegate = this;
				vad.parentalGateDelegate = this;
				vad.videoAdDelegate = this;
				vad.play();
			}
		}

		/** <SALoaderInterface> */
		void SALoaderInterface.didLoadAd(SAAd ad) {
			if (ad.placementId == 40) {
				adVideo = ad;
			} else if (ad.placementId == 44) {
				adInterstitial = ad;
			} else {
				adBanner = ad;
			}
		}

		void SALoaderInterface.didFailToLoadAd(int placementId) {
			Debug.Log ("[Unity] - didFailToLoadAd " + placementId);
		}

		/** <SAAdInterface> */
		public void adWasShown(int placementId) {
			Debug.Log ("[Unity] - adWasShown " + placementId);
		}

		public void adFailedToShow(int placementId) {
			Debug.Log ("[Unity] - adFailedToShow" + placementId);
		}

		public void adWasClosed(int placementId) {
			Debug.Log ("[Unity] - adWasClosed " + placementId);
		}

		public void adWasClicked(int placementId) {
			Debug.Log ("[Unity] - adWasClicked " + placementId);
		}

		public void adHasIncorrectPlacement(int placementId) {
			Debug.Log ("[Unity] - adHasIncorrectPlacement");
		}

		public void parentalGateWasCanceled(int placementId) {
			Debug.Log ("[Unity] - parentalGateWasCanceled " + placementId);
		}
		
		public void parentalGateWasFailed(int placementId) {
			Debug.Log ("[Unity] - parentalGateWasFailed " + placementId);
		}

		public void parentalGateWasSucceded(int placementId) {
			Debug.Log ("[Unity] - parentalGateWasSucceded " + placementId);
		}

		public void adStarted(int placementId){
			Debug.Log ("[Unity] - adStarted " + placementId);
		}
		
		public void videoStarted(int placementId){
			Debug.Log ("[Unity] - videoStarted " + placementId);
		}
		
		public void videoReachedFirstQuartile(int placementId){
			Debug.Log ("[Unity] - videoReachedFirstQuartile " + placementId);
		}
		
		public void videoReachedMidpoint(int placementId){
			Debug.Log ("[Unity] - videoReachedMidpoint " + placementId);
		}
		
		public void videoReachedThirdQuartile(int placementId){
			Debug.Log ("[Unity] - videoReachedThirdQuartile " + placementId);
		}
		
		public void videoEnded(int placementId){
			Debug.Log ("[Unity] - videoEnded " + placementId);
		}
		
		public void adEnded(int placementId){
			Debug.Log ("[Unity] - adEnded " + placementId);
		}
		
		public void allAdsEnded(int placementId){
			Debug.Log ("[Unity] - allAdsEnded " + placementId);
		}
	}
}


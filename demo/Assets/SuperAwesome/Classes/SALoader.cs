/** 
 * Imports used for this class
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.Runtime.InteropServices;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * This is the Unity pre-loader class that manages the Unity-iOS/Android communication
	 */
	public class SALoader: MonoBehaviour, SANativeInterface {

		/** private members */
		private int placementId = 0;

		/** delegates & events */
		public SALoaderInterface loaderDelegate = null;

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")] 
		private static extern void SuperAwesomeUnityLoadAd(string unityName, int placementId, bool isTestingEnabled);
#endif

		/** static function initialiser */
		public static SALoader createInstance() {
			/** create a new game object */
			GameObject obj = new GameObject ();
			
			/** add to that new object the video ad */
			SALoader loader = obj.AddComponent<SALoader> ();
			loader.name = "SALoader_" + (new System.Random()).Next(100, 1000).ToString();
			
			/** and return the ad Obj instance */
			return loader;
		}

		void Start () {
			/** do nothing */
		}
		
		void Update () {
			/** do nothing */
		}

		/** This function is used as a static wrapper against SALoaderScript's member loadAd function */
		public void loadAd(int placementId) {

			/** assign placement */
			this.placementId = placementId;
			
			/** get if testing is enabled */
			bool isTestingEnabled = SuperAwesome.instance.isTestingEnabled ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SALoader.SuperAwesomeUnityLoadAd(this.name, placementId, isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			Debug.Log("Not in Android yet");
#else
			Debug.Log ("Load: " + this.name + ", " + placementId + ", " + isTestingEnabled);
#endif
		}

		/** 
		 * Native callback interface implementation
		 */
		public void nativeCallback(string payload) {
			Dictionary<string, object> payloadDict;
			string type = "";

			/** try to get payload and type data */
			try {
				payloadDict = Json.Deserialize (payload) as Dictionary<string, object>;
				type = (string) payloadDict ["type"];
			} catch {
				if (loaderDelegate != null) {
					loaderDelegate.didFailToLoadAd(this.placementId);
				}
				return;
			}

			switch (type) {
			case "callback_didLoadAd":{
				/** form the new ad */
				Dictionary<string, object> data = payloadDict["adJson"] as Dictionary<string, object>;
				SAAd ad = new SAAd();
				ad.adJson = Json.Serialize(data);
				ad.placementId = this.placementId;

				if (loaderDelegate != null) {
					loaderDelegate.didLoadAd(ad);
				}
				break;
			}
			case "callback_didFailToLoadAd":{
				if (loaderDelegate != null) {
					loaderDelegate.didFailToLoadAd(this.placementId);
				}
				break;
			}
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamInput : MonoBehaviour {

	private WebCamDevice[] devices;

	// Use this for initialization
	void Start () {

		devices = WebCamTexture.devices;
		for(int i = 0 ; i < devices.Length ; i++ ){
			Debug.Log(devices[i].name);
		}
		WebCamTexture webcam = new WebCamTexture(devices[0].name);
		GetComponent<RawImage>().texture = webcam;
		webcam.Play();	

	}
}

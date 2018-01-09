using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGeneretor : MonoBehaviour {
	[SerializeField]
	private GameObject _ballPrefab;
	private GameObject _ballObject;
	[SerializeField]
	private SteamVR_TrackedObject _trackObj;
	public GameObject insPos;


	// Use this for initialization
	void Start () {
		GameManager.Instance.AddStartAction("generateBall", Generete);
		GameManager.Instance.AddFinishAction("deleteBall", Delete);
	}
	
	// Update is called once per frame
	void Update () {
		/*var device = SteamVR_Controller.Input ((int)_trackObj.index);
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			Generete ();
		}*/
	}

	//生成
	private void Generete(){
		//Vector3 cameraPos = Camera.main.transform.position + new Vector3(3, -6, 7f);
		_ballObject = (GameObject)Instantiate(_ballPrefab, insPos.transform.position, Quaternion.identity);
	}
	
	//消す
	private void Delete(){
		Destroy(_ballObject);
	}
}

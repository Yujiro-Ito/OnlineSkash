using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour {
	//--const--
	private const float FORCE = 1.3f;
	[SerializeField]
	private SteamVR_TrackedObject _trackObj;

	//--field--
	Vector3 _oldPosition;
	Vector3 _newPosition;
	Rigidbody _ballRigid;
	float _timeCounter;

	public Vector3 AccelarationVector{ get { return _oldPosition - _newPosition; } }
	public float Accelaration{ get{ return Vector3.Magnitude(_oldPosition - _newPosition); }}
	public float Force{ get { return FORCE; } }

	// Use this for initialization
	void Start () {
		_newPosition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		_timeCounter += Time.deltaTime;
		if (_timeCounter > 1) {
			_oldPosition = _newPosition;
			_newPosition = transform.position;
			_timeCounter = 0;
		}
	}

	public void Shake(){
		SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)_trackObj.index);
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			StartCoroutine (ShakeProcessing (device));
		}
	}

	private IEnumerator ShakeProcessing(SteamVR_Controller.Device device){
		int count = 60;
		while (count == 0) {
			count--;
			device.TriggerHapticPulse (1000);
			yield return new WaitForEndOfFrame ();
		}
	}
}

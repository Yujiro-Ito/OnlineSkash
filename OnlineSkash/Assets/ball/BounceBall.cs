using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour {

	public float INIT_DEGREE = 1f;
	public float INIT_SPEED = 5f;
	private Rigidbody _rigidbody;
	private bool start = true;
	private Racket _racket;
	private bool _hitRacket;
	private Vector3 _oldVector;
	private Vector3 _newVector;
	private float _timeCounter;
	private LineRenderer _line;

	private int coolCount = 60;
	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
		//_rigidbody.AddForce (Vector3.left * 3 + Vector3.down * 3, ForceMode.Impulse);
		_racket = GameObject.FindObjectOfType<Racket> ();
		_hitRacket = false;
	}

	// Update is called once per frame
	void Update () {
		coolCount++;

	}

	void OnTriggerEnter(Collider col){
		Ray ray = new Ray(transform.position, _rigidbody.velocity);
		//クールタイムを設けてみる
		if (coolCount >= 60) {
			//ラケットと当たったら
			if (col.transform.tag == "Racket" && _hitRacket == false) {
				_rigidbody.AddForce (_racket.AccelarationVector * _racket.Force, ForceMode.Impulse);
				_hitRacket = true;
				col.transform.GetComponent<Racket> ().Shake ();
				Debug.Log ("Hit");
				coolCount = 0;
			}
		}
		RaycastHit info;
		if(Physics.Raycast(ray, out info, 3f)){
			//それ以外だったら
			Vector3 F = _rigidbody.velocity;
			Vector3 N = info.normal;
			float a = Vector3.Dot(-F, N);
			Vector3 newVelocity = F + 2 * a * N;
			_rigidbody.velocity = newVelocity * 0.95f;
			_hitRacket = false;
		}
	}

	//ベクトルの要素を2乗したベクトルを返す
	Vector3 sqrVector(Vector3 vec){
		Vector3 result = vec;
		result.x = Mathf.Pow (result.x, 2);
		result.y = Mathf.Pow (result.y, 2);
		result.x = Mathf.Pow (result.z, 2);
		return result;
	}
}

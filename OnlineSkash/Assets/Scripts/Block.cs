using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	private const float LIFE_TIME_MAX = 3f;
	public const float ADD_FORCE = 50;
	private float _currentPower;
	private float _attenuateRate;
	private Renderer _renderer;
	
	Racket racket;

	// Use this for initialization
	void Start () {
		racket = GameObject.FindObjectOfType<Racket>();
		_currentPower = 0.1f;
		_attenuateRate = 0f;
		_renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		ColorUpdate();
	}

	//当たり判定
	void OnTriggerEnter(Collider collider){
		if(collider.tag == "Racket"){
			float racketAccelaration = racket.Accelaration;

		}
	}

	//減衰率の計算
	private void AttenuateCalc(float a){
		_currentPower += a * ADD_FORCE;
		_attenuateRate = _currentPower * (Time.deltaTime * LIFE_TIME_MAX);
	}

	//現在の力の更新
	private void PowerUpdate(){
		if(_currentPower > 0){
			_currentPower -= _attenuateRate;
			if(_currentPower < 0) _currentPower = 0;
		}
	}

	//オブジェクトカラーの更新
	private void ColorUpdate(){
		_renderer.material.color = new Color(_renderer.material.color.r,
											_renderer.material.color.g,
											_renderer.material.color.b,
											(_currentPower / LIFE_TIME_MAX) * 0.95f + 0.05f);
	}
}

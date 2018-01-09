using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2 : MonoBehaviour {

	public int stateW = 2;
	private int stateA = 2;
	private int stateS = 2;

	// Use this for initialization
	void Start () {
		shotBall ();
	}

	void shotBall(){
		stateW = 0;
		stateA = 0;
		stateS = 0;
	}

	// Update is called once per frame
	void Update () {

		//壁反射（position）の挙動
		switch (stateA) {
		case 0:
			this.transform.position -= new Vector3 (0.1f, 0f, 0f);
			break;
		case 1:
			this.transform.position += new Vector3 (0.1f, 0f, 0f);
			break;
		}

		switch (stateW) {
		case 0:
			this.transform.position -= new Vector3 (0f, 0.1f, 0f);
			break;
		case 1:
			this.transform.position += new Vector3 (0f, 0.1f, 0f);
			break;
		}

		switch (stateS) {
		case 0:
			this.transform.position += new Vector3 (0f, 0f, 0.1f);
			break;
		case 1:
			this.transform.position -= new Vector3 (0f, 0f, 0.1f);
			break;
		}
	}

	void FixedUpdate()
	{
		//Collider[] colNet = Physics.OverlapBox (transform.position, GetComponent<BoxCollider> ().size / 2.0f, transform.rotation);


	}

	void OnTriggerStay(Collider col){
		Debug.Log (col.gameObject.name);
		if (col.gameObject.tag == "Hand") {
			//同時にあたり判定できない！
			stateS = 0;
		}
		//ラケット簡易跳ね返り（奥に跳ね返すだけ）
		FixedUpdate();

		//壁反射(position版)
		if(col.gameObject.name == "Right"){
			stateA = 0;
		}
		if(col.gameObject.name == "Left"){
			stateA = 1;
		}
		if (col.gameObject.name == "Up") {
			stateW = 0;
		}
		if (col.gameObject.name == "Floor") {
			stateW = 1;
		}
		if (col.gameObject.name == "Front") {
			stateS = 0;
		}
		if (col.gameObject.name == "Over") {
			stateS = 1;
		}
	}
}

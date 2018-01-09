using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	[SerializeField]
	private Text _score;
	[SerializeField]
	private Text _time;
	private bool _finish = false;

	// Use this for initialization
	void Start () {
		_score.text = "SCORE : " + GameManager.Instance.Score;
		_time.text = "TIME   : " + GameManager.Instance.Time;
		GameManager.Instance.AddFinishAction("finishTime", FinishGame);
		StartCoroutine (GameStart ());
	}

	private IEnumerator GameStart(){
		yield return new WaitForSeconds (0.2f);
		GameManager.Instance.GameStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if(_finish == false){
			_score.text = "SCORE : " + GameManager.Instance.Score;
			_time.text = "TIME   : " + GameManager.Instance.Time;
		}
	}

	private void FinishGame(){
		_time.text = "FINISH";
		_finish = true;
	}
}

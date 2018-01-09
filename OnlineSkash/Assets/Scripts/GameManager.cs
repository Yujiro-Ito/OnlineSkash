using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
	//------singleton---------
	private static GameObject _singletonObject;
	private static GameManager _singleton;
	public static GameManager Instance{
		get{
			if(_singletonObject == null){
				_singletonObject = new GameObject("GameManager");
				_singletonObject.AddComponent<GameManager>();
				DontDestroyOnLoad(_singletonObject);
				_singleton = _singletonObject.GetComponent<GameManager>();
				_singleton.InitializeGame();
			}
			return _singleton;
		}
	}

	//-----const-----
	private const int GAME_TIME = 60;
	private const int ADD_SCORE = 10;

	//------field--------
	private float _score;
	private float _time;
	private bool _gameNow;
	private Dictionary<string, Action> _startAction = new Dictionary<string, Action>();
	private Dictionary<string, Action> _finishAction = new Dictionary<string, Action>();

	//----propatie----
	public int Score{
		get{ return (int)_score; }
	}

	public int Time{
		get{ return (int)_time; }
	}

	//----method----
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//ゲーム中の処理
		if(_gameNow){
			_time -= UnityEngine.Time.deltaTime;
			if(_time < 0){
				_gameNow = false;
				//登録された終了時のメソッドを全部呼び出し
				foreach(KeyValuePair<string, Action> action in _finishAction){
					action.Value();
				}
			}
		}
	}

	//ゲームの初期化
	public void InitializeGame(){
		_score = 0;
		_time = GAME_TIME;
		_gameNow = false;
	}

	///<summary>
	///このメソッドはゲームスタート時に呼んでください
	///</summary>
	public void GameStart(){
		_gameNow = true;
		//登録されたメソッドを全部呼び出し
		foreach(KeyValuePair<string, Action> action in _startAction){
			action.Value();
		}
	}

	//スコアの加算
	public void AddScore(){
		_score += ADD_SCORE;
	}

	//スタートアクションを追加
	public void AddStartAction(string actionName, Action action){
		if(_startAction.ContainsKey(actionName)){
			_startAction[actionName] = action;
		} else {
			_startAction.Add(actionName, action);
		}
	}

	//フィニッシュアクションを追加
	public void AddFinishAction(string actionName, Action action){
		if(_finishAction.ContainsKey(actionName)){
			_finishAction[actionName] = action;
		} else {
			_finishAction.Add(actionName, action);
		}
	}
}

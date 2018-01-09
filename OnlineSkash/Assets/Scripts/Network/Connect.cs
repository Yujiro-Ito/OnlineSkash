using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connect : MonoBehaviour {
	public GameObject CameraRig;
	public bool master = false;
	public Text _text;

	void Start() {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        PhotonNetwork.ConnectUsingSettings(null);
    }
 
    // ロビーに入ると呼ばれる
    void OnJoinedLobby() {
        Debug.Log("ロビーに入りました。");
 
        // ルームに入室する
        PhotonNetwork.JoinRandomRoom();
		_text.text = "Enter Lobby";

    }

	void CreateCameraRig(){
		Vector3 pos = new Vector3(9.63f, -0.43f, 11.95f);
		if(master){
			pos = new Vector3(9.63f, -0.43f, 43.7f);
		}
		CameraRig.transform.position = pos;
		//PhotonNetwork.Instantiate("CameraRig", pos, Quaternion.identity, 0);
	}
 
    // ルームに入室すると呼ばれる
    void OnJoinedRoom() {
        Debug.Log("ルームへ入室しました。");
		_text.text = "Im " + ((master) ? "master" : "client");

		CreateCameraRig();
    }
 
    // ルームの入室に失敗すると呼ばれる
    void OnPhotonRandomJoinFailed() {
        Debug.Log("ルームの入室に失敗しました。");
		_text.text = "Missed Enter Room";
 
        // ルームがないと入室に失敗するため、その時は自分で作る
        // 引数でルーム名を指定できる
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 2;
		roomOptions.IsOpen = true;
		roomOptions.IsVisible = true;
		master = true;
        PhotonNetwork.CreateRoom("myRoomName", roomOptions, null);
    }
}

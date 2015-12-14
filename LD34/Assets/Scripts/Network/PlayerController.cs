using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {
    public void OnStartLocalPlayer() {
        Debug.LogFormat("OnStartLocalPlayer");
    }

    public void Update() {
#if UNITY_ANDROID
        Vector3 pos = Input.GetTouch(0).position;
#else
        Vector3 pos = Input.mousePosition;
#endif

        Vector3 realWorldPos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector3(realWorldPos.x, realWorldPos.y, 10f);
    }

}

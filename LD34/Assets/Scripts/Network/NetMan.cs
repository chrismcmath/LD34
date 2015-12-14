using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetMan : NetworkManager {
    private int _PlayerCount = 0;
    List<NetworkConnection> _Conns = new List<NetworkConnection>();

	public override void OnServerConnect(NetworkConnection conn) {
        NetworkServer.SetClientReady(conn);
        _PlayerCount++;
        _Conns.Add(conn);
		Debug.LogFormat("OnPlayerConnected, count: {0}, conn: {1}", _PlayerCount, conn.address);

        if (_PlayerCount == 3) {
            ServerChangeScene("Test");
        }
	}
}

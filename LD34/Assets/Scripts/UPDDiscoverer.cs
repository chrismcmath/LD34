using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;
using System.Text;

public class UPDDiscoverer : MonoBehaviour {
    public UdpClient DiscoveryBroadcaster;
    public UdpClient DiscoveryListener;
    public IPEndPoint ListenIPGroup;
    public int DiscoveyBroadcastPort = 3001;
    public int DiscoveryListenPort = 19784;

    public UDPConnectionRequest _Request;

    public Text Text;
    private bool _StartHost = false;
    private List<string> _DiscoveredIPs = new List<string>();
    private string IPString;
    private string AppendString = string.Empty;

    public void Start() {
        _Request = gameObject.AddComponent<UDPConnectionRequest>();
        _Request.Text = Text;
        IPString = Network.player.ipAddress;

        Broadcast();
        Listen();
    }

    public void Update() {
        if (AppendString != string.Empty) {
            Text.text += AppendString + "\n";
            AppendString = string.Empty;
        }

        if (_StartHost) {
            _StartHost = false;
            GetComponent<NetworkManager>().StartHost();
            StepController.ThisPlayer = StepController.Player.TWO;
            _Request.SendData();
        }
    }

    public void Broadcast() {
        bool gotClient = false;
        while (!gotClient) {
            try {
                DiscoveryBroadcaster = new UdpClient(DiscoveyBroadcastPort, AddressFamily.InterNetwork);
                gotClient = true;
                //Debug.Log("[DISCOVERY] Connected with localPort " + DiscoveyBroadcastPort);
            } catch(SocketException) {
                //Debug.Log("[DISCOVERY] Couldn't use port " + DiscoveyBroadcastPort + ", " + e.Message);
                DiscoveyBroadcastPort++;
            }
        }

        IPEndPoint groupEP = new IPEndPoint(IPAddress.Broadcast, DiscoveryListenPort);
        DiscoveryBroadcaster.Connect(groupEP);

        InvokeRepeating("SendData", 0, 5f);
    }

    public void StopBroadcast()
	{
        Debug.Log("[DISCOVERY] StopBroadcastingPresence");
        CancelInvoke();
    }

    public void SendData () {
        if (!NetworkClient.active && !NetworkServer.active) {
            Debug.Log("[DISCOVERY] sending " + IPString);
            DiscoveryBroadcaster.Send(Encoding.ASCII.GetBytes(IPString), IPString.Length);
        }
    }

    public void Listen() {
        try {
            if (DiscoveryListener == null) {
                DiscoveryListener = new UdpClient(DiscoveryListenPort);
                DiscoveryListener.BeginReceive(new AsyncCallback(ReceiveData), null);
            }
        } catch(SocketException e) {
            Debug.Log(e.Message);
        }
    }

    private void ReceiveData(IAsyncResult result) {
        Debug.Log("[DISCOVERY] Data Received");
        ListenIPGroup = new IPEndPoint(IPAddress.Any, DiscoveryListenPort);
        byte[] received;
        if (DiscoveryListener != null) {
            received = DiscoveryListener.EndReceive(result, ref ListenIPGroup);
        } else {
            return;
        }
        DiscoveryListener.BeginReceive(new AsyncCallback(ReceiveData), null);
        string receivedString = Encoding.ASCII.GetString(received);

        if (!_DiscoveredIPs.Contains(receivedString) &&
                receivedString != IPString) {
            Debug.Log("[DISCOVERY] found " + receivedString + ", _DiscoveredIPs count now " + _DiscoveredIPs.Count);
            AppendString = "[DISCOVERY] found " + receivedString + ", _DiscoveredIPs count now " + _DiscoveredIPs.Count;

            _StartHost = true;
            _DiscoveredIPs.Add(receivedString);
            return;
        }

        AppendString = string.Format("Appended: {0}, received: {1}, this: {2}", _DiscoveredIPs.Contains(receivedString), receivedString, IPString);
        AppendString += " : Ignored ";
    }
}

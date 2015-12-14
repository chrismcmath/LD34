using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPConnectionRequest : MonoBehaviour {
    public UdpClient RequestBroadcaster;
    public UdpClient RequestListener;
    public IPEndPoint ListenIPGroup;
    public int DiscoveyBroadcastPort = 4001;
    public int RequestListenPort = 29784;

    public Text Text;
    private List<string> _DiscoveredIPs = new List<string>();
    private string IPString;
    private string AppendString = string.Empty;

    private bool _StartClient = false;
    private string _HostIP;

    public void Start() {
        IPString = Network.player.ipAddress;

        Setup();
        Listen();
    }

    public void Update() {
        if (AppendString != string.Empty) {
            Text.text += AppendString + "\n";
            AppendString = string.Empty;
        }

        if (_StartClient) {
            GetComponent<NetworkManager>().networkAddress = _HostIP;
            GetComponent<NetworkManager>().StartClient();
            StepController.ThisPlayer = StepController.Player.TWO;
            _StartClient = false;
        }
    }

    public void Setup() {
        bool gotClient = false;
        while (!gotClient) {
            try {
                RequestBroadcaster = new UdpClient(DiscoveyBroadcastPort, AddressFamily.InterNetwork);
                gotClient = true;
                //Debug.Log("[REQUEST] Connected with localPort " + DiscoveyBroadcastPort);
            } catch(SocketException) {
                //Debug.Log("[REQUEST] Couldn't use port " + DiscoveyBroadcastPort + ", " + e.Message);
                DiscoveyBroadcastPort++;
            }
        }

        IPEndPoint groupEP = new IPEndPoint(IPAddress.Broadcast, RequestListenPort);
        RequestBroadcaster.Connect(groupEP);
    }

    public void StopBroadcast()
	{
        Debug.Log("[REQUEST] StopBroadcastingPresence");
        CancelInvoke();
    }

    public void SendData () {
        Debug.Log("[REQUEST] sending " + IPString);
        RequestBroadcaster.Send(Encoding.ASCII.GetBytes(IPString), IPString.Length);
    }

    public void Listen() {
        try {
            if (RequestListener == null) {
                RequestListener = new UdpClient(RequestListenPort);
                RequestListener.BeginReceive(new AsyncCallback(ReceiveData), null);
            }
        } catch(SocketException e) {
            Debug.Log(e.Message);
        }
    }

    private void ReceiveData(IAsyncResult result) {
        Debug.Log("[REQUEST] Data Received");
        ListenIPGroup = new IPEndPoint(IPAddress.Any, RequestListenPort);
        byte[] received;
        if (RequestListener != null) {
            received = RequestListener.EndReceive(result, ref ListenIPGroup);
        } else {
            return;
        }
        RequestListener.BeginReceive(new AsyncCallback(ReceiveData), null);
        string receivedString = Encoding.ASCII.GetString(received);

        if (!_DiscoveredIPs.Contains(receivedString) &&
                receivedString != IPString) {
            Debug.Log("[REQUEST] found " + receivedString + ", _DiscoveredIPs count now " + _DiscoveredIPs.Count);
            AppendString = "[REQUEST] found " + receivedString + ", _DiscoveredIPs count now " + _DiscoveredIPs.Count;
            
            _HostIP = receivedString;
            _StartClient = true;
            return;
        }

        AppendString = string.Format("Appended: {0}, received: {1}, this: {2}", _DiscoveredIPs.Contains(receivedString), receivedString, IPString);
        AppendString += " : Ignored ";
    }
}

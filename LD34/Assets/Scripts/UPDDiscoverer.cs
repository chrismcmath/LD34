using UnityEngine;
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

    public Transform HostList = null;
    private List<string> _DiscoveredIPs = new List<string>();
    private bool _RebuildHostList = false;

    public void Start() {
        Broadcast();
        Listen();
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
        Debug.LogFormat("try send data");
        string customMessage = Network.player.ipAddress;

        if (customMessage != "") {
            Debug.Log("[DISCOVERY] sending " + customMessage);
            DiscoveryBroadcaster.Send(Encoding.ASCII.GetBytes(customMessage), customMessage.Length);
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
                receivedString != Network.player.ipAddress) {
            Debug.Log("[DISCOVERY] found " + receivedString + ", _DiscoveredIPs count now " + _DiscoveredIPs.Count);
            _DiscoveredIPs.Add(receivedString);
            _RebuildHostList = true;
        }
    }

    public void Update() {
        if (_RebuildHostList) {
            RebuildHostList();
            _RebuildHostList = false;
        }
    }

    private void RebuildHostList() {
		if (HostList == null)
						return;
        foreach (Transform child in HostList) {
            GameObject.Destroy(child.gameObject);
        }	

        int i = 0;
        foreach (string ip in _DiscoveredIPs)
		{
			if(ip != Network.player.ipAddress)
			{
            	GameObject hostIP = (GameObject) Instantiate(Resources.Load("UI/host_ip"));
            	RectTransform rectTransform = hostIP.transform as RectTransform;
            	rectTransform.SetParent (HostList);
            	rectTransform.localPosition = new Vector3(0f, rectTransform.rect.height * i, 0f);
            	Text text = hostIP.GetComponentInChildren<Text>();
            	text.text = ip;
            	i++;
			}
        }
    }
}

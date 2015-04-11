using UnityEngine;
using System.Collections;

public class NetworkMenu : MonoBehaviour
{
	public string host = "127.0.0.1";
	public int port = 9999;
	public bool connected = false;

	private void OnGUI()
	{
		if (connected)
		{
			if (Network.isServer)
				GUILayout.Label("Connections: " + Network.connections.Length);

			return;
		}

		if (GUILayout.Button("Host"))
		{
			NetworkConnectionError error = Network.InitializeServer(32, port, true);
			if (error != NetworkConnectionError.NoError)
			{
				Debug.LogError(error.ToString());
				return;
			}

			connected = true;
		}

		if (GUILayout.Button("Connect"))
		{
			NetworkConnectionError error = Network.Connect(host, port);
			if (error != NetworkConnectionError.NoError)
			{
				Debug.LogError(error.ToString());
				return;
			}

			connected = true;
		}
	}
}
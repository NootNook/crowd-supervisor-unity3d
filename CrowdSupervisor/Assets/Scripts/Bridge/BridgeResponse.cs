using System;
using System.Threading;

using UnityEngine;

using NetMQ;
using NetMQ.Sockets;

public class BridgeResponse : MonoBehaviour
{
    private Thread networkThread;
    private CancellationTokenSource cancellationTokenSource;

    void Awake()
    {
        cancellationTokenSource = new CancellationTokenSource();

        // Start the network thread
        networkThread = new Thread(waitResponse);
        networkThread.Start(cancellationTokenSource.Token);
    }

    private void waitResponse(object obj)
    {
        CancellationToken token = (CancellationToken)obj;

        using (var responseSocket = new ResponseSocket("@tcp://*:5559"))
        {
            while (!token.IsCancellationRequested)
            {
                bool success = responseSocket.TryReceiveFrameString(TimeSpan.FromMilliseconds(100), out string message);
                
                if (success)
                {
                    Debug.Log("MESSAGE RECU : " + message);
                    responseSocket.SendFrame("OK SERVER !");
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        cancellationTokenSource.Cancel();
        networkThread.Join();
    }
}

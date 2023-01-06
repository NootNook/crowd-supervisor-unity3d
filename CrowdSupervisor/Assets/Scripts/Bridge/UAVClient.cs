using System;
using System.Threading.Tasks;

using UnityEngine;

using NetMQ;
using NetMQ.Sockets;

public class UAVClient
{
    public static void sendDataToBridge(int id, byte[] data)
    {
        string request = serializeData(id, data);
        Task.Run(() => runClient(request));
    }

    private static void runClient(string data)
    {
        using (var socket = new DealerSocket())
        {
            socket.Options.Correlate = true;
            socket.Options.Linger = TimeSpan.FromMilliseconds(100);
            socket.Connect("tcp://localhost:5555");
            socket.SendFrame(data);
            
            string response = socket.ReceiveFrameString();
            if (response != "OK")
            {
                Debug.LogError("WARNING RESPONSE SERVER : " + response);
            }
        }
    }

    private static string serializeData(int id, byte[] data)
    {
        DataRequest newRequest = new DataRequest
        {
            Id = id,
            Data = data
        };

        return JsonUtility.ToJson(newRequest);
    }
}

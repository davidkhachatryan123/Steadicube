using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class CubePositioner : MonoBehaviour
{
    [SerializeField]
    private int localPort;

    private UdpClient receiver;
    private IPEndPoint remoteIp;

    float x = 0;
    float y = 0;
    float z = 0;

    void Start()
    {
        receiver = new UdpClient(localPort);
        remoteIp = null;

        Thread thread = new Thread(new ThreadStart(ReceiveMessages));
        thread.Start();
    }

    void Update()
    {
        transform.position = new Vector3(x, z, y);
    }

    private void ReceiveMessages()
    {
        try
        {
            while (true)
            {
                byte[] data = receiver.Receive(ref remoteIp);

                string message = Encoding.Unicode.GetString(data);


                if (message.IndexOf('x') != -1)
                {
                    x = float.Parse(message.Substring(2, message.Length - 2));

                    //Debug.Log(x);
                }
                else if (message.IndexOf('y') != -1)
                {
                    y = float.Parse(message.Substring(2, message.Length - 2));

                    //Debug.Log(y);
                }
                else if (message.IndexOf('z') != -1)
                {
                    z = float.Parse(message.Substring(2, message.Length - 2));

                    //Debug.Log(z);
                }

            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void OnDestroy()
    {
        receiver.Close();
    }
}

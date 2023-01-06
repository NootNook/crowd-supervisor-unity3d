using System;
using System.Diagnostics;
using System.IO;

using UnityEngine;

public class InstantiateBridge : MonoBehaviour
{
    public bool isOn;
    public bool activateLogs;
    private Process m_process;

    void Awake()
    {
        if(isOn)
        {
            string fileName = "python3";
            string arguments = string.Format("-u {0}/Scripts/Bridge/Python/ServerZMQ.py", Application.dataPath);

            m_process = new Process();

            m_process.StartInfo.FileName = fileName;
            m_process.StartInfo.Arguments = arguments;
            
            if (activateLogs)
            {
                m_process.StartInfo.UseShellExecute = false;
                m_process.StartInfo.RedirectStandardOutput = true;

                m_process.OutputDataReceived += Process_OutputDataReceived;
            }

            m_process.Start();

            if (activateLogs)
                m_process.BeginOutputReadLine();
        }
    }

    static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log(e.Data);
    }

    void OnApplicationQuit()
    {
        if (isOn)
        {
            m_process.Kill();
            m_process.WaitForExit(); //m_process.WaitForExitAsync();
        }
    } 
}
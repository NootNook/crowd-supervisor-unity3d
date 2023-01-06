using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Unity.Collections;

using UnityEngine;
using UnityEngine.Rendering;

public class UAV
{
    private readonly int WIDTH = 1280;
    private readonly int HEIGHT = 720;

    private int m_id;
    private GameObject m_gameObject;
    private Camera m_camera;

    private RenderTexture m_renderTexture;

    public UAV(int id, GameObject gameObject) {
        this.m_id = id;
        this.m_gameObject = gameObject;
        this.m_camera = this.m_gameObject.GetComponentInChildren(typeof(Camera)) as Camera;

        this.m_renderTexture = new RenderTexture(WIDTH, HEIGHT, 24, RenderTextureFormat.ARGB32);
        this.m_renderTexture.name = string.Format("UAV_{0}_renderTexture", id);

        this.m_camera.targetTexture = this.m_renderTexture;
        this.m_camera.name = string.Format("Camera_{0}", m_id);
    }

    public byte[] getImageData() {
        Texture2D texture = new Texture2D(this.m_renderTexture.width, this.m_renderTexture.height);
        RenderTexture.active = this.m_renderTexture;
        texture.ReadPixels(new Rect(0, 0, this.m_renderTexture.width, this.m_renderTexture.height), 0, 0);
        texture.Apply();

        return texture.EncodeToPNG();
    }

    public RenderTexture getRenderTexture() {
        return this.m_renderTexture;
    }

    public void takePicture() {
        byte[] imageData = this.getImageData();
        string filename = string.Format("output_{0}.jpg", this.m_id);
        File.WriteAllBytes(filename, imageData);

        Debug.Log(filename + " is done !");
    }

    public void sendDataToBridge()
    {
        AsyncGPUReadback.Request(m_renderTexture, 0, TextureFormat.RGBA32, ReadbackCompleted);
    }

    void ReadbackCompleted(AsyncGPUReadbackRequest request)
    {
        using (var imageBytes = request.GetData<byte>())
        {
            byte[] imageBytesArray = imageBytes.ToArray();
            byte[] data = ImageConversion.EncodeArrayToJPG(
                imageBytesArray, 
                m_renderTexture.graphicsFormat,
                (uint) m_renderTexture.width,
                (uint) m_renderTexture.height
            );
            //Task.Run(() => UAVClient.sendDataToBridge(m_id, data));
            Thread t = new Thread(() => UAVClient.sendDataToBridge(m_id, data));
            t.Start();
        }
    }
}

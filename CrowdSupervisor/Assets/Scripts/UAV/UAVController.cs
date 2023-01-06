using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVController : MonoBehaviour
{
    private GameObject[] m_objectUAV;
    private Dictionary<int, UAV> m_UAVs = new Dictionary<int, UAV>();

    private int m_id = 0;
    private bool m_isLive = false;

    void Awake()
    {
        m_objectUAV = GameObject.FindGameObjectsWithTag("Drone");

        foreach(GameObject uav in m_objectUAV) {
            m_UAVs.Add(m_id, new UAV(m_id, uav));
            m_id++;
        }

        m_isLive = true;
    }

    void Start()
    {
        // Starting in 2 seconds.
        // Every 3 seconds...
        //InvokeRepeating("RequestsBridge", 1.0f, 1.0f);
    }

    public void RequestsBridge()
    {
        StartCoroutine(RetrieveImages());
    }

    private IEnumerator RetrieveImages()
    {
        foreach (UAV uav in m_UAVs.Values)
        {
            yield return new WaitForEndOfFrame();

            uav.sendDataToBridge();

            yield return new WaitForSeconds(.1f);

        }
    }

    // ------------------

    public UAV getUAVByID(int id) {
        UAV temp_uav;
        bool hasValue = m_UAVs.TryGetValue(id, out temp_uav);
        if(hasValue) {
            return temp_uav;
        } else {
            Debug.LogError("[UAVController] ID not found in dictionnary");
            return null;
        }
    }

    public int sizeSwarm() {
        return m_UAVs.Count;
    }

    public bool isReady() {
        return m_isLive;
    }

}

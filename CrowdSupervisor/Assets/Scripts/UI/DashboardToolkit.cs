using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class DashboardToolkit : MonoBehaviour
{
    private UIControllerToolkit m_uiController;
    private DetectPertubator m_detectPertubator;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        UAVController uavController = GameObject.Find("Drones").GetComponent<UAVController>();
        m_detectPertubator = GameObject.Find("Core").GetComponent<DetectPertubator>();
        m_uiController = new UIControllerToolkit(root, uavController);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            m_uiController.nextScene();
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            m_uiController.previousScene();
        }

        HashSet<int> camera = m_detectPertubator.getVisiblePertubator();

        if(camera.Count == 0)
        {
            m_uiController.hideRedAlert();
            m_uiController.clearCameraNames();
        }
        else
        {
            m_uiController.clearCameraNames();
            m_uiController.addRedAlert();
            foreach (int id in camera)
            {
                string name = string.Format("Camera {0}", id);
                m_uiController.addCameraNames(name);
            }
        }
    }
}

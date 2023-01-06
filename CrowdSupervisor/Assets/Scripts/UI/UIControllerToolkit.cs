using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControllerToolkit
{
    private UAVController m_uavController;
    private SceneSwitcherToolkit m_screenSwitcher;
    private VisualElement m_root;
    private VisualElement m_rootVisual;
    private VisualElement m_textContainer;

    private HashSet<VisualElement> frames = new HashSet<VisualElement>();

    public UIControllerToolkit(VisualElement root, UAVController uavController)
    {
        m_uavController = uavController;
        m_root = root;

        m_rootVisual = m_root.Q<VisualElement>("rootVisual");
        m_rootVisual.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);

        m_screenSwitcher = new SceneSwitcherToolkit(m_rootVisual, m_uavController);

        m_root.Q<Button>("previousButton").clicked += handlePreviousButton;
        m_root.Q<Button>("nextButton").clicked += handleNextButton;

        m_textContainer = m_rootVisual.Q<VisualElement>("textContainer");
    }

    ////testRender.RegisterCallback<MouseDownEvent>(MyCallback, TrickleDown.TrickleDown);

    public void nextScene()
    {
        m_screenSwitcher.nextScene();
    }

    public void previousScene()
    {
        m_screenSwitcher.previousScene();
    }

    public void addRedAlert()
    {
        m_textContainer.style.display = DisplayStyle.Flex;
    }

    public void hideRedAlert()
    {
        m_textContainer.style.display = DisplayStyle.None;
    }

    public void addCameraNames(string text)
    {
        TextElement textElement = new TextElement();
        textElement.style.color = new StyleColor(Color.white);
        textElement.style.alignSelf = Align.Center;
        textElement.text = text;

        m_textContainer.Insert(m_textContainer.childCount, textElement);
    }

    public void clearCameraNames()
    {
        int size = m_textContainer.childCount;
        for (int i = 1; i < size; i++)
        {
            m_textContainer.RemoveAt(1);
        }
    }

    // --------------------------

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        (float widthOld, float heightOld) = (evt.oldRect.size.x, evt.oldRect.size.y);

        float width = evt.newRect.size.x;
        float height = evt.newRect.size.y;

        m_screenSwitcher.updateSizeScreen(width, height);

        if (widthOld + heightOld == 0f) {
            m_screenSwitcher.mainScene();
        } else {
            m_screenSwitcher.updateSizeFrames();
        }
    }

    private void MyCallback(MouseDownEvent evt)
    {
        Debug.Log("Click");
    }

    private void handlePreviousButton() 
    {
        m_screenSwitcher.previousScene();
    }

    private void handleNextButton() 
    {
        m_screenSwitcher.nextScene();
    }
}

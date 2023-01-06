using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class SceneSwitcherToolkit
{
    private UAVController m_uavController;
    private int m_sizeSwarm;

    private VisualElement m_rootVisual;
    private int m_currentState = 0;

    private int m_squareSize;
    private int m_nbrState;
    private float m_widthScreen;
    private float m_heightScreen;

    private List<VisualElement> m_frames = new List<VisualElement>();

    public SceneSwitcherToolkit(VisualElement rootVisual, UAVController uavController) {
        m_uavController = uavController;
        m_sizeSwarm = m_uavController.sizeSwarm();
        m_nbrState = m_sizeSwarm;
        m_squareSize = getSquareSize(m_sizeSwarm);
        m_rootVisual = rootVisual;
    }

    private int getSquareSize(int sizeSwarm)
    {
        int i = 1;
        while (i < 10)
        {
            int limit = i*i;
            if (sizeSwarm <= limit )
                return i;

            i++;
        }

        return 0;
    }

    private void stateScene(int currentId) {
        clearScene();

        if(m_currentState >= m_nbrState) {
            m_currentState = 0;
        } else if (m_currentState < 0) {
            m_currentState = m_nbrState - 1;
        }

        if (m_currentState == 0)
        {
            mainScene();
        } 
        else
        {
            fullScene(m_currentState - 1);
        }
    }

    public void mainScene() {
        for(int id = 0; id < m_sizeSwarm; id++)
        {
            string name = string.Format("frame_mainScene_UAV_{0}", id);
            (float width, float height) = (m_widthScreen / m_squareSize, m_heightScreen / m_squareSize);
            addFrame(id, name, (width, height));

            VisualElement newFrame = m_frames[m_frames.Count - 1];
            newFrame.RegisterCallback<MouseDownEvent>(handleMainScene, TrickleDown.TrickleDown);
        } 
    }

    private void handleMainScene(MouseDownEvent evt)
    {
        VisualElement targetFrame = evt.currentTarget as VisualElement;
        string[] nameSplit = targetFrame.name.Split('_');
        int id = Int32.Parse(nameSplit[nameSplit.Length - 1]);

        setScene(id);
    }

    public void fullScene(int id)
    {
        string name = string.Format("frame_fullScene_UAV_{0}", id);
        addFrame(id, name, (m_widthScreen, m_heightScreen));
    }

    public void previousScene() {
        m_currentState--;
        stateScene(m_currentState);
    }

    public void nextScene() {
        m_currentState++;
        stateScene(m_currentState);
    }

    public void setScene(int id)
    {
        m_currentState = id + 1;
        stateScene(m_currentState);
    }

    public void clearScene() {
        foreach(VisualElement child in m_frames) {
            m_rootVisual.Remove(child);
        }

        m_frames.Clear();
    }

    public void updateSizeScreen(float width, float height)
    {
        m_widthScreen = width;
        m_heightScreen = height;
    }

    public void updateSizeFrames()
    {
        if (m_currentState == 0)
        {
            foreach(VisualElement frame in m_frames)
            {
                frame.style.width = new StyleLength((m_widthScreen /  m_squareSize) - 10f);
                frame.style.height = new StyleLength((m_heightScreen /  m_squareSize) - 10f);
            }
        }
        else 
        {
            m_frames[0].style.width = new StyleLength(m_widthScreen);
            m_frames[0].style.height = new StyleLength(m_heightScreen);
        }
    }


    private void addFrame(int id, string name, (float, float) sizeFrame)
    {
        RenderTexture renderFrame = getRenderFrame(id);

        VisualElement newFrame = new VisualElement();
        newFrame.name = name;
        newFrame.style.width = new StyleLength(sizeFrame.Item1- 10f);
        newFrame.style.height = new StyleLength(sizeFrame.Item2 - 10f);
        newFrame.style.backgroundImage = new StyleBackground(Background.FromRenderTexture(renderFrame));

        newFrame.style.marginBottom = new StyleLength(5.0f);
        newFrame.style.marginLeft = new StyleLength(5.0f);
        newFrame.style.marginRight = new StyleLength(5.0f);
        newFrame.style.marginTop = new StyleLength(5.0f);

        // Text frame

        TextElement textElement = new TextElement();
        textElement.style.color = new StyleColor(Color.black);
        textElement.style.unityFontStyleAndWeight = FontStyle.Bold;
        textElement.style.backgroundColor = new StyleColor(new Color(0.984f, 0.76f, 0.32f));
        textElement.text = string.Format("Camera {0}", id);

        textElement.style.position = Position.Absolute;
        textElement.style.top = new StyleLength(0.0f);
        textElement.style.left = new StyleLength(0.0f);

        textElement.style.marginTop = new StyleLength(2.0f);
        textElement.style.marginBottom = new StyleLength(2.0f);
        textElement.style.marginLeft = new StyleLength(2.0f);
        textElement.style.marginRight = new StyleLength(2.0f);

        textElement.style.paddingTop = new StyleLength(2.0f);
        textElement.style.paddingBottom = new StyleLength(2.0f);
        textElement.style.paddingLeft = new StyleLength(2.0f);
        textElement.style.paddingRight = new StyleLength(2.0f);

        newFrame.Add(textElement);
    
        m_rootVisual.Insert(m_frames.Count, newFrame);
        m_frames.Add(newFrame);
    }

    private RenderTexture getRenderFrame(int id)
    {
        return m_uavController.getUAVByID(id).getRenderTexture();
    }
}

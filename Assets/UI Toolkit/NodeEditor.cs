using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/NodeEditor")]
    public static void ShowExample()
    {
        NodeEditor wnd = GetWindow<NodeEditor>();
        wnd.titleContent = new GUIContent("NodeEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        
        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}

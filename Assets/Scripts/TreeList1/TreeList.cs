using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeList : MonoBehaviour
{
    // �m�[�h��Prefab
    public GameObject nodePrefab;

    // �\������m�[�h�̃��X�g
    private List<TreeNode> nodes = new List<TreeNode>();

    // �I�u�W�F�N�g�v�[��
    private Stack<GameObject> objectPool = new Stack<GameObject>();

    void Start()
    {
        // ���[�g�m�[�h���쐬����
        var rootNode = new TreeNode("Root", null);
        nodes.Add(rootNode);
        var c1 = new TreeNode("c1", rootNode);
        var c1_1 = new TreeNode("c1_1", c1);
        var c2 = new TreeNode("c2", rootNode);
        var c3 = new TreeNode("c3", rootNode);

        // �I�u�W�F�N�g�v�[��������������
        for (int i = 0; i < 10; i++)
        {
            var obj = Instantiate(nodePrefab, transform);
            obj.SetActive(false);
            objectPool.Push(obj);
        }

        // �m�[�h��\������
        ShowNode(rootNode, 0);
    }

    // �m�[�h��\������
    void ShowNode(TreeNode node, int depth)
    {
        // �m�[�h��GameObject���擾����
        var obj = GetObjectFromPool();
        obj.SetActive(true);

        // �m�[�h��UI�v�f���X�V����
        var text = obj.GetComponentInChildren<Text>();
        text.text = new string('-', depth) + node.name;

        // �m�[�h�̎q��\������
        foreach (var child in node.children)
        {
            ShowNode(child, depth + 1);
        }
    }

    // �I�u�W�F�N�g�v�[������GameObject���擾����
    GameObject GetObjectFromPool()
    {
        if (objectPool.Count == 0)
        {
            // �I�u�W�F�N�g�v�[���ɗ]�T���Ȃ��ꍇ�́A�V������������
            var obj = Instantiate(nodePrefab, transform);
            obj.SetActive(false);
            objectPool.Push(obj);
        }

        return objectPool.Pop();
    }

    // TreeNode�N���X
    class TreeNode
    {
        public string name;
        public TreeNode parent;
        public List<TreeNode> children = new List<TreeNode>();

        public TreeNode(string name, TreeNode parent)
        {
            this.name = name;
            this.parent = parent;

            if (parent != null)
            {
                parent.children.Add(this);
            }
        }
    }
}

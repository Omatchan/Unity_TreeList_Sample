using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeList : MonoBehaviour
{
    // ノードのPrefab
    public GameObject nodePrefab;

    // 表示するノードのリスト
    private List<TreeNode> nodes = new List<TreeNode>();

    // オブジェクトプール
    private Stack<GameObject> objectPool = new Stack<GameObject>();

    void Start()
    {
        // ルートノードを作成する
        var rootNode = new TreeNode("Root", null);
        nodes.Add(rootNode);
        var c1 = new TreeNode("c1", rootNode);
        var c1_1 = new TreeNode("c1_1", c1);
        var c2 = new TreeNode("c2", rootNode);
        var c3 = new TreeNode("c3", rootNode);

        // オブジェクトプールを初期化する
        for (int i = 0; i < 10; i++)
        {
            var obj = Instantiate(nodePrefab, transform);
            obj.SetActive(false);
            objectPool.Push(obj);
        }

        // ノードを表示する
        ShowNode(rootNode, 0);
    }

    // ノードを表示する
    void ShowNode(TreeNode node, int depth)
    {
        // ノードのGameObjectを取得する
        var obj = GetObjectFromPool();
        obj.SetActive(true);

        // ノードのUI要素を更新する
        var text = obj.GetComponentInChildren<Text>();
        text.text = new string('-', depth) + node.name;

        // ノードの子を表示する
        foreach (var child in node.children)
        {
            ShowNode(child, depth + 1);
        }
    }

    // オブジェクトプールからGameObjectを取得する
    GameObject GetObjectFromPool()
    {
        if (objectPool.Count == 0)
        {
            // オブジェクトプールに余裕がない場合は、新しく生成する
            var obj = Instantiate(nodePrefab, transform);
            obj.SetActive(false);
            objectPool.Push(obj);
        }

        return objectPool.Pop();
    }

    // TreeNodeクラス
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

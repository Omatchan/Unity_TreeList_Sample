using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeView : MonoBehaviour
{
    [SerializeField] private TreeViewItem rootItem;

    private void Start()
    {
        var data = new TreeViewItemData
        {
            Text = "Root",
            layer = 0,
            Children = new List<TreeViewItemData>
            {
                new TreeViewItemData {
                    Text = "1", layer = 1,
                    Children = new List<TreeViewItemData>
                    {
                        new TreeViewItemData { Text = "1-1", layer = 2 },
                        new TreeViewItemData { Text = "1-2", layer = 2 },
                        new TreeViewItemData { Text = "1-3", layer = 2 },
                        new TreeViewItemData { Text = "1-4", layer = 2 },
                        new TreeViewItemData { Text = "1-5", layer = 2 },
                    }
                },
                new TreeViewItemData {
                    Text = "2", layer = 1,
                    Children = new List<TreeViewItemData>
                    {
                        new TreeViewItemData { Text = "2-1", layer = 2 },
                        new TreeViewItemData { Text = "2-2", layer = 2 },
                        new TreeViewItemData { Text = "2-3", layer = 2 },
                    }
                },
                new TreeViewItemData
                {
                    Text = "3", layer = 1,
                    Children = new List<TreeViewItemData>
                    {
                        new TreeViewItemData { Text = "3-1", layer = 2 },
                        new TreeViewItemData { Text = "3-2", layer = 2 },
                    }
                },
                new TreeViewItemData {
                    Text = "4", layer = 1,
                    Children = new List<TreeViewItemData>
                    {
                        new TreeViewItemData {
                            Text = "4-1", layer = 2,
                            Children = new List<TreeViewItemData>
                            {
                                new TreeViewItemData { Text = "4-1-1", layer = 3 },
                                new TreeViewItemData { Text = "4-1-2", layer = 3 },
                            }
                        },
                        new TreeViewItemData {
                            Text = "4-2", layer = 2,
                        },
                        new TreeViewItemData {
                            Text = "4-3", layer = 2,
                            Children = new List<TreeViewItemData>
                            {
                                new TreeViewItemData { Text = "4-3-1", layer = 3 },
                                new TreeViewItemData { Text = "4-3-2", layer = 3 },
                                new TreeViewItemData { Text = "4-3-3", layer = 3 },
                            }
                        }
                    }
                },
                new TreeViewItemData {
                    Text = "5", layer = 1,
                },
                new TreeViewItemData
                {
                    Text = "6", layer = 1,
                    Children = new List<TreeViewItemData>
                    {
                        new TreeViewItemData { Text = "6-1", layer = 2 },
                        new TreeViewItemData { Text = "6-2", layer = 2 },
                        new TreeViewItemData { Text = "6-3", layer = 2 },
                    }
                }
            }
        };

        rootItem.SetData(data);
    }
}

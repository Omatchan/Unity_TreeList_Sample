using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TreeViewItem : MonoBehaviour
{
    [SerializeField] private Button expandButton;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject expandImage;
    [SerializeField] private GameObject collapseImage;
    [SerializeField] private Text text;
    [SerializeField] private GameObject childrenContainer;

    private List<TreeViewItem> children = new List<TreeViewItem>();

    private bool expanded = true;

    private void Start()
    {
        expandButton.onClick.AddListener(() =>
        {
            expanded = !expanded;
            UpdateChildren();
        });
        expandImage.GetComponent<Button>().onClick.AddListener(() =>
        {
            expanded = true;
            UpdateChildren();
        });
        collapseImage.GetComponent<Button>().onClick.AddListener(() =>
        {
            expanded = false;
            UpdateChildren();
        });
    }

    public void SetData(TreeViewItemData data)
    {
        if (data == null)
            return;

        if (data.Children == null || data.Children.Count < 1)
        {
            image.SetActive(false);
            expandButton.GetComponent<RectTransform>().sizeDelta =
            new Vector2(data.layer * 20 + 20, expandButton.GetComponent<RectTransform>().sizeDelta.y);
        }
        else
        {
            image.SetActive(true);
            childrenContainer.SetActive(expanded);
            expandImage.SetActive(!expanded);
            collapseImage.SetActive(expanded);
            expandButton.GetComponent<RectTransform>().sizeDelta =
                new Vector2(data.layer * 20, expandButton.GetComponent<RectTransform>().sizeDelta.y);
        }

        this.SetText(data.Text);
        this.SetChildren(data.Children);
    }


    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void SetChildren(List<TreeViewItemData> childrenData)
    {
        if (childrenData == null)
            return;

        foreach (var childData in childrenData)
        {
            var child = ObjectPoolManager.Instance.GetObject<TreeViewItem>("TreeViewItem");
            child.SetData(childData);
            child.transform.SetParent(childrenContainer.transform, false);
            children.Add(child);
        }
    }

    public void UpdateChildren()
    {
        if (children == null || children.Count < 1)
        {
            image.SetActive(false);
        }
        else
        {
            image.SetActive(true);
            childrenContainer.SetActive(expanded);
            expandImage.SetActive(!expanded);
            collapseImage.SetActive(expanded);
            foreach (var child in children)
            {
                child.gameObject.SetActive(expanded);
                if (expanded)
                {
                    child.UpdateChildren();
                }
            }
        }
    }

    public void ReturnToPool()
    {
        foreach (var child in children)
        {
            child.ReturnToPool();
        }
        ObjectPoolManager.Instance.ReturnObject("TreeViewItem", this);
    }
}


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image ItemIcon;
    public TextMeshProUGUI StackText;

    [SerializeField] private Button _removeButton;
    [SerializeField] private InventoryManager _manager;

    private Item _currentItem;
    private int _stackCount = 0;

    [HideInInspector] public int SlotIndex;


    private void Start()
    {
        if (_removeButton != null)
        {
            _removeButton.gameObject.SetActive(false);
        }

        if (_removeButton != null)
        {
            _removeButton.onClick.AddListener(OnRemoveButtonClicked);
        }
    }

    public void RemoveButtonOff()
    {
        _removeButton?.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _manager.AllRemoveButtonsOff();
        if (_stackCount > 0)
        {
            bool isVisible = _removeButton.gameObject.activeSelf;
            _removeButton.gameObject.SetActive(!isVisible);
        }
    }

    public void OnRemoveButtonClicked()
    {
        if (_manager != null)
        {
            _manager.RemoveItem(SlotIndex);

            _removeButton.gameObject.SetActive(false);
        }
    }

    public void UpdateSlot(Item item, int count)
    {
        _currentItem = item;
        _stackCount = count;

        ItemIcon.sprite = _currentItem.Icon;
        ItemIcon.enabled = true;

        if (_stackCount > 1)
        {
            StackText.text = _stackCount.ToString();
            StackText.enabled = true;
        }
        else
        {
            StackText.enabled = false;
        }

        if (_removeButton != null) _removeButton.gameObject.SetActive(false);
    }

    public void ClearSlot()
    {
        _currentItem = null;
        _stackCount = 0;

        ItemIcon.sprite = null;
        ItemIcon.enabled = false;
        StackText.enabled = false;

        if (_removeButton != null) _removeButton.gameObject.SetActive(false);
    }
}

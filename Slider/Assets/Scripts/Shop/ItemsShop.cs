using Slicer.Shop.Events;
using Slicer.Tools;
using Slicer.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slicer.Shop
{
    public class ItemsShop : MonoBehaviour
    {
        [SerializeField]
        private ShopWindow window;

        [SerializeField]
        private string itemPath = "ShopItems/";

        private Dictionary<ItemTypes, DoublyLinkedList<ShopItem>> items;

        private int currentElementNumber;
        private ItemTypes currentType;

        private ShopItem currentElement;

        private int selectedKnife;
        private int selectedTable;

        private void Awake()
        {
            items = ResourcesLoader.ShopElementsLoad(itemPath);

            ShopEvents.ItemTypeChanged += ChangeCurrentType;

            window.NextElementSwitched += NextElement;
            window.BackElementSwitched += BackElement;
            window.ElementSelected += SelectItem;
        }

        private void ChangeCurrentType(ItemTypes types)
        {
            currentType = types;
            currentElementNumber = 0;
            UpdateItem(items[types].SelectedElement);
        }

        private void NextElement()
        {
            UpdateItem(items[currentType].NextElement());
        }

        private void BackElement()
        {
            UpdateItem(items[currentType].BackElement());
        }

        private void SelectItem()
        {
            items[currentType].Selection();
        }

        private void UpdateItem(ShopItem item)
        {
            currentElement = item;
            ShopEvents.ItemChanged.Call(currentElement, ItemStatus.Available);
        }

        public class DoublyLinkedList<T>
        {
            private List<T> list;

            public DoublyLinkedList()
            {
                list = new List<T>();
            }

            public int CurrentElementNumber { get; private set; }
            public T CurrentElement { get; private set; }
            public T SelectedElement { get; private set; }

            public void AddElement(T element)
            {
                list.Add(element);

                if(CurrentElement == null)
                {
                    CurrentElement = element;
                }

                if(SelectedElement == null)
                {
                    SelectedElement = element;
                }
            }

            public void Remove(T element)
            {
                list.Remove(element);
            }

            public T NextElement()
            {
                if (CurrentElementNumber >= list.Count - 1)
                {
                    CurrentElementNumber = 0;
                }
                else
                {
                    CurrentElementNumber++;
                }
                CurrentElement = list[CurrentElementNumber];
                return CurrentElement;
            }

            public void Selection()
            {
                SelectedElement = CurrentElement;
            }

            public T BackElement()
            {
                if (CurrentElementNumber <= 0)
                {
                    CurrentElementNumber = list.Count - 1;
                }
                else
                {
                    CurrentElementNumber--;
                }

                CurrentElement = list[CurrentElementNumber];
                return CurrentElement;
            }
        }
    }
}
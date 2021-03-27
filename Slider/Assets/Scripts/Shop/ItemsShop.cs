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
            UpdateItem(items[types].CurrentElement);
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
            
        }

        private void UpdateItem(ShopItem item)
        {
            currentElement = item;
            ShopEvents.ItemChanged.Call(currentElement, ItemStatus.Available);
        }

        public class DoublyLinkedList<T>
        {
            private List<T> list;
            private T currentElement;
            private int currentElementNumber;

            public DoublyLinkedList()
            {
                list = new List<T>();
            }

            public int CurrentElementNumber => currentElementNumber;
            public T CurrentElement => currentElement;

            public void AddElement(T element)
            {
                list.Add(element);

                if(currentElement == null)
                {
                    currentElement = element;
                }
            }

            public void Remove(T element)
            {
                list.Remove(element);
            }

            public T NextElement()
            {
                if (currentElementNumber >= list.Count - 1)
                {
                    currentElementNumber = 0;
                }
                else
                {
                    currentElementNumber++;
                }
                currentElement = list[currentElementNumber];
                return currentElement;
            }

            public T BackElement()
            {
                if (currentElementNumber <= 0)
                {
                    currentElementNumber = list.Count - 1;
                }
                else
                {
                    currentElementNumber--;
                }

                currentElement = list[currentElementNumber];
                return currentElement;
            }
        }
    }
}
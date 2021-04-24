using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slicer.Shop
{
    public partial class ItemsShop
    {
        public class DoublyLinkedList<T> where T : ScriptableObject
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

            public void ChangeSelectedElement(T element)
            {
                SelectedElement = element;
            }

            public void ChangeCurrentElement(T element)
            {
                CurrentElement = element;
                CurrentElementNumber = list.IndexOf(element);
            }

            public void Remove(T element)
            {
                list.Remove(element);
            }

            public T FindElementByID(int id)
            {
                return list.FirstOrDefault(x => x.GetInstanceID().Equals(id));
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
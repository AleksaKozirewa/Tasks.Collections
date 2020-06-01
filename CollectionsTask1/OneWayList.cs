using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit.Sdk;

namespace CollectionsTask1
{
    class OneWayList
    {
        OneWayListNode head = null;
        /// insert value to end of list
        public void Add(int value)
        {
            var node = new OneWayListNode(value);
            if (head == null)
            {
                // empty collection
                head = node;
            }
            else
            {
                // not empty collection
                OneWayListNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = node;
            }
        }
        public class OneWayListNode
        {
            public OneWayListNode(int value)
            {
                Value = value;
                Next = null;
            }
            public OneWayListNode(int value, OneWayListNode next)
            {
                Value = value;
                Next = next;
            }
            public int Value;
            public OneWayListNode Next;
        }

        public int Count
        {
            get;
            private set;
        }

        public bool Remove(int value)
        {
            OneWayListNode current = head;
            OneWayListNode previous = null;
            
            // 1: Пустой список: ничего не делать.
            // 2: Один элемент: установить Previous = null.
            // 3: Несколько элементов:
            //    a: Удаляемый элемент первый.
            //    b: Удаляемый элемент в середине или конце.

            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    /// Узел в середине или в конце.
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                    }
                    else
                    {
                        /// если удаляется первый элемент
                        /// переустанавливаем значение head
                        head = head.Next;
                    }
                    Count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            
            return false;
        }

        ///add value before {index} element
        public void AddAt(int value, int index)
        {
            var node = new OneWayListNode(value);
            OneWayListNode current = head;

            if (head != null)
            {
                if (index != 0)
                {
                    for (var i = 0; i < index - 1; i++)
                    {
                        if (current == null)
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        current = current.Next;

                    }

                    node.Next = current.Next;
                    current.Next = node;
                }

                node.Next = head;
            }

            if (index != 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            head = node;
        }

    }

}


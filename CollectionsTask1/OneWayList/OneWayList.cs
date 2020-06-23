using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Transactions;
using Xunit.Sdk;

namespace CollectionsTask1
{
    class OneWayList : IEnumerable<int>
    {
        OneWayListNode head = null;

        public OneWayList(int[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                Add(input[i]);
            }
        }

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

        public int Count { get; private set; }

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

        public void AddAt(int value, int index)
        {
            var node = new OneWayListNode(value);

            if (head != null && index != 0)
            {
                OneWayListNode current = head;

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

            else if (head == null && index != 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            else if (index == 0)
            {
                node.Next = head;
                head = node;
            }
        }

        public bool RemoveAt(int index)
        {
            OneWayListNode current = head;
            OneWayListNode previous = null;

            while (current != null)
            {
                if (index != 0)
                {
                    for (var i = 0; i < index; i++)
                    {
                        previous = current;

                        if (current == null || current.Next == null)
                        {
                            return false;
                        }

                        current = current.Next;
                    }

                    previous.Next = current.Next;
                    return true;
                }

                head = head.Next;
                return true;
            }

            return false;
        }

        public int GetElementByIndex(int index)
        {
            OneWayListNode current = head;

            while (current != null)
            {
                for (var i = 0; i < index; i++)
                {
                    if (current == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    current = current.Next;
                }

                return current.Value;
            }

            throw new ArgumentOutOfRangeException();
        }

        public int this[int index]
        {
            get
            {
                return GetElementByIndex(index);
            }

            set
            {
                AddAt(value, index);
                RemoveAt(index + 1);
            }
        }

        public void Sort()
        {
            if (head == null)
            {
                return;
            }

            OneWayListNode node1 = head;
            OneWayListNode node2 = head.Next;
            int temp;
            bool doubt = false;

            do
            {
                doubt = false;
                while (node1 != null && node1.Next != null)
                {

                    if (node2.Value < node1.Value)
                    {
                        doubt = true;
                        temp = node1.Value;
                        node1.Value = node2.Value;
                        node2.Value = temp;
                    }

                    node1 = node1.Next;
                    node2 = node2.Next;

                }

                node1 = head;
                node2 = node1.Next;

            } while (doubt);

        }

        public void Reverse()
        {
            if (head == null)
            {
                return;
            }

            OneWayListNode current = head;
            OneWayListNode previous = null;
            OneWayListNode next = null;

            while (current.Next != null)
            {

                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            current.Next = previous;
            head = current;
        }

        public int[] ConvertToArray()
        {
            OneWayListNode current = head;
            var countOfNodes = 1;

            if (current == null)
            {
                var arr = new int[0];
                return arr;
            }

            while (current.Next != null)
            {
                if (current != null)
                {
                    countOfNodes = countOfNodes + 1;
                    current = current.Next;
                }
            }

            var array = new int[countOfNodes];
            current = head;

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = current.Value;
                current = current.Next;
            }

            return array;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new OneWayListEnumerator(head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new OneWayListEnumerator(head);
        }

        private class OneWayListEnumerator : IEnumerator<int>
        {
            private OneWayList.OneWayListNode _current;
            private OneWayList.OneWayListNode _head;

            public OneWayListEnumerator(OneWayList.OneWayListNode head)
            {
                _head = head;
            }

            public int Current
            {
                get
                {
                    if (_current == null)
                    {
                        throw new Exception("Enumerator not started");
                    }

                    return _current.Value;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (_current == null)
                {
                    if (_head == null)
                    {
                        return false;
                    }

                    _current = _head;
                    return true;
                }
                else
                {
                    if (_current.Next == null)
                    {
                        return false;
                    }

                    _current = _current.Next;
                    return true;
                }
            }

            public void Reset()
            {
                _current = null;
            }
        }

    }
}




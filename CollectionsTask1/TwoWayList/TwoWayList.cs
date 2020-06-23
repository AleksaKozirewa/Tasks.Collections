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
    class TwoWayList : IEnumerable<int>
    {
        TwoWayListNode head = null;

        public TwoWayList(int[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                Add(input[i]);
            }
        }

        /// insert value to end of list
        public void Add(int value)
        {
            var node = new TwoWayListNode(value);
            if (head == null)
            {
                // empty collection
                head = node;
            }
            else
            {
                // not empty collection
                TwoWayListNode current = head;
                TwoWayListNode previous = current.Previous;
                while (current.Next != null)
                {
                    if (current == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    previous = current;
                    current = current.Next;

                }

                current.Next = node;
                Count++;
            }
        }

        public class TwoWayListNode
        {
            public TwoWayListNode(int value)
            {
                Value = value;
                Next = null;
                Previous = null;
            }

            public TwoWayListNode(int value, TwoWayListNode next, TwoWayListNode previous)
            {
                Value = value;
                Next = next;
                Previous = previous;
            }

            public int Value;
            public TwoWayListNode Next;
            public TwoWayListNode Previous;

        }

        public int Count { get; private set; }

        public bool Remove(int value)
        {
            TwoWayListNode current = head;

            if (current == null)
            {
                return false;
            }

            TwoWayListNode previous = current.Previous;

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
                    if (current.Previous != null)
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

                if (current.Next == null)
                {
                    return false;
                }

                previous = current;
                current = current.Next;
                current.Previous = previous;
            }

            return false;
        }

        public void AddAt(int value, int index)
        {
            var node = new TwoWayListNode(value);
            
            if (head != null && index != 0)
            {
                TwoWayListNode current = head;
                TwoWayListNode previous = current.Previous;

                for (var i = 0; i < index - 1; i++)
                {
                    if (current == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    previous = current;
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
            TwoWayListNode current = head;

            if (current == null)
            {
                return false;
            }

            TwoWayListNode previous = current.Previous;

            while (current != null)
            {
                if (index != 0)
                {
                    for (var i = 0; i < index; i++)
                    {
                        previous = current;

                        if (current.Next == null)
                        {
                            return false;
                        }

                        previous = current;
                        current = current.Next;
                        current.Previous = previous;
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
            TwoWayListNode current = head;

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
                var node = new TwoWayListNode(value);

                TwoWayListNode current = head;

                for (var i = 0; i < index; i++)
                {
                    if (current == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    current = current.Next;
                }

                if (current == null)
                {
                    return;
                }

                current.Value = node.Value;

            }
        }

        public void Sort()
        {
            if (head == null)
            {
                return;
            }

            TwoWayListNode node1 = head;
            TwoWayListNode node2 = head.Next;
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

            TwoWayListNode current = head;
            TwoWayListNode previous = current.Previous;
            TwoWayListNode next = current.Next;

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
            TwoWayListNode current = head;
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
            return new TwoWayListEnumerator(head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TwoWayListEnumerator(head);
        }

        private class TwoWayListEnumerator : IEnumerator<int>
        {
            private TwoWayList.TwoWayListNode _current;
            private TwoWayList.TwoWayListNode _head;

            public TwoWayListEnumerator(TwoWayList.TwoWayListNode head)
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




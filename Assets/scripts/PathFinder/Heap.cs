using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap <T> where T : IHeapItem<T>
{
    T[] element;
    int NumberOfItems;

    public Heap(int SizeofHeap)
    {
        element = new T[SizeofHeap];  //Setting the size of the array
    }   

    public void Add(T item) //Adding new items to the heap
    {
        
        item.HeapIndex = NumberOfItems;
        element[NumberOfItems] = item;
        SortUp(item);
        NumberOfItems++;
    }

    public T RemoveFirst()
    {
        T topelement = element[0];
        NumberOfItems--;
        element[0] = element[NumberOfItems];
        element[0].HeapIndex = 0;
        SortDown(element[0]);
        return topelement;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }
    public int Count
    {
        get
        {
            return NumberOfItems;
        }
    }
    public bool Contains(T item)
    {
        return Equals(element[item.HeapIndex], item);
    }
    void SortDown(T item)
    {
        while (true)
        {
            int childTOLeft = item.HeapIndex * 2 + 1;
            int childTORIght = item.HeapIndex * 2 + 2;
            int exchangeIndexvalue = 0;

            if(childTOLeft < NumberOfItems)
            {
                exchangeIndexvalue = childTOLeft;

                if(childTORIght < NumberOfItems)
                {
                    if(element[childTOLeft].CompareTo(element[childTORIght]) < 0)
                    {
                        exchangeIndexvalue = childTORIght;
                    }
                }

                if(item.CompareTo(element[exchangeIndexvalue]) < 0)
                {
                    Swap(item, element[exchangeIndexvalue]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentelement = element[parentIndex];
            if (item.CompareTo(parentelement) > 0)
            {
                Swap(item, parentelement);
            }
            else
            {
                break;
            }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB) //This will swap items based on priority, The priority here is the F cost of a node.
    {
        element[itemA.HeapIndex] = itemB;
        element[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}
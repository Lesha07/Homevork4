using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MyCollection<T> : IEnumerable<T> where T : IComparable<T>
{
    private T[] _data = new T[0];
    public event Action OnExpandedEvent;
    
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _data.Length)
                throw new IndexOutOfRangeException("Індекс виходить за межі масиву.");
            return _data[index];
        }
        set
        {
            if (index < 0 || index >= _data.Length)
                throw new IndexOutOfRangeException("Індекс виходить за межі масиву.");
            _data[index] = value;
        }
    }

    public void Add(T element)
    {
        
        T[] newData = new T[_data.Length + 1];
        for (int i = 0; i < _data.Length; i++)
        {
            newData[i] = _data[i];
        }
        newData[^1] = element; 
        _data = newData;

        
        OnExpandedEvent?.Invoke();
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _data)
        {
            yield return item;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
        MyCollection<int> collection = new MyCollection<int>();
        
        collection.OnExpandedEvent += () => Console.WriteLine("Масив було розширено!");
        
        collection.Add(10);
        collection.Add(5);
        collection.Add(20);
        collection.Add(15);

        Console.WriteLine("Елементи масиву:");
        foreach (var item in collection)
        {
            Console.WriteLine(item);
        }
        
        Console.WriteLine("\nЕлементи після сортування:");
        var sorted = collection.OrderBy(x => x);
        foreach (var item in sorted)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\nЕлементи, які більше за 10:");
        var filter = collection.Where(x => x > 10);
        foreach (var item in filter)
        {
            Console.WriteLine(item);
        }
        
        Console.WriteLine($"\nЕлемент за індексом 2: {collection[2]}");
        
        collection[2] = 25;
        Console.WriteLine($"Елемент за індексом 2 після зміни: {collection[2]}");
    }
}

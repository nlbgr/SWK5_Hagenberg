using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace HashDictionary.Impl;

public class HashDictinary<K, V> : IDictionary<K, V>
{
    private const int INITIAL_HASH_TABLE_LENGTH = 8;
    private Node[] ht = new Node[INITIAL_HASH_TABLE_LENGTH];
    private static readonly EqualityComparer<K> comparer = EqualityComparer<K>.Default;
    
    public int Count { get; private set; }
    public bool IsReadOnly { get; private set; }

    #region inner classes
    private class Node
    {
        public required K Key { get; init; }
        public required V Value { get; set; }
        public Node Next { get; set; }
    }

    #endregion

    #region helper methods

    private int IndexFor(K key) => Math.Abs(key.GetHashCode()) % ht.Length;

    private Node findNode(K key)
    {
        Node n = ht[IndexFor(key)];

        for (; n is not null; n = n.Next)
        {
            if (comparer.Equals(n.Key, key))
                return n;
        }

        return null;
    }

    private bool Add(K key, V value, out Node node)
    {
        node = findNode(key);
        if (node is not null)
        {
            return false; // key already exists
        }
        
        int index = IndexFor(key);
        node = ht[index] = new Node { Key = key, Value = value, Next = ht[index] };
        ++Count;
        
        return true;
    }
    
    #endregion

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(KeyValuePair<K, V> item) => this.Add(item.Key, item.Value);

    public void Clear()
    {
        ht = new Node[INITIAL_HASH_TABLE_LENGTH];
        Count = 0;
    }

    public bool Contains(KeyValuePair<K, V> item) => this.ContainsKey(item.Key);

    public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<K, V> item)
    {
        throw new NotImplementedException();
    }
    
    public void Add(K key, V value)
    {
        if (!Add(key, value, out _))
        {
            throw new ArgumentException("Key has already been added");
        }
    }

    public bool ContainsKey(K key) => findNode(key) is not null;

    public bool Remove(K key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(K key, [MaybeNullWhen(false)] out V value)
    {
        Node n = findNode(key);
        value = n is not null ? n.Value : default;
        return n is not null;
    }

    public V this[K key]
    {
        get
        {
            Node n = findNode(key);
            if (n is null) throw new KeyNotFoundException();
            return n.Value;
        }
        set
        {
            if (!Add(key, value, out Node node))
            {
                node.Value = value;
            }
        }
    }

    public ICollection<K> Keys
    {
        get
        {
            List<K> keys = new List<K>(this.Count);
            foreach (KeyValuePair<K, V> pair in this)
            {
                keys.Add(pair.Key);
            }
            return keys;
        }
    }

    public ICollection<V> Values { get; }
}
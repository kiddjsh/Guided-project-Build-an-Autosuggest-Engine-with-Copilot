public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; set; }
    public bool IsEndOfWord { get; set; }

    public char _value;

    public TrieNode(char value = ' ')
    {
        Children = new Dictionary<char, TrieNode>();
        IsEndOfWord = false;
        _value = value;
    }

    public bool HasChild(char c)
    {
        return Children.ContainsKey(c);
    }
}

public class Trie
{
    private TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    // Used Copilot to auto-generate code for Search method
    // Search for a word in the trie
    public bool Search(string word)
    {
        TrieNode current = root;
        // For each character in the word
        foreach (char c in word)
        {
            // If the current node does not have the character as a child
            if (!current.HasChild(c))
            {
                // The word is not in the trie
                return false;
            }
            // Move to the child node
            current = current.Children[c];
        }
        // If the word is in the trie
        return current.IsEndOfWord;
    }

    //Used Copilot to auto-generate comments
    //Inserts a word into the trie
    public bool Insert(string word)
    {
        // Start at the root node
        TrieNode current = root;
        // For each character in the word
        foreach (char c in word)
        {
            // If the current node does not have the character as a child
            if (!current.HasChild(c))
            {
                // Add the character as a child
                current.Children[c] = new TrieNode(c);
            }
            // Move to the child node
            current = current.Children[c];
        }
        // If the word is already in the trie
        if (current.IsEndOfWord)
        {
            // Return false
            return false;
        }
        // Mark the end of the word
        current.IsEndOfWord = true;
        return true;
    }
    
    /// <summary>
    /// Retrieves a list of suggested words based on the given prefix.
    /// </summary>
    /// <param name="prefix">The prefix to search for.</param>
    /// <returns>A list of suggested words.</returns>
    public List<string> AutoSuggest(string prefix)
    {
        TrieNode currentNode = root;
        foreach (char c in prefix)
        {
            if (!currentNode.HasChild(c))
            {
                return new List<string>();
            }
            currentNode = currentNode.Children[c];
        }
        return GetAllWordsWithPrefix(currentNode, prefix);
    }

    private List<string> GetAllWordsWithPrefix(TrieNode root, string prefix)
    {
        // removed return statement
        // Used Copilot to auto-generate List<string> code for GetAllWordsWithPrefix method

        // Create a list to store the words
        List<string> words = new List<string>();
        // If the root is null
        if (root == null)
        {
            // Return the list of words
            return words;
        }
        // If the root is the end of a word
        if (root.IsEndOfWord)
        {
            // Add the prefix to the list of words
            words.Add(prefix);
        }
        // For each child of the root
        foreach (var child in root.Children)
        {
            // Add the words with the prefix to the list of words
            words.AddRange(GetAllWordsWithPrefix(child.Value, prefix + child.Key));
        }
        // Return the list of words
        return words;
    }

    public List<string> GetAllWords()
    {
        return GetAllWordsWithPrefix(root, "");
    }

    public void PrintTrieStructure()
    {
        Console.WriteLine("\nroot");
        _printTrieNodes(root);
    }

    private void _printTrieNodes(TrieNode root, string format = " ", bool isLastChild = true) 
    {
        if (root == null)
            return;

        Console.Write($"{format}");

        if (isLastChild)
        {
            Console.Write("└─");
            format += "  ";
        }
        else
        {
            Console.Write("├─");
            format += "│ ";
        }

        Console.WriteLine($"{root._value}");

        int childCount = root.Children.Count;
        int i = 0;
        var children = root.Children.OrderBy(x => x.Key);

        foreach(var child in children)
        {
            i++;
            bool isLast = i == childCount;
            _printTrieNodes(child.Value, format, isLast);
        }
    }

    public List<string> GetSpellingSuggestions(string word)
    {
        char firstLetter = word[0];
        List<string> suggestions = new();
        List<string> words = GetAllWordsWithPrefix(root.Children[firstLetter], firstLetter.ToString());
        
        foreach (string w in words)
        {
            int distance = LevenshteinDistance(word, w);
            if (distance <= 2)
            {
                suggestions.Add(w);
            }
        }

        return suggestions;
    }

    // Used Copilot to auto-generate code for LevenshteinDistance method to fix the nested `for` loop causing an `IndexOutOfRangeException` updating the loop conditions to `i < m` and `j < n`
    private int LevenshteinDistance(string s, string t)
    {
        
        int m = s.Length;
        int n = t.Length;
        int[,] d = new int[m, n];
    
        if (m == 0)
        {
            return n;
        }
    
        if (n == 0)
        {
            return m;
        }
    
        for (int i = 0; i < m; i++)
        {
            d[i, 0] = i;
        }
    
        for (int j = 0; j < n; j++)
        {
            d[0, j] = j;
        }
    
        for (int j = 0; j < n; j++)
        {
            for (int i = 0; i < m; i++)
            {
                int cost = (s[i] == t[j]) ? 0 : 1;
                d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
            }
        }
    
        return d[m - 1, n - 1];
    }
}
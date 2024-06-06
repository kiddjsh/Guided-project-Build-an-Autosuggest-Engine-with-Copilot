namespace TrieDictionaryTest;

[TestClass]
public class TrieTest
{
    // Test that a word is inserted in the trie
    [TestMethod]
    public void InsertWord()
    {
        // Arrange
        var trie = new Trie();
        var word = "hello";

        // Act
        trie.Insert(word);

        // Assert
        Assert.IsTrue(trie.Contains(word));
    }

    // Test that a word is deleted from the trie
    [TestMethod]
    public void DeleteWord()
    {
        // Arrange
        var trie = new Trie();
        var word = "hello";
        trie.Insert(word);
    
        // Act
        trie.Delete(word);
    
        // Assert
        Assert.IsFalse(trie.Contains(word));
    }

    // Test that a word is not inserted twice in the trie
    [TestMethod]

    public void InsertWordTwice()
    {
        // Arrange
        var trie = new Trie();
        var word = "hello";

        // Act
        trie.Insert(word);
        trie.Insert(word);

        // Assert
        Assert.IsTrue(trie.Contains(word));
    }

    // Test that a word is not deleted from the trie if it is not present
    [TestMethod]

    public void DeleteWordNotPresent()
    {
        // Arrange
        var trie = new Trie();
        var word = "hello";

        // Act
        trie.Delete(word);

        // Assert
        Assert.IsFalse(trie.Contains(word));
    }

    // Test that a word is deleted from the trie if it is a prefix of another word
    [TestMethod]

    public void DeleteWordPrefix()
    {
        // Arrange
        var trie = new Trie();
        var word1 = "hello";
        var word2 = "hell";
        trie.Insert(word1);
        trie.Insert(word2);

        // Act
        trie.Delete(word2);

        // Assert
        Assert.IsTrue(trie.Contains(word1));
        Assert.IsFalse(trie.Contains(word2));
    }

    // Test AutoSuggest for the prefix "cat" not present in the 
    // trie containing "catastrophe", "catatonic", and "caterpillar"
    [TestMethod]

    public void TestAutoSuggest()
    {
        Trie dictionary = new Trie();
        dictionary.Insert("catastrophe");
        dictionary.Insert("catatonic");
        dictionary.Insert("caterpillar");

        List<string> suggestions = dictionary.AutoSuggest("cat");

        Assert.AreEqual(3, suggestions.Count);
        Assert.AreEqual("catastrophe", suggestions[0]);
        Assert.AreEqual("catatonic", suggestions[1]);
        Assert.AreEqual("caterpillar", suggestions[2]);
    }
    
    // Test GetSpellingSuggestions for a word not present in the trie
    [TestMethod]

    public void TestGetSpellingSuggestions()
    {
        Trie dictionary = new Trie();
        dictionary.Insert("catastrophe");
        dictionary.Insert("catatonic");
        dictionary.Insert("caterpillar");

        List<string> suggestions = dictionary.GetSpellingSuggestions("catatonicc");

        Assert.AreEqual(1, suggestions.Count);
        Assert.AreEqual("catatonic", suggestions[0]);
    }
}
using System.Text.Json;
using System.Text.RegularExpressions;


public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // First consideration, we are looking to use a O(n) that means a loop, a foreach pair in words then
        // if the pair first letter is not equal to the second letter then create a newPair with the inverted 
        // order, then if the new pair exits in the set, and if the pair (with a comparison) is < 0
        // (this comparison avoids duplicates), then adds the new pair into the list, and finally the list
        // is converted into an array.
        var symmetricPairs = new HashSet<string>(words);
        var pairs = new List<string>();

        foreach (string pair in words)
        {
            if (pair[1] != pair[0])
            {
                var newPair = $"{pair[1]}{pair[0]}";
                if (symmetricPairs.Contains(newPair) && (string.Compare(pair, newPair) < 0))
                {
                    pairs.Add($"{pair} & {newPair}");
                }
            }
        }
        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            // First we have to read the correct field
            // which is 3 and then for each unique key we will add
            // it to the dictionary and put a 1, then if we see a duplicate,
            // we will add it one more.

            var educationLevel = fields[3];


            if (degrees.ContainsKey(educationLevel))
                degrees[educationLevel]++;
            else
                degrees[educationLevel] = 1;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        // First, we normalize (put all in lower case) then we ignore the spaces, then
        // we have to check if word 1 and word 2 has the same length
        // Then we add the word to a dictionary (key = letter / value = how many times 
        // appears) and check with a for each loop if the letters in the second
        // word are in the map, if we find the letter we subtract one to the value
        // and if all the values in the dictionary are 0 it's an anagram if not is false

        var isAnagramMap = new Dictionary<char, int>();

        var newWord1 = Regex.Replace(word1.ToLower(), @"\s", "");
        var newWord2 = Regex.Replace(word2.ToLower(), @"\s", "");

        if (newWord1.Length != newWord2.Length)
            return false;
        else
        {
            foreach (char letter in newWord1)
            {
                if (isAnagramMap.ContainsKey(letter))
                    isAnagramMap[letter]++;
                else
                    isAnagramMap[letter] = 1;
            }
        }
        foreach (char letter in newWord2)
        {
            if (!isAnagramMap.ContainsKey(letter))
                return false;

            isAnagramMap[letter]--;

            if (isAnagramMap[letter] < 0)
                return false;
        }
        foreach (var count in isAnagramMap.Values)
        {
            if (count != 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        // First we have to add the code in the FeatureCollection to use and read the information from the JSON
        var earthquakeData = new List<string>();
        foreach (var earthquake in featureCollection.Features)
        {
            var place = earthquake.Properties.Place;
            var magnitude = earthquake.Properties.Magnitude;

            earthquakeData.Add($"{place} - Mag {magnitude}");
        }

        return earthquakeData.ToArray();
    }
}
public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary
    string Type { get; set; }
    public Feature[] Features { get; set; }
}

public class Feature
{
    string Type { get; set; }
    public Property Properties { get; set; }
}

public class Property
{
    public double Magnitude { get; set; }
    public string Place { get; set; }
}
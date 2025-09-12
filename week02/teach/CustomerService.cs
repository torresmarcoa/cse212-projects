/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Creating one new queue with a max size of -5 to trigger the 
        // default size
        // Expected Result: Queue size 10
        Console.WriteLine("Test 1");
        var cs = new CustomerService(-5);
        Console.WriteLine($"Queue should be 10: {cs}");
        // Defect(s) Found: No defect found

        Console.WriteLine("=================");


        // Test 2
        // Scenario: Adding and serving one customer 
        // Expected Result: Display the customer
        Console.WriteLine("Test 2");
        var customer = new CustomerService(2);
        customer.AddNewCustomer();
        customer.ServeCustomer();

        // Defect(s) Found: ServeCustomer was not checking length
        // and was deleting without getting the customer first 

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
        // Test 3
        // Scenario: Testing service order 2 customers
        // Expected Result: Entering order should remain untouched
        Console.WriteLine("Test 2");
        customer = new CustomerService(4);
        customer.AddNewCustomer();
        customer.AddNewCustomer();
        Console.WriteLine($"Before serving customers: {customer}");
        customer.ServeCustomer();
        customer.ServeCustomer();
        Console.WriteLine($"After serving customers: {customer}");
        // Defect(s) Found: No problems

        // Test 4
        // Scenario: Trying to add more customers than the max size
        // Expected Result: Error prompted 
        Console.WriteLine("Test 4");
        customer = new CustomerService(3);
        customer.AddNewCustomer();
        customer.AddNewCustomer();
        customer.AddNewCustomer();
        customer.AddNewCustomer();
        Console.WriteLine($"Service Queue: {customer}");
        // Defect(s) Found: Add customer was not handling max size correctly
        // using > instead of >= is like max size + 1 prompt error
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) // Error, using > will let one more customer
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        if (_queue.Count <= 0) // it needs a check on the length
        {
            Console.WriteLine("No Customers in the queue");
        }
        else {
            var customer = _queue[0];
            _queue.RemoveAt(0); // deletion should happen before to provide accurate info
            Console.WriteLine(customer);
        }
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}
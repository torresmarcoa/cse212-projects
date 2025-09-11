using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Trying to Dequeue from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message
    // Defect(s) Found: No defect found
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Trying to dequeue from an empty queue should fail");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
        }
    }

    [TestMethod]
    // Scenario: Create a new queue with the following elements: Doe priority:5, John priority:7,
    // and run until it's empty
    // Expected Result: John dequeued first, then Doe
    // Defect(s) Found: The dequeue function was never dequeueing, so the first element to get in
    // was the only element being printed. Also, the for loop had an error when counting  _queue.Count -1
    // provoking that the last item on the index was never compared
    public void TestPriorityQueue_ReversePriority()
    {
        var doe = new PriorityItem("Doe", 5);
        var john = new PriorityItem("John", 7);

        PriorityItem[] expectedResults = [john, doe];

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(doe.Value, doe.Priority);
        priorityQueue.Enqueue(john.Value, john.Priority);
        int i = 0;
        while (priorityQueue.Length > 0)
        {
            if (i >= expectedResults.Length)
            {
                Assert.Fail("Queue should have ran out of priorityItems");
            }
            var priorityItem = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResults[i].Value, priorityItem);
            i++;
        }
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Create a new queue with the following elements: h,1 e,2 l,3 l,4 o,5
    // and run until it's empty
    // Expected Result: Dequeue o first and H last (testing queue normal functionality)
    // Defect(s) Found: None, queue functionality assured
    public void TestPriorityQueue_QueueFunctionality()
    {
        var h = new PriorityItem("H", 1);
        var e = new PriorityItem("e", 2);
        var l = new PriorityItem("l", 3);
        var l_2 = new PriorityItem("l", 4);
        var o = new PriorityItem("o", 5);
        PriorityItem[] expectedResults = [o, l_2, l, e, h];
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(h.Value, h.Priority);
        priorityQueue.Enqueue(e.Value, e.Priority);
        priorityQueue.Enqueue(l.Value, l.Priority);
        priorityQueue.Enqueue(l_2.Value, l_2.Priority);
        priorityQueue.Enqueue(o.Value, o.Priority);
        int i = 0;
        while (priorityQueue.Length > 0)
        {
            if (i >= expectedResults.Length)
            {
                Assert.Fail("Queue should have ran out of priorityItems");
            }
            var priorityItem = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResults[i].Value, priorityItem);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a new queue with two elements: 'I am second',2 'I should be first', 2
    // to test same priority functionality
    // Expected Result: Given that "If there are more than one item with the highest priority, 
    // then the item closest to the front of the queue will be removed and its value returned."
    // we are expecting the first element that is closest to the top to be removed and the 'I am second'
    //(First to get in) will be the last to get out
    // Defect(s) Found: We were using >= instead of > which causes ties to favor the last inserted item
    public void TestPriorityQueue_TiePriority()
    {
        var second = new PriorityItem("I am second", 2);
        var first = new PriorityItem("I should be first", 2);

        PriorityItem[] expectedResults = [second, first];
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(second.Value, second.Priority);
        priorityQueue.Enqueue(first.Value, first.Priority);
        int i = 0;
        while (priorityQueue.Length > 0)
        {
            if (i >= expectedResults.Length)
            {
                Assert.Fail("Queue should have ran out of priorityItems");
            }
            var priorityItem = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResults[i].Value, priorityItem);
            i++;
        }
    }
}
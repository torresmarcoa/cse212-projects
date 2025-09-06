public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /// To create the desired functionality and create an array of multiples for a given number
        /// we will have to create a loop that creates a new array with the multiples of a given
        /// number and a given quantity of multiples for that number
        /// 
        /// First we will initialize and array with the desired length
        /// var multiplesArray = new int[length]
        /// in that way we are sure that the array will contain only the fixed size we wanted
        /// 
        /// Then we will have to create the loop (using a for loop) that will take the number and
        /// multiply it by i++ starting with 1 (in that way we will reach 1, 2, 3...) until reaching
        /// the length
        /// for i starting with 1 loop i = length times, each loop add 1 to i (i++)
        ///     (inside the loop) multiply number by i times 
        ///     add the result in the multiples array (because it starts with 0 as it first index
        ///     I had to subtract 1 from i before adding it to the array)

        var multiplesArray = new double[length];

        for (var i = 1; i <= length; i++)
        {
            double result = number * i;
            multiplesArray[i - 1] = result;
        }

        return multiplesArray; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /// To create the functionality to rotate the items on the list to right we will have to
        /// use the Reverse method to the list so we can use the amount and the list.
        /// Since the Reverse reverts all the list, we will reverse the list 3 times,
        /// one for all the list, for example (1, 2, 3, 4, 5), then one rotate for the
        /// first indexes starting with 0 and ending in amount, and one last for the rest of the
        /// list

        data.Reverse();
        data.Reverse(0, amount);
        data.Reverse(amount, data.Count - amount);

        return;
    }
}

/*
    If we list all the natural numbers below 10 that are multiples of 3 or 5, 
    we get 3, 5, 6 and 9. The sum of these multiples is 23.

    Find the sum of all the multiples of 3 or 5 below 1000.
*/


object One 
{
    def main(args: Array[String]) 
    {
        var sum = 0;
        var z = 0; 
        for ( z <- 1 until 1000) 
        {
            print(z);

            if (z % 3 == 0)
            {
                println(" multiple of 3");
                sum = sum + z;
            }
            else
            {
                if ((z % 5) == 0)
                {
                    println(" multiple of 5");
                    sum = sum + z;
                }
                else
                {
                    println("");
                }
            }
        }

        println("The sum is " + sum);
    }
}
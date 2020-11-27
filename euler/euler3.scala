/*
    The prime factors of 13195 are 5, 7, 13 and 29.

    What is the largest prime factor of the number 600851475143 ?
*/

import scala.math._;
import scala.language.postfixOps;
import scala.util.control.Breaks._;

object Three
{

    //val n = 13195;
    val n = 600851475143L;

    // At some point we will be marking array values to indicate that they
    // are not prime.
    val notPrime = 0;

    def remove_multiples(p : Int, primes : Array[Int], sqrtN : Int)
    {
        //println("in remove_mulitiples p is " + p + " value " + primes(p));

        for (z <- p + primes(p) until sqrtN by primes(p))
        {
            primes(z) = notPrime;
        }
    }

    def main(args: Array[String])
    {
        // ASSERT the largest prime factor <= floor(sqrt(N))

        // GOAL Generate a list to calculate prime numbers upto sqrt(N)

            // What is the limit of the possible prime numbers?
            val sqrtN = scala.math.floor(scala.math.sqrt(n)).toInt;
            println("The largest factor can't exceed " + sqrtN);

            // We need a list with those numbers
            var primes = 1 to sqrtN toArray;
            //println(primes.deep.mkString(" "));

        // GOAL cross out elements that are not prime.

            // By definition, 1 is not prime.
            primes(0) = notPrime;
            //println(primes.deep.mkString(" "));

            // Now, let's remove multiples, starting with 2
            for(p <- 1 to sqrtN if primes(p - 1) != notPrime)
            {
                remove_multiples(p - 1, primes, sqrtN);
            }
            //println(primes.deep.mkString(" "));

        // Goal scan that list backward, looking for the first factor

            breakable
            {
                for (p <- sqrtN - 1 to 1 by -1 if primes(p) != notPrime)
                {
                    var mod = n % (p + 1);
                    //println("N " + n + " p " + p + " primes(p) " + primes(p) + " rem " + mod);

                    if (mod == 0)
                    {
                        println("And the answer is " + (p + 1));
                        break;
                    }
                    else
                    {
                    }
                }
            }
    }
}


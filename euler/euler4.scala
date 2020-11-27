
/*                                                                                          
    A palindromic number reads the same both ways.                                          
    The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 X 99.  
                                                                                            
    Find the largest palindrome made from the product of two 3-digit numbers.               
                                                                                          */

import scala.language.postfixOps;


object Four extends App
{
    println("start");
    val numberOfDigits = 3;

    val start = scala.math.pow(10, numberOfDigits - 1).toInt;
    val end = scala.math.pow(10, numberOfDigits).toInt - 1;
    println("Range from " + start + " to " + end);

    val numbers = for (z <- start to end;
                       y <- start to z)
                           yield(z * y);

    //println(numbers, " ");

    val palindromes = numbers.filter {p => p.toString == p.toString.reverse}

    println(palindromes, "\n");

    // Pick the maximum value from that list.

    val largest = palindromes.max;

    println("Largest palindrome " + largest);

    println("end");
}

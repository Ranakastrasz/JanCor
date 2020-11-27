
/*
    2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.

    What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
*/

import scala.annotation.tailrec;
import util.control.Breaks._
import scala.collection.mutable.BitSet

object Five extends App
{
    println("Start");

    val n = 20L;

    var numbers = 1L to  n;

    println(numbers mkString " ");

    var upperLimit = numbers.product;

    println("product of " + n + " is " + upperLimit);

    var bitArray =  new scala.collection.mutable.BitSet(upperLimit)

//    var listToTest = n to upperLimit by n;
//
//    println("the list to test contains " + listToTest.length + " elements");
//
//    breakable
//    {
//        for (element <- listToTest)
//        {
//            var good = true;
//            breakable
//            {
//                good = true;
//                for (divisor <- numbers)
//                {
//                    if (element % divisor == 0)
//                    {
//                    }
//                    else
//                    {
//                        good = false;
//                        break;
//                    }
//                }
//            }
//
//            if (good)
//            {
//                println("Found " + element);
//                break;
//            }
//        }
//    }

    println("Finish");
}



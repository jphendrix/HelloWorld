using System;
using System.Collections.Generic;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * This is a flag to tell the program to quit running or not. As
             * long as quit is false, the program will continue to ask for a 
             * new number.
             */ 
            Boolean quit = false;

            Console.WriteLine("Welcome to the Prime Factorization demo!");

            //Keep running until we are told to quit.
            while (!quit)
            {
                Console.WriteLine("Give me an integer.  Or just press ENTER to quit");

                /*
                 * ReadLine() tells the application to wait for user input.  The program will
                 * pause until the user hits the ENTER key.  Any text typed in will be stored
                 * in the response variable as a string.
                 */ 
                string response = Console.ReadLine();

                if(response == "")
                {
                    //If the reponse was empty then the user pressed ENTER without typeing anything else in.
                    quit = true;
                }
                else
                {
                    /*
                     * This block of code is called a "try catch".  Without it, if there is an error, the program would crash and stop running.
                     * Any time you think you might encounter an error, it is a good idea to warp your code in a try catch.  Parsing (or converting)
                     * a string to a number is a common place for errors.  The runtime would not know how to convert "quit" to a number.
                     */ 
                    try
                    {
                        //response is a string datatype.  But to do any math on it, we need to convert it to a number.  In this case as 64 bit integer
                        Int64 num = Int64.Parse(response);

                        //Find all of the factors and the prime factors of the number.
                        Int64[] factors = factor(num);
                        Int64[] primeFactors = primeFactor(num);

                        //Show the user what we found
                        Console.WriteLine("The factors of {0} are [{1}]", num, intArrayToCSV(factors));
                        Console.Write("The ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Prime ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("factors of {0} are [{1}]\n\n", num, intArrayToCSV(primeFactors));
                    }
                    catch (Exception ex)
                    {
                        //Something bad happend.  Tell the user about it then quit.
                        Console.WriteLine("I asked for an interger but you gave me {0} which caused error '{1}'.", response, ex.Message);
                        quit = true;
                    }
                }
            }

            //Let the user know we are done.
            Console.WriteLine("\nGood Bye.");

            //Wait 5 seconds (same as 5000 miliseconds) before actually exiting.  This gives the user enought time to see the bye message.
            System.Threading.Thread.Sleep(5000);
        }

        /// <summary>
        /// Find all of the factors of a number.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Array of numbers between 1 and the input number that divide evenly into the number</returns>
        static Int64[] factor(Int64 x)
        {
            /*
             * A List is very similar to an Array but a List
             * allows us to add values to it.  An Array is fixed
             * size and can not be altered.
             */ 
            List<Int64> factorList = new List<Int64>();

            //Loop through every number between 1 and our input number
            for(Int64 y=1; y<=(x/2); y++)
            {
                /*
                 * Just like "+" is an addition operation and "/" represents division,  "%" is an operation called modulo.
                 * It tells you the remainder of a division operation. 5%2=1, 3%3=0, 10%8=2.  If x%2=0 then x is an even number.  
                 * Modulo is handy for a lot of things but here we are using it to see if the divion has a remainder or not.  If
                 * the remainder is zero, then x is divisible by y.
                 */ 
                if(x % y == 0)
                {
                    //y is a factor of x because there is no remainder after x/y
                    factorList.Add(y);

                    Console.Write("found factor {0}\r", y);
                }
            }

            //We did not count all the way up the number but we know it is a factor of itself
            factorList.Add(x);

            //Convert the List to an Array and return it.
            return factorList.ToArray();
        }

        /// <summary>
        /// Find all the prime factors of a number
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Array of prime numbers.  When multiplied together, they will equal the input number.</returns>
        static Int64[] primeFactor(Int64 x)
        {
            List<Int64> factorList = new List<Int64>();

            //Loop through every number between 2 and our input number-1
            for (int y = 2; y <= (x/2); y++)
            {
                if (x % y == 0)
                {
                    //y is a factor of x b/c there is no remainder after x/y but is it a prime factor?

                    if (isPrime(y))
                    {
                        //It is prime.  Add it to the list.
                        factorList.Add(y);
                        Console.Write("found prime {0}\r", y);

                        //divide x by the prime value y then set y back to 2 and see if there are any more primes in x/y
                        x = x / y;
                        y = y-1; //It will be y again in the next step
                    }

                }
            }

            //We skipped 1 in the loop above.  Add it to the beginning of the list now.
            factorList.Insert(0, 1);

            //We did not count all the way up the number but we know it is a factor of itself
            factorList.Add(x);

            //Convert the List to an Array and return it.
            return factorList.ToArray();
        }

        /// <summary>
        /// Check to see if a number is prime or not.
        /// </summary>
        /// <param name="num"></param>
        /// <returns>true/false</returns>
        static bool isPrime(Int64 num)
        {
            /**
             * Prime numbers are number that are only divisible by
             * themselves and 1.
             * 
             * TODO: This function is broken.  Until it knows how to check if a number is
             * prime or not, it will just guess.
             */
            bool isPrime = false;
            Random random = new Random();

            //Returns a random number that is equal or greater than 1  less than 3.
            //Expect either 1 or 2
            int r = random.Next(1, 3);

            //r should be 1 roughly 50% of the time
            if(r == 1)
            {
                isPrime = true;
            }

            //If you are having trouble with this funtion, uncomment this line to help with trouble shooting.
            //Console.WriteLine("\t{0} {1} prime", num,isPrime?"is":"is not");

            return isPrime;
        }

        /// <summary>
        /// Converts an Array of intergers into a comma seperated list of strings.
        /// </summary>
        /// <param name="a"></param>
        /// <returns>string list of integers in the format of "x,y,z"</returns>
        static string intArrayToCSV(Int64[] a)
        {
            string list = "";
            for (Int64 i = 0; i < a.Length; i++)
            {
                //concatenate the string value of the integer followed by a comma.
                list += a[i].ToString() + ",";
            }

            if(list.Length>1)
            {
                //Trim off the last character of the string.  This is a trailing ",".
                list = list.Substring(0, list.Length - 1);
            }

            return list;
        }
    }
}

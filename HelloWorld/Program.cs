using System;
using System.Collections.Generic;

namespace HelloWorld
{
    class Program
    {
        static List<UInt64> _primes = new List<UInt64>();
        static void Main(string[] args)
        {
            /*
             * This is a flag to tell the program to quit running or not. As
             * long as quit is false, the program will continue to ask for a 
             * new number.
             */ 
            Boolean quit = false;

            Console.WriteLine("Welcome to the Prime Factorization demo!");

            init();

            //Keep running until we are told to quit.
            while (!quit)
            {
                Console.WriteLine("Give me an integer.  Or typle \"prime\" to find them all");
                Console.WriteLine("Or just press ENTER to quit");

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
                else if(response.Trim().ToLower() == "prime")
                {
                    findThemAll();
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
                        UInt64 num = UInt64.Parse(response);

                        //Find all of the factors and the prime factors of the number.
                        UInt64[] primeFactors = primeFactor(num);

                        //Show the user what we found
                        Console.WriteLine("The prime factors of {0} are [{1}]", num, intArrayToCSV(primeFactors));
                    }
                    catch(OverflowException ex)
                    {
                        //Too big
                        Console.WriteLine("dang.  That number is too big (or too small?) for me. I can only do numbers between 0 and {0:n0}",UInt64.MaxValue);
                    }
                    catch (Exception ex)
                    {
                        //Something bad happend.  Tell the user about it then quit.
                        Console.WriteLine("I asked for an interger but you gave me {0} which caused error '{1}'.", response, ex.Message);
                    }
                }
            }

            //Let the user know we are done.
            Console.WriteLine("\nGood Bye.");

            //Wait 5 seconds (same as 5000 miliseconds) before actually exiting.  This gives the user enought time to see the bye message.
            System.Threading.Thread.Sleep(5000);
        }


        /// <summary>
        /// Find all the prime factors of a number
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Array of prime numbers.  When multiplied together, they will equal the input number.</returns>
        static UInt64[] primeFactor(UInt64 x)
        {
            List<UInt64> factorList = new List<UInt64>();


            //Loop through every odd number between 3 and our input sqrt(number)
            for (UInt64 y = 2; y <= Math.Sqrt(x); y++)
            {
                if (x % y == 0)
                {
                    //y is a factor of x b/c there is no remainder after x/y but is it a prime factor?

                    if (isPrime(y))
                    {
                        //It is prime.  Add it to the list.
                        factorList.Add(y);

                        //divide x by the prime value y then set y back to 2 and see if there are any more primes in x/y
                        x = x / y;
                        y = y-1; //It will be y again in the next step
                    }
                }
            }

            //x is always divisible by 1
            factorList.Insert(0, 1);

            if (isPrime(x))
            {
                factorList.Add(x);
            }

            //Convert the List to an Array and return it.
            return factorList.ToArray();
        }

        /// <summary>
        /// Check to see if a number is prime or not.
        /// </summary>
        /// <param name="num"></param>
        /// <returns>true/false</returns>
        static bool isPrime(UInt64 num)
        {
            /**
             * Prime numbers are number that are only divisible by
             * themselves and 1.
             * 
             */

            //2 is the only even prime
            if(num>2 && num %2 == 0)
            {
                return false;
            }

            //Check to see if it is known.
            for(int i =0; i<_primes.Count; i++)
            {
                if (_primes[i] == num)
                {
                    return true;
                }
            }

            for (UInt64 i = 3; i< Math.Sqrt(num); i += 2)
            {
                //If it divides by any other number than its self, then it is not prime.
                if(num%i==0 && i != num)
                {
                    return false;
                }
            }

            _primes.Add(num);
            return true;
        }

        /// <summary>
        /// Converts an Array of intergers into a comma seperated list of strings.
        /// </summary>
        /// <param name="a"></param>
        /// <returns>string list of integers in the format of "x,y,z"</returns>
        static string intArrayToCSV(UInt64[] a)
        {
            string list = "";
            for (int i = 0; i < a.Length; i++)
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

        /// <summary>
        /// We already know some primes.
        /// </summary>
        static void init()
        {
            _primes.Add(1);
            _primes.Add(2);
            _primes.Add(3);
        }

        static void findThemAll()
        {
            for(UInt64 i=1; i<Math.Sqrt(UInt64.MaxValue); i+=2)
            {
                if (isPrime(i))
                {
                    Console.Write(" {0:n0} is prime\r", i);
                }
            }
        }
    }

 
}

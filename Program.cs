using System;
using System.Collections.Generic;
using System.Numerics;

namespace TestLearningApplication
{
    class Program
    {
        public class RSAEncryption
        {
            public int p1, p2;
            public int n1, n2;
            public int n;
            public int phi;
            public int e, d;

            public void Display()
            {
                Console.WriteLine("p1:{0}, p2:{1}", p1, p2);
                Console.WriteLine("n1:{0}, n2:{1}", n1, n2);
                Console.WriteLine("n:{0}", n);
                Console.WriteLine("phi:{0}", phi);
                Console.WriteLine("e:{0}, d:{1}", e, d);

            }
        }

        public static bool IsPrimeNumber(int number)
        {

            if (number < 2)
                return true;

            for (int i = 2; i <= (number / 2); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;

        }
        
        // Greatest Common Divisor
        private static int gcd(int n1, int n2)
        {
            int t;
            while (n2 != 0)
            {
                t = n1;
                n1 = n2;
                n2 = t % n2;
            }
            return n1;
        }

        // Is n1 relatively prime to n2
        private static bool IsRelativelyPrime(int n1, int n2)
        {
            return gcd(n1, n2) == 1;
        }

        private static int FindDecryptionKey(int e, int phi)
        {
            List<int> ValidPrimeNumbers = new List<int>();

            Console.WriteLine("Finding all prime numbers between 2 and phi: {0}", phi);

            for(int i=2; i<phi; i++)
            {
                if (IsPrimeNumber(i) && ((i*e)%phi) == 1)
                {
                    Console.WriteLine("Adding item: {0} to the prime numbers list", i);
                    ValidPrimeNumbers.Add(i);
                }
            }

            Console.WriteLine("Found {0} prime numbers between 2 and phi: {1}", ValidPrimeNumbers.Count, phi);


            if (ValidPrimeNumbers.Count > 0)
            {

                int random = new Random().Next(0, ValidPrimeNumbers.Count);
                return ValidPrimeNumbers[random];
            }

            return 0;
        }

        public static RSAEncryption GeneratePublicPrivateKeys()
        {
            RSAEncryption encryptionObj = new RSAEncryption();

            encryptionObj.n1 = 10;
            encryptionObj.n2 = 100;

            encryptionObj.p1 = GenerateRandomPrimeNumber(encryptionObj.n1, encryptionObj.n2);
            encryptionObj.p2 = GenerateRandomPrimeNumber(encryptionObj.p1 + 1, encryptionObj.n2);

            encryptionObj.n = encryptionObj.p1 * encryptionObj.p2;
            encryptionObj.phi = (encryptionObj.p1 - 1) * (encryptionObj.p2 - 1);

            //encryptionObj.Display();

            do
            {
                Console.WriteLine("Generating e ... ");
                encryptionObj.e = GenerateRandomPrimeNumber(encryptionObj.n1, encryptionObj.n2);
                Console.WriteLine("Generated e: {0}", encryptionObj.e);
            } while (!IsRelativelyPrime(encryptionObj.e, encryptionObj.n) || !IsRelativelyPrime(encryptionObj.e, encryptionObj.phi));

            //encryptionObj.Display();

            //int NumOfTries = 0;

            Console.WriteLine("Generating d ... ");

            encryptionObj.d = FindDecryptionKey(encryptionObj.e, encryptionObj.phi);

            //do
            //{


                //encryptionObj.d = GenerateRandomPrimeNumber(encryptionObj.n1, encryptionObj.n2);// encryptionObj.phi);
                //NumOfTries++;
                
                //Console.WriteLine("d: {0}, ((encryptionObj.d * encryptionObj.e) % encryptionObj.phi) : {1}", encryptionObj.d, ((encryptionObj.d * encryptionObj.e) % encryptionObj.phi));

                //if (NumOfTries != 0 && NumOfTries % 100000 == 0)
                //{
                //    Console.WriteLine("NumOfTries = {0}", NumOfTries);
                //    do
                //    {

                //        encryptionObj.e = GenerateRandomPrimeNumber(1, 30);

                //    } while (encryptionObj.e == encryptionObj.d || !IsRelativelyPrime(encryptionObj.e, encryptionObj.n) || !IsRelativelyPrime(encryptionObj.e, encryptionObj.phi));

                //    //encryptionObj.Display();
                //}

                //if (((encryptionObj.d * encryptionObj.e) % encryptionObj.phi) != 1)
                //    continue;

            //} while (((((encryptionObj.d * encryptionObj.e) % encryptionObj.phi) != 1) || !IsRelativelyPrime(encryptionObj.d, encryptionObj.n) || !IsRelativelyPrime(encryptionObj.d, encryptionObj.phi) || !IsRelativelyPrime(encryptionObj.d, encryptionObj.e) || encryptionObj.d == encryptionObj.e));
            //while (encryptionObj.d != encryptionObj.e);
            //(!IsRelativelyPrime(encryptionObj.d, encryptionObj.n) || !IsRelativelyPrime(encryptionObj.d, encryptionObj.phi) || !IsRelativelyPrime(encryptionObj.d, encryptionObj.e));

            //Console.WriteLine("((encryptionObj.d * encryptionObj.e) % encryptionObj.phi) : {0}", ((encryptionObj.d * encryptionObj.e) % encryptionObj.phi));

            //encryptionObj.Display();

            Console.WriteLine("Generated d: {0}", encryptionObj.d);


            return encryptionObj;
        }

        public static int GenerateRandomPrimeNumber(int n1, int n2)
        {
            int random = -1;
            int numberOfTries = 0;

            do
            {
                random = new Random().Next(n1, n2);
                if (IsPrimeNumber(random))
                {
                    break;
                }

            } while (random != -1 && numberOfTries < 10000);

            return random;
        }

        static void Main(string[] args)
        {

            RSAEncryption encryptionObj = GeneratePublicPrivateKeys();
            do
            {
                //Console.WriteLine("Hello World! \nWelcome to RSA algorithm of generating Public-Private Keys. \nPress 'q' to Quit..");
                Console.WriteLine("Welcome to RSA Encryption algorithm of generating Public-Private Keys.\nPress 'q' to Quit from the program..");

                encryptionObj.Display();

                int ToSend = 0;

                Console.WriteLine("Hey Bob, Enter the number to send to Alice: ");

                try
                {
                    ToSend = int.Parse(Console.ReadLine());
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }

                Console.WriteLine("Now Let's say Bob wants to send Alice : {0}", ToSend);

                Console.WriteLine("Alice asks Bob to send m ^ {e} mod (n) instead of sending m directly");

                Console.WriteLine("Alice confidently shares e = {0} and n = {1} knowing well that the message could be intruded by Eve", encryptionObj.e, encryptionObj.n);

                BigInteger msgSent = ((BigInteger.Pow(ToSend, encryptionObj.e)) % encryptionObj.n);

                Console.WriteLine("So now Bob tries to send m ^ e mod (n) = {0} ^ {1} mod ({2}) = {3} (Let's call that msgSent)", ToSend, encryptionObj.e, encryptionObj.n, msgSent);

                Console.WriteLine("So now Alice tries to decrypt the message using msgSent ^ d mod (n) = {0} ^ {1} mod ({2}) = {3}", msgSent, encryptionObj.d, encryptionObj.n, ((BigInteger.Pow(msgSent, encryptionObj.d)) % encryptionObj.n));
            } while (!Console.ReadKey().Equals("q"));
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows;
using ZeroKnowledgeProofApp.Other;

namespace ZeroKnowledgeProofApp.Dialogs.DialogsViewModels
{
    class GenerateKeysViewModel: BaseVM
    {
        #region Поля

        BigInteger p = 0;
        BigInteger q = 0;
        BigInteger n = 0;
        BigInteger v0 = 0;
        BigInteger s = 0;
        DelegateCommand register;

        #endregion

        #region Конструктор

        public GenerateKeysViewModel()
        {
            n = GetN(out p, out q);
            v0 = GetOpenedKey(n, p, q);
            s = GetHiddenKey(v0, n);
        }

        #endregion

        #region Свойства

        public BigInteger P
        {
            get => p;
        }

        public BigInteger Q
        {
            get => q;
        }

        public BigInteger N
        {
            get => n;
        }

        public BigInteger V0
        {
            get => v0;
        }

        public BigInteger S
        {
            get => s;
        }

        #endregion

        #region Команды

        public DelegateCommand Register
        {
            get
            {
                return register ??= new DelegateCommand((arg) =>
                {
                    var dialog = (Window)arg;
                    dialog.DialogResult = true;
                });
            }
        }

        #endregion

        #region Методы

        static BigInteger GetN(out BigInteger p, out BigInteger q)
        {
            var primesList = Helpers.SieveEratosthenes(Helpers.maxEratosthenes);
            var random = new Random();
            p = primesList[random.Next(primesList.Count)];
            q = primesList[random.Next(primesList.Count)];
            BigInteger n = p * q;
            return n;
        }

        static BigInteger GetOpenedKey(BigInteger n, BigInteger p, BigInteger q)
        {
            var random = new Random();
            //Расчет всех квадратичных вычетов
            var quadraticResiduesList = new List<BigInteger>();
            for (BigInteger i = 1; i < n; i++)
            {
                BigInteger value = i * i % n;
                quadraticResiduesList.Add(value);
            }
            //Фильтрация квадратичных вычетов
            quadraticResiduesList = quadraticResiduesList.Distinct().ToList();
            var filteredList = quadraticResiduesList.Where(v => !(v % p == 0 || v % q == 0)).ToList();
            return filteredList[random.Next(filteredList.Count)];
        }

        static BigInteger GetHiddenKey(BigInteger v0, BigInteger n)
        {
            var v1 = (1 + n) / v0;
            return (v1 + n).Sqrt();
        }

        #endregion
    }

    #region Вспомогательный класс

    public static class Helpers
    {
        #region Вспомогательные алгоритмы

        #region Решето Эратосфена

        public static int maxEratosthenes = 21000;
        public static List<BigInteger> SieveEratosthenes(int n)
        {

            bool[] is_prime = new bool[n + 1];
            for (int i = 2; i <= n; ++i)
                is_prime[i] = true;

            List<BigInteger> primes = new List<BigInteger>();

            for (int i = 2; i <= n; ++i)
                if (is_prime[i])
                {
                    primes.Add(i);
                    if (i * i <= n)
                        for (int j = i * i; j <= n; j += i)
                            is_prime[j] = false;
                }

            return primes;
        }

        #endregion

        #region Квадратный корень из BigInteger

        public static BigInteger Sqrt(this BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }

        #endregion

        #endregion
    }

    #endregion
}

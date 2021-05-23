using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ZeroKnowledgeProofApp.Other;

namespace ZeroKnowledgeProofApp.Dialogs.DialogsViewModels
{
    class IterationControlViewModel: BaseVM
    {
        #region Поля

        BigInteger n = BigInteger.Parse(CurrentUserInfo.CurrentUser.N);
        BigInteger v = BigInteger.Parse(CurrentUserInfo.CurrentUser.V0);
        BigInteger s = CurrentUserInfo.S;
        BigInteger r = 0;
        BigInteger x = 0;
        BigInteger y = 0;
        bool result;
        int b;

        #endregion

        #region Конструктор

        public IterationControlViewModel()
        {
            r = RandomIntegerBelow(n);
            x = r * r % n;
            b = new Random().Next(2);
        }

        #endregion

        #region Свойства

        public BigInteger R { get => r; }

        public BigInteger X { get => x; }

        public int B { get => b; }

        public bool Result { get => result; }

        public string Step3Text
        {
            get
            {
                if (b == 0)
                    return "3. b = 0, тогда сторона A отправляет r стороне B";
                else
                    return "3. b = 1, тогда сторона A отправляет стороне B\ny = r * S mod n";
            }
        }

        public string Step3Value
        {
            get
            {
                if (b == 0)
                    return $"R : {R}";
                else
                {
                    y = r * CurrentUserInfo.S % n;
                    OnPropertyChanged(nameof(Step4Value));
                    return $"Y : {y}";
                }
            }
        }

        public string Step4Text
        {
            get
            {
                if (b == 0)
                {
                    return "4. b = 0, сторона В проверяет, что х = r^2 mod n,\nчтобы убедиться, что А знает sqrt (x).";
                }
                else
                    return "4. b = 1, сторона В проверяет, что х = у^2 *V mod n,\nчтобы быть уверенной, что А знает sqrt (V-1).";
            }
        }

        public string Step4Value
        {
            get
            {
                if (b == 0)
                {
                    if (x == r * r % n)
                    {
                        result = true;
                        return "Результат : прошел";
                    }
                    else
                    {
                        result = false;
                        return "Результат : не прошел";
                    }
                }
                else if (b == 1)
                    if (x == y * y * v % n)
                    {
                        result = true;
                        return "Результат : прошел";
                    }
                    else
                    {
                        result = false;
                        return "Результат : не прошел";
                    }
                else
                    return null;
            }
        }

        #endregion

        #region Методы

        public static BigInteger RandomIntegerBelow(BigInteger N)
        {
            byte[] bytes = N.ToByteArray();
            BigInteger R;

            do
            {
                new Random().NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = new BigInteger(bytes);
            } while (R >= N);

            return R;
        }

        #endregion
    }
}

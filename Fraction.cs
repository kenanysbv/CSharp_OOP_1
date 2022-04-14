using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionBase
{
    internal class Riyazi
    {
        static public int Cicik(int num1, int num2) => num1 < num2 ? num1 : num2;
        static public int Boyuk(int num1, int num2) => num1 > num2 ? num1 : num2;
        static public bool IsBolen(int bolunen, int num) => !Convert.ToBoolean(bolunen % num);

        static public bool IsSade(int num)
        {
            if (num == 4)
                return false;
            for (int i = 2; i < (num / 2); i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }

        static public List<int> SadeEdedler(int num)
        {
            List<int> list = new List<int>();
            for (int i = 2; i <= num; i++)
                if (IsSade(i))
                    list.Add(i);
            return list;
        }

        static public List<int> SadeVuruq(int num)
        {
            List<int> sadeEdedler = SadeEdedler(num);
            List<int> vuruqlar = new List<int>();


            for (int i = 0; i < sadeEdedler.Count(); i++)
            {
                if (!Convert.ToBoolean(num % sadeEdedler[i]))
                {
                    num /= sadeEdedler[i];
                    vuruqlar.Add(sadeEdedler[i]);
                    i--;
                }
            }
            return vuruqlar;

        }

        static public List<int> OrtaqVuruqlar(List<int> small, List<int> big)
        {
            List<int> ortaq = new List<int>();
            foreach (int i in big)
            {
                if (small.Contains(i))
                    ortaq.Add(i);
            }
            return ortaq;
        }

        static public void Ixtisar(ref int num1, ref int num2)
        {
            for (int i = Cicik(num1, num2); i > 1; i--)
            {
                if (num1 % i == 0 && num2 % i == 0)
                {
                    num1 /= i;
                    num2 /= i;
                }

            }
        }
        static public Fraction Ixtisar(Fraction fr)
        {
            for (int i = Cicik(fr.Mexrec, fr.Suret); i > 1; i--)
            {
                if (fr.Mexrec % i == 0 && fr.Suret % i == 0)
                {
                    fr.Mexrec /= i;
                    fr.Suret /= i;
                }

            }
            return fr;
        }


        static public int EKOB(int num1, int num2)
        {
            int smallNum = Cicik(num1, num2);
            int bigNum = Boyuk(num2, num1);
            int ekob = bigNum;
            /* EKOB(a,b) = a (bolunen) */
            if (IsBolen(bigNum, smallNum))
                return bigNum;

            /* EKOB(a,b) =  c */
            var smallList = SadeVuruq(smallNum);
            var bigList = SadeVuruq(bigNum);
            List<int> ortaq = OrtaqVuruqlar(smallList, bigList);

            /* Find Different */
            foreach (int i in ortaq)
                smallList.Remove(i);


            foreach (int i in smallList)
                ekob *= i;

            return ekob;

        }
    }


    internal class Fraction
    {

        private int mexrec;
        private int suret;

        public int Suret
        {
            get => suret;
            set => suret = value == 0 ? 1 : value;
        }
        public int Mexrec
        {
            get => mexrec;
            set => mexrec = value == 0 ? 1 : value;
        }

        public override string ToString() => $"{suret}\n-\n{mexrec}";

        public Fraction(int _suret, int _mexrec) { mexrec = _mexrec; suret = _suret; }
        #region Funcs

        /* Riyazi Dusturlar */

        public void Show() => Console.WriteLine($"{suret}\n-\n{mexrec}");

        static public int OrtaqMexrec(Fraction first, Fraction second) => first.mexrec == second.mexrec ? first.mexrec : Riyazi.EKOB(first.mexrec, second.mexrec);


        /* Kesr uzerinde emeller */
        static public Fraction Add(Fraction first, Fraction second)
        {
            int ortaq = OrtaqMexrec(first, second);
            first.suret *= ortaq / first.mexrec;
            second.suret *= ortaq / second.mexrec;
            first.suret += second.suret;
            first.mexrec = ortaq;
            second.mexrec = ortaq;
            Riyazi.Ixtisar(first);
            return first;

        }

        static public Fraction Sub(Fraction first, Fraction second)
        {
            int ortaq = OrtaqMexrec(first, second);
            first.suret *= ortaq / first.mexrec;
            second.suret *= ortaq / second.mexrec;
            first.suret -= second.suret;
            first.mexrec = ortaq;
            second.mexrec = ortaq;
            Riyazi.Ixtisar(first);

            return first;
        }

        static public Fraction Mult(Fraction first, Fraction second) => Riyazi.Ixtisar(new Fraction((first.suret * second.suret), (first.mexrec * second.mexrec)));

        static public Fraction Div(Fraction first, Fraction second)
        {
            int temp = second.mexrec;
            second.mexrec = second.suret;
            second.suret = temp;
            return Mult(first, second);
        }

        #endregion



    }
}

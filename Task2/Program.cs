using System.IO;
using System.Threading;

namespace Task2
{
    /*
     Створіть консольний додаток, який в різних потоках зможе отримати доступ до двох файлів. 
    Зчитайте з цих файлів їх вміст і спробуйте записати отриману інформацію в третій файл. 
    Читання/запис повинні здійснюватися одночасно в кожному з дочірніх потоків. 
    Використовуйте блокування потоків для того, щоб домогтися коректного запису в кінцевий файл.
     */
    class Program
    {
        static readonly StreamReader stream1 = File.OpenText("text1.txt");
        static readonly StreamReader stream2 = File.OpenText("text2.txt");
        static readonly StreamWriter stream3 = File.CreateText("text3.txt");

        static object blok = new object();

        static void ReadText1()
        {
            string str = stream1.ReadToEnd();

            lock (blok)
            {
                stream3.WriteLine(str);
            }
        }

        static void ReadText2()
        {
            string str = stream2.ReadToEnd();

            lock (blok)
            {
                stream3.WriteLine(str);
            }
        }

        static void Main()
        {
            Thread[] array = new Thread[] { new Thread(ReadText1), new Thread(ReadText2) };

            for (int i = 0; i < array.Length; i++)
            {
                array[i].Start();
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i].Join();
            }

            stream3.Close();
        }
    }
}
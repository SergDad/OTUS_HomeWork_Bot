using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace RihterCollections
{
    public static class Program
    {
        public static void Main()
        {
            ValueTypePerfТest();
            ReferenceTypePerfТest();
        }
        private static void ValueTypePerfТest()
        {
            const Int32 count = 10000000;
            using (new OperationTimer("List<Int32>"))
            //using (new OperationTimer("List<Int32>"))
            {
                List<Int32>? l = new List<Int32>(count);
                for (Int32 n = 0; n < count; n++)
                {
                    l.Add(n);
                    Int32 х = l[n];
                }
                l = null; // Это должно удаляться в процессе сборки мусора
            }

            using (new OperationTimer("ArrayList of Int32 "))
            {
                ArrayList? a = new ArrayList();
                for (Int32 n = 0; n < count; n++)
                {
                    a.Add(n);
                    Int32 х = (Int32)a[n];
                }
                a = null; // Это должно удаляться в процессе сборки мусора
            }
        }
        private static void ReferenceTypePerfТest()
        {
            const Int32 count = 10000000;
            using (new OperationTimer("List<String>"))
            {
                List<String> l = new List<String>(); //  почему без count?
                for (Int32 n = 0; n < count; n++)
                {
                    l.Add(" X ");
                    String х = l[n];
                }
                l = null; // Это должно удаляться в процессе сборки мусора
            }

            using (new OperationTimer("ArrayList of String "))
            {
                ArrayList a = new ArrayList();
                for (Int32 n = 0; n < count; n++)
                {
                    a.Add(" Х ");
                    String х = (String)a[n];
                }
                a = null; // Это должно удаляться в процессе сборки мусора
            }
        }
    }
    // Это полезный способ оценки времени выполнения алгоритма
    internal sealed class OperationTimer : IDisposable
    {
        private Int64 m_startTime;
        private String m_text;
        private Int32 m_collectionCount;

        public OperationTimer(String text)
        {
            PrepareForOperation();
            m_text = text;
            m_collectionCount = GC.CollectionCount(0);
            // Это выражение должно быть последним в этом методе.
            // чтобы обеспечить максимально точную оценку быстродействия
            m_startTime = Stopwatch.GetTimestamp();
        }
        public void Dispose()
        {
            Console.WriteLine(" {0,8:#.000000} seconds ( GCs={1,3} ) {2} ", (Stopwatch.GetTimestamp() - m_startTime) / (Double)Stopwatch.Frequency, GC.CollectionCount(0) - m_collectionCount, m_text);
        }
        private static void PrepareForOperation()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


    }
}
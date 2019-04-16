using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB6DS
{

    class Program
    {
        static Random rand = new Random();

        static int[] RandArray(int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = i;
            }
            array = array.OrderBy(x => rand.Next()).ToArray();
            return array;
        }

        static void InsertElements(int[] array, Tree tree)
        {
            for (int i = 0; i < array.Length; i++)
            {
                tree.Add(array[i]);
            }
        }

        static double AverageDepth(int[] elements, Tree tree)
        {
            int sum = 0;

            foreach (var element in elements)
            {
                sum += tree.DepthOf(element);
            }
            return sum / elements.Length;
        }


        static void CompareAverage(int elements)
        {
            RBTree rb = new RBTree();
            AVLTree avl = new AVLTree();
            var array = RandArray(elements);
            InsertElements(array, rb);
            InsertElements(array, avl);
            var randElements = RandArray(elements).Take(elements / 10).ToArray();
            double averageRB = AverageDepth(randElements, rb);
            double averageAVL = AverageDepth(randElements, avl);
            Console.WriteLine($"Average depth of RB with {elements} element is {averageRB}");
            Console.WriteLine($"Average depth of AVL with {elements} element is {averageAVL}");
        }

        static void CompareMax(int elements)
        {
            RBTree rb = new RBTree();
            AVLTree avl = new AVLTree();
            var array = RandArray(elements);
            InsertElements(array, rb);
            InsertElements(array, avl);
            Console.WriteLine($"Max depth of RB with {elements} element is {rb.MaxDepth()}");
            Console.WriteLine($"Max depth of AVL with {elements} element is {avl.MaxDepth()}");
        }

        static void Test()
        {
            RBTree rb = new RBTree();
            AVLTree avl = new AVLTree();
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            InsertElements(array, rb);
            InsertElements(array, avl);
            Console.WriteLine($"Max depth of RB with {array.Length} element is {rb.MaxDepth()}");
            Console.WriteLine($"Max depth of AVL with {array.Length} element is {avl.MaxDepth()}");
        }

        static void Main(string[] args)
        {
            //for (int i = 1000; i <= 10000; i += 1000)
            //{
            //    CompareMax(i);
            //    Console.WriteLine("==========================================");
            //}
            Test();
            Console.ReadLine();
        }
    }
}

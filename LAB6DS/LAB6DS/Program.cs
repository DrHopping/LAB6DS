using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB6DS
{

    class Program
    {
        //static Random rand = new Random();

        //static int[] RandArray(int length)
        //{
        //    int[] array = new int[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        array[i] = i;
        //    }
        //    array = array.OrderBy(x => rand.Next()).ToArray();
        //    return array;
        //}

        //static void InsertElements(int[] array, Tree tree)
        //{
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        tree.Add(array[i]);
        //    }
        //}

        //static double AverageDepth(int count, Tree tree)
        //{
        //    int sum = 0;

        //    for (int i = 0; i < count; i++)
        //        sum += tree.DepthOf(rand.Next(count * 10));

        //    return sum / count;
        //}


        //static void Compare(int elements)
        //{
        //    RBTree rb = new RBTree();
        //    AVLTree avl = new AVLTree();
        //    var array = RandArray(elements);
        //    InsertElements(array, rb);
        //    InsertElements(array, avl);
        //    double averageRB = AverageDepth(elements / 10, rb);
        //    double averageAVL = AverageDepth(elements / 10, avl);
        //    Console.WriteLine($"Average depth of RB with {elements} element is {averageRB}");
        //    Console.WriteLine($"Average depth of AVL with {elements} element is {averageAVL}");
        //}

        static void Main(string[] args)
        {
            //Compare(1000);
            RBTree tree = new RBTree();
            tree.Insert(1);
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(5);
            tree.Insert(4);
            tree.Insert(3);
            tree.Insert(2);
            tree.DisplayTree();
            Console.ReadLine();
        }
    }
}

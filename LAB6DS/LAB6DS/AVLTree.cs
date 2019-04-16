using System;

namespace LAB6DS
{
    class AVLTree : Tree
    {
        public class Node
        {
            public int data;
            public int height;
            public Node left, right;

            public Node(int data = 0)
            {
                this.data = data;
                this.height = 1;
            }
        }

        public Node root;

        public int Heigth(Node element)
        {
            if (element != null) return element.height;
            return 0;
        }

        public int BalanceFactor(Node element)
        {
            return Heigth(element.right) - Heigth(element.left);
        }

        public void CalcHeight(Node element)
        {
            int hLeft = Heigth(element.left);
            int hRight = Heigth(element.right);
            element.height = (hLeft > hRight ? hLeft : hRight) + 1;
        }

        public Node leftRotare(Node q)
        {
            Node p = q.right;

            q.right = p.left;
            p.left = q;

            CalcHeight(q);
            CalcHeight(p);

            return p;
        }

        public Node rightRotate(Node p)
        {
            Node q = p.left;
            p.left = q.right;
            q.right = p;

            CalcHeight(p);
            CalcHeight(q);

            return q;
        }

        public Node Balancing(Node element)
        {
            CalcHeight(element);
            if (BalanceFactor(element) == 2)
            {
                if (BalanceFactor(element.right) < 0)
                    element.right = rightRotate(element.right);
                return leftRotare(element);
            }
            else if (BalanceFactor(element) == -2)
            {
                if (BalanceFactor(element.left) > 0)
                    element.left = leftRotare(element.left);
                return rightRotate(element);

            }
            return element;
        }

        public Node Insert(Node element, int data)
        {
            if (element == null)
                return new Node(data);

            if (data < element.data)
                element.left = Insert(element.left, data);
            else
                element.right = Insert(element.right, data);
            return Balancing(element);
        }

        public override void Add(int data)
        {
            root = Insert(root, data);
        }

        public Node FindLeft(Node element)
        {
            if (element.left != null) return FindLeft(element.left);
            return element;
        }

        public Node RemoveMin(Node element)
        {
            if (element.left == null)
                return element.right;
            element.left = RemoveMin(element.left);
            return Balancing(element);
        }

        public Node Remove(Node element, int data)
        {
            if (element == null) return null;
            if (data < element.data)
                element.left = Remove(element.left, data);
            else if (data > element.data)
                element.right = Remove(element.right, data);
            else
            {
                Node q = element.left;
                Node r = element.right;

                if (r == null) return q;

                Node min = FindLeft(r);
                min.left = RemoveMin(r);
                min.left = q;
                return Balancing(min);
            }
            return Balancing(element);
        }

        public void Delete(int data)
        {
            root = Remove(root, data);
        }

        public void Infix(Node top)
        {
            if (top == null) return;
            Infix(top.left);
            System.Console.Write(top.data + " ");
            Infix(top.right);
        }

        public void Prefix(Node top)
        {
            if (top == null) return;
            System.Console.Write(top.data + " ");
            Prefix(top.left);
            Prefix(top.right);
        }

        public void Postfix(Node top)
        {
            if (top == null) return;
            Postfix(top.left);
            Postfix(top.right);
            System.Console.Write(top.data + " ");
        }

        public bool Find(int data)
        {
            Node p = root;
            while (p != null)
            {
                if (data < p.data) p = p.left;
                if (data > p.data) p = p.right;
                if (p.data == data) return true;
            }
            return false;
        }

        public int GetWidth(Node top, int level)
        {
            if (top == null) return 0;
            if (level == 1) return 1;
            else if (level > 1)
                return GetWidth(top.left, level - 1) + GetWidth(top.right, level - 1);
            return 0;
        }

        public override int DepthOf(int n)
        {
            int count = 0;
            if (root == null)
                return 0;

            if (root.data == n)
                return count;

            return ++count + DepthOf(n, ((root.data > n) ? root.left : root.right));
        }

        private int DepthOf(int n, Node node)
        {
            int count = 0;
            if (node == null)
                return 0;

            if (node.data == n)
                return count;

            return ++count + DepthOf(n, ((node.data > n) ? node.left : node.right));

        }

        public override int MaxDepth()
        {
            if (root == null) return 0;
            return Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;
        }

        private int MaxDepth(Node node)
        {
            if (node == null) return 0;
            return Math.Max(MaxDepth(node.left), MaxDepth(node.right)) + 1;
        }
    }
}

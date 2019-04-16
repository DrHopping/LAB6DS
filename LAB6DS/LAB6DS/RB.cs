using System;

namespace LAB6DS
{
    public class RBTree : Tree
    {
        private const bool RED = true;

        private const bool BLACK = false;

        internal class Node
        {
            public int data;
            internal bool Color;
            internal Node left;
            internal Node right;

            public Node(int data)
            {
                this.data = data;
                this.Color = RED;
                this.left = null;
                this.right = null;
            }
        }

        private Node root;

        internal Node Root => this.root;

        public int Search(int data)
        {
            Node r = this.root;
            while (r != null)
            {
                int cmp = r.data.CompareTo(data);

                if (cmp == 0)
                {
                    return r.data;
                }

                if (cmp < 0)
                {
                    r = r.left;
                }
                else
                {
                    r = r.right;
                }
            }

            return data;
        }

        public override void Add( int data)
        {
            this.root = RBTree.Insert(this.root, data);
            this.root.Color = BLACK;
        }

        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree!");
                return;
            }
            if (root != null)
            {
                InOrderDisplay(root);
            }
        }

        private void InOrderDisplay(Node current)
        {
            if (current != null)
            {
                InOrderDisplay(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplay(current.right);
            }
        }

        public void DeleteMin()
        {
            Node deletedNode;
            this.root = RBTree.DeleteMin(this.root, out deletedNode);
            if (this.root != null)
            {
                this.root.Color = BLACK;
            }
        }

        public void Delete(int data)
        {
            this.root = RBTree.Delete(this.root, data);
            if (root != null)
            {
                this.root.Color = BLACK;
            }
        }

        private static Node Insert(Node p, int data)
        {
            if (p == null)
            {
                return new Node(data);
            }

            if (RBTree.IsRed(p.left) && RBTree.IsRed(p.right))
            {
                RBTree.FlipColors(p);
            }

            int cmp = data.CompareTo(p.data);
            if (cmp == 0)
            {
                p.data = data;
            }

            if (cmp < 0)
            {
                p.left = RBTree.Insert(p.left, data);
            }
            else
            {
                p.right = RBTree.Insert(p.right, data);
            }

            if (RBTree.IsRed(p.right) && !RBTree.IsRed(p.left))
            {
                p = RBTree.RotateLeft(p);
            }

            if (RBTree.IsRed(p.left) && RBTree.IsRed(p.left.left))
            {
                p = RBTree.RotateRight(p);
            }

            return p;
        }

        private static Node Delete(Node p, int data)
        {
            if (p == null)
            {
                return null;
            }

            if (data.CompareTo(p.data) < 0)
            {
                if (p.left == null)
                {
                    return p;
                }

                if (!RBTree.IsRed(p.left) && p.left != null && !RBTree.IsRed(p.left.left))
                {
                    p = RBTree.MoveRedLeft(p);
                }

                p.left = RBTree.Delete(p.left, data);
                return RBTree.Fix(p);
            }

            // Right side of tree
            if (RBTree.IsRed(p.left))
            {
                p = RBTree.RotateRight(p);
            }

            if (data.CompareTo(p.data) == 0 && p.right == null)
            {
                return null;
            }

            if (!RBTree.IsRed(p.right) && p.right != null && !RBTree.IsRed(p.right.left))
            {
                p = RBTree.MoveRedRight(p);
            }

            if (data.CompareTo(p.data) == 0)
            {
                Node deletedNode;
                p.right = RBTree.DeleteMin(p.right, out deletedNode);
                if (deletedNode == null)
                {
                    throw new ArgumentOutOfRangeException("Delete min node was null");
                }

                p.data = deletedNode.data;
                p.data = deletedNode.data;
            }
            else
            {
                p.right = RBTree.Delete(p.right, data);
            }

            return RBTree.Fix(p);
        }

        private static Node DeleteMin(Node p, out Node deleted)
        {
            if (p == null)
            {
                deleted = null;
                return null;
            }

            if (p.left == null)
            {
                deleted = p;
                return null;
            }

            if (!RBTree.IsRed(p.left) && !RBTree.IsRed(p.left.left))
            {
                p = RBTree.MoveRedLeft(p);
            }

            p.left = RBTree.DeleteMin(p.left, out deleted);
            return RBTree.Fix(p);
        }

        private static Node Fix(Node p)
        {
            if (RBTree.IsRed(p.right))
            {
                p = RBTree.RotateLeft(p);
            }

            if (RBTree.IsRed(p.left) && RBTree.IsRed(p.left.left))
            {
                p = RBTree.RotateRight(p);
            }

            if (RBTree.IsRed(p.left) && RBTree.IsRed(p.right))
            {
                RBTree.FlipColors(p);
            }

            return p;
        }

        private static Node MoveRedLeft(Node p)
        {
            RBTree.FlipColors(p);
            if (RBTree.IsRed(p.right.left))
            {
                p.right = RBTree.RotateRight(p.right);
                p = RBTree.RotateLeft(p);
                RBTree.FlipColors(p);
            }

            return p;
        }

        private static Node MoveRedRight(Node p)
        {
            RBTree.FlipColors(p);
            if (RBTree.IsRed(p.left.left))
            {
                p = RBTree.RotateRight(p);
                RBTree.FlipColors(p);
            }

            return p;
        }

        private static void FlipColors(Node p)
        {
            p.Color = !p.Color;
            p.left.Color = !p.left.Color;
            p.right.Color = !p.right.Color;
        }

        private static Node RotateLeft(Node p)
        {
            var tmp = p.right;
            p.right = tmp.left;
            tmp.left = p;
            tmp.Color = p.Color;
            p.Color = RED;
            return tmp;
        }

        private static Node RotateRight(Node p)
        {
            var tmp = p.left;
            p.left = tmp.right;
            tmp.right = p;
            tmp.Color = p.Color;
            p.Color = RED;
            return tmp;
        }

        private static bool IsRed(Node p)
        {
            if (p == null)
            {
                return false;
            }

            return p.Color == RED;
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

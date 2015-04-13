using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15puzz
{
    class node
    {
        public List<node> ends(node n)
        {
            var a = new List<node>();
            if (n.children.Count == 0)
                a.Add(n);
            else
                n.children.ForEach(x => { a = a.Union(ends(x)).ToList(); });
            return a;

        }
        public dynamic data;
        public List<node> children;
        public node parent;
        public node(dynamic data, node parent = null)
        {
            this.data = data;
            this.parent = parent;
            children = new List<node>();
        }
        public int addChilderen(node n)
        {
            this.children.Add(n);
            n.parent = this;
            return 0;
        }
    }
    
    static class Program
    {
        static int dd = 1;
        static int[] arr;
        static void Main(string[] args)
        {
            //var max = 0;
            //arr = new int[,] {{11,6,0,8},{15,4,12,7},{5,9,3,2},{1,14,10,13}};            
            arr = new int[] { 11, 6, 0, 8, 15, 4, 12, 7, 5, 9, 3, 2, 1, 14, 10, 13 };
            //arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 10, 11, 13, 14, 15, 0 };
           // arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 15 };
            //arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 0, 12, 13, 10, 14, 15 };
           // arr = new int[] { 0,5,6,12,11,4,7,8,14,1,3,2,9,13,15,10};
            display();
            Console.Title = arr.h2().ToString();
            while (arr.bait() > 0)
            {
                var no = new node(new { a = arr });
                for (int i = 0; i < 15; i++)
                {
                    foreach (node n in no.ends(no))
                    {
                        // Console.ReadKey();
                        List<int> la = new List<int>();
                        
                        if (n.parent != null)
                            la.Add(posof((int[])n.parent.data.a, 0));
                        List<int> opp = nb(posof(new List<int>(n.data.a).ToList().ToArray(), 0)).Except(la).ToList();

                        var ed = opp.Select(x => new { x, a = move(x, new List<int>(n.data.a).ToList().ToArray()) }).ToList();
                        ed.ForEach(x => { n.addChilderen(new node(new { a = new List<int>(x.a).ToList().ToArray(), x.x })); });
                        //arr = move(ed.Where(x => x.a.h2() == ed.Min(y => y.a.h2())).OrderBy(x => Guid.NewGuid()).FirstOrDefault().x, arr);
                        //Console.Title = arr.h2().ToString();
                        var d = n.ends(n).Where(x=>(int [])x.data.a.bait() == n.ends(n).Min(y=>(int [])y.data.a.bait()));
                    }
                    //display();
                }
                var ki = no.ends(no).Select(x => x).OrderBy(x => ((int[])x.data.a).bait()).First();
                var vv = ki;
                var moves = new List<int>();
                while (vv.parent != null)
                {
                    moves.Add(vv.data.x);
                    vv = vv.parent;
                }
                display();
                moves.Reverse();
                moves.ForEach(x => { arr = move(x, arr); display(); Console.ReadKey(); Console.Title = arr.bait().ToString(); });
                if (arr.bait() == 0)
                    dd++;                
            }
            Console.WriteLine("done");
            Console.ReadKey();            
        }
        static int bait(this int[] arr)
        {
            int n = 0;
            for (int i = 0; i < 16; i++)
            {
                if (dd >= 1 && i < 4)
                {
                    if (arr[i] > 0)
                        n += dist(i, arr[i] - 1);
                }
                if(dd  >= 2 && i%4 == 0)
                    if (arr[i] > 0)
                        n += dist(i, arr[i] - 1);
                if (dd >= 3)
                    if (arr[i] > 0)
                        n += dist(i, arr[i] - 1);
            }
            if (posof(arr,0) != 15)
                n++;
            return n;        
        }
        static int[] move(int pos, int[] a)
        {
            a = a.ToArray();
            var mb = pos.nb().Zip(pos.nb().val(a), (i,x)=>new {i,x}).ToList();
            var hel = pos.nb().val(a);
            if (a[pos] != 0 && hel.Contains(0))
            {
                a[posof(a,0)] = a[pos];
                a[pos] = 0;                
            }
            else
                throw new Exception("not possible to move");
            return a;
        }
        static int val(this int pos, int[] arr)
        {
            return arr[pos];
        }
        static List<int> val(this List<int> pos, int[] arr)
        {
            return pos.Select(x=>val(x,arr)).ToList();
        }
        static List<int> nb(this int pos)
        {            
            var a = new List<int>();            
            if (pos % 4 != 0) //bal
                a.Add(pos - 1);
             if(pos % 4 != 3) //jobb
                a.Add(pos+1);
             if (pos / 4 != 0) //fel
                 a.Add(pos - 4);
             if (pos / 4 != 3) //le
                 a.Add(pos + 4);
             return a;
        }
        static int posof(int[] arr, int a)
        {
            int pos = 0;
            for (int i = 0; i < 16; i++)
            {
                
                if (arr[i] == a)
                    pos = i;
            }
            return pos;
        }
        static int h1(this int[] arr)
        {
            int n = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (dist(i, arr[i]-1) > 0 && arr[i] > 0)
                    n++;
            }
            return n;
        }
        static int h2(this int[] arr)
        {
            int n = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                    n += dist(i, arr[i] - 1);
            }
            return n;
        }
        static dynamic pos(int a)
        {
            return new {y=a/4, x = a%4 };
        }
        static int dist(int a, int b)
        {
            int n = 0;
            var e = pos(a);
            var f = pos(b);
            n += Math.Abs(e.x - f.x);
            n += Math.Abs(e.y - f.y);
            return n;
        }
        static void display()
        {
            Console.Clear();
            for (int i = 0; i < arr.Length; i++)
            {

                Console.Write((arr[i] == 0? " " : arr[i].ToString())+"\t");
                if(i%4 == 3)
                    Console.WriteLine("\n");
            }
        }
    }
}

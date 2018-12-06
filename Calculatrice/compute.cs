using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq.Expressions;
using System.Threading;

namespace Calculatrice
{
    class Compute
    {
        /*
        * @pre expression != null
        * expression [i] == ’+’, ’-’, ’*’, ’/’ or is an integer
        * @post The returned value contains the evaluation of the specified postfix expression
        * or 0 if it is empty
        */
        public static int DoCompute(params string[] expe)
        {
            var ope = new List<string>
            {
                "+",
                "-",
                "*",
                "/"
            };

            var groupe = new Dictionary<string, string>()
            {
                {"(",")"}
            };

            Stack<int> value = new Stack<int>();
            Stack stack_ope = new Stack();
            Stack stack_groupe = new Stack();
            int result = 0;

            for (int i = 0; i < expe.Length; i++)
            {
                string elem = expe[i];
                for (int x = 0; x < elem.Length; x++)
                {
                    try
                    {
                        int a = int.Parse(elem[x].ToString());
                        value.Push(a);
                    }
                    catch (FormatException)
                    {
                        if (ope.Contains(elem[x].ToString()))
                        {
                            stack_ope.Push(elem[x].ToString());
                        }

                        else if (groupe.ContainsKey(elem[x].ToString()))
                        {
                            stack_groupe.Push(elem[x]);
                        }

                        else if (groupe.ContainsValue(elem[x].ToString()))
                        {
                            var last = stack_groupe.Peek().ToString();

                            if (elem[x].ToString() == groupe[last])
                            {
                                int rep = Calcul(value, stack_ope);
                                value.Push(rep);
                            }
                        }
                    }
                }
                value.Push(Calcul(value, stack_ope));

            }
            result += value.Peek();
            return result;
        }

        public static int Calcul(Stack<int> value, Stack stack_ope)
        {
            int result = 0;
            int y = value.Peek();
            value.Pop();
            int z = value.Peek();
            value.Pop();

            string calcul = stack_ope.Peek().ToString();

            if (calcul == "+")
            {
                result = z + y;
            }
            else if (calcul == "*")
            {
                result = z * y;
            }
            else if (calcul == "-")
            {
                result = z - y;
            }
            else if (calcul == "/")
            {
                result = z / y;
            }
            return result;
        }
    }
}

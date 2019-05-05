using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculator
{
    public partial class Calculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/error.html");
        }

        protected void Button_Click_Num_Op(object sender, EventArgs e)
        {
            TextBox1.Text = TextBox1.Text + (sender as Button).Text;
        }

        protected void Button_Click_C(object sender, EventArgs e)
        {
            TextBox1.Text = "";
        }

        protected void Button_Click_CE(object sender, EventArgs e)
        {
            TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - 1);
        }

        protected void Button_Click_Enter(object sender, EventArgs e)
        {
            Calculate test = new Calculate(TextBox1.Text);
            System.Diagnostics.Debug.WriteLine(test.calculation);
            
            //check whether the input format might cause an error
            if (test.checkInput())  //if the input format is ok
            {
                System.Diagnostics.Debug.WriteLine(test.calculation);
                string result = test.executeCalculation();
                TextBox1.Text = result;
            }
            else  //if the input format might cause an error, pop up alert
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Please recheck your input!');</script>");
            }
        }
    }

    public class Calculate
    {
        public struct data
        {
            public int number;
            public char operator_sign;
        };

        public String calculation;
        public int index;
        Stack<data> operator_signs = new Stack<data>();
        Queue<data> postfix = new Queue<data>();

        public Calculate(String calculation)
        {
            this.calculation = calculation;
            index = 0;
        }

        //check whether the input format might cause an error
        public bool checkInput()
        {
            int open = 0;
            
            //first character of the input have to be a number, '(', or '-'
            if (isOperator(calculation[0]))
            {
                if (calculation[0] == '-')
                {
                    calculation = calculation.Insert(0, "0");
                    return true;
                }
                else  if (calculation[0] != '(') return false;
            }

            //last character of the input have to be a number or ')'
            if (isOperator(calculation[calculation.Length-1]) && calculation[calculation.Length - 1] != ')') return false;

            //if the first and last character is ok, check each character one by one
            for (int i=0; i<calculation.Length; i++)
            {
                if(calculation[i] == '(')
                {
                    //character before '(' have to be an operator but not a ')'
                    if (i > 0)
                    {
                        if (!isOperator(calculation[i - 1]) || calculation[i - 1] == ')') return false;
                    }

                    //character after '(' have to be a number, '(' or '-', if it's a '-' then add '0' before it
                    if (isOperator(calculation[i + 1]) && calculation[i+1] != '-' && calculation[i + 1] != '(') return false;
                    if (calculation[i + 1] == '-') calculation = calculation.Insert(i + 1, "0");

                    //count the number of '(' to check if it's have a ')' couple
                    open += 1;
                }

                else if(calculation[i] == ')')
                {
                    //if there's ')' before '(' return false
                    if (open == 0) return false;

                    //character before ')' have to be a number or ')'
                    if (isOperator(calculation[i-1]) && calculation[i - 1] != ')') return false;

                    //character after ')', but not last character, have to be an operator
                    if (i < calculation.Length - 1 && !isOperator(calculation[i+1])) return false;

                    open -= 1;
                }
            }

            //if every '(' has a couple ')' return true else return false
            if (open == 0) return true;
            else return false;
        }

        //return true if char c is either '(', ')', '+', '-', 'x', ':'
        public bool isOperator(char c)
        {
            if (c == '(' || c == ')' || c == 'x' || c == ':' || c == '+' || c == '-')
            {
                return true;
            }
            else return false;
        }

        //return true if data d is an operator
        public bool isOperatorData(data d)
        {
            if (d.number == -1)
            {
                return true;
            }
            else return false;
        }

        //return the next number
        public int getNextNumber()
        {
            int next = (int)Char.GetNumericValue(calculation[index]);
            index += 1;

            while (index<calculation.Length && !isOperator(calculation[index]))
            {
                next *= 10;
                next += (int)Char.GetNumericValue(calculation[index]);
                index += 1;
            }

            return next;
        }

        //return the next data
        public data getNextData()
        {
            data next;
            if (isOperator(calculation[index]))
            {
                next.number = -1;
                next.operator_sign = calculation[index];
                index += 1;
            }
            else
            {
                next.number = getNextNumber();
                next.operator_sign = '~';
            }
            return next;
        }

        //return the priority of an operator, the higher the number, the higher the priority
        public int getPriority(char operator_sign)
        {
            if (operator_sign == '-' || operator_sign == '+')
            {
                return 1;
            }
            else if (operator_sign == 'x' || operator_sign == ':')
            {
                return 2;
            }
            else return 3;
        }

        //check priority of a data and process it to a stack for calculating process
        public void checkPriority(data d)
        {
            if (operator_signs.Count == 0)
            {
                operator_signs.Push(d);
            }
            else if (d.operator_sign == ')')
            {
                popBetweenParenthesis();
            }
            else if (getPriority(d.operator_sign) > getPriority(operator_signs.Peek().operator_sign))
            {
                operator_signs.Push(d);
            }
            else
            {
                if (operator_signs.Peek().operator_sign != '(')
                {
                    postfix.Enqueue(operator_signs.Pop());
                    checkPriority(d);
                }
                else
                {
                    operator_signs.Push(d);
                }
            }
        }

        //pop all operator between a brackets
        public void popBetweenParenthesis()
        {
            data temp = operator_signs.Pop();
            System.Diagnostics.Debug.WriteLine(temp.number + " " + temp.operator_sign);
            while (temp.operator_sign != '(')
            {
                postfix.Enqueue(temp);
                temp = operator_signs.Pop();
                System.Diagnostics.Debug.WriteLine(temp.number + " " + temp.operator_sign);
            }
        }

        //return the reslut calculation of operand1 operator_ operand2
        public data calculate(data operand2, data operand1, data operator_)
        {

            data result;
            result.operator_sign = '~';

            if (operator_.operator_sign == '+')
            {
                result.number = operand1.number + operand2.number;
            }
            else if (operator_.operator_sign == '-')
            {
                result.number = operand1.number - operand2.number;
            }
            else if (operator_.operator_sign == 'x')
            {
                result.number = operand1.number * operand2.number;
            }
            else
            {
                result.number = operand1.number / operand2.number;
            }
            return result;
        }

        //transform a string to a postfix to examine the priority of operand
        public void toPostFix()
        {
            //get every data from the calculation
            while (index < calculation.Length)
            {
                data next = getNextData();
                System.Diagnostics.Debug.Write(next.number + " ");
                System.Diagnostics.Debug.WriteLine(next.operator_sign);

                //if the data is a number, add it to the queue
                if (!isOperatorData(next))
                {
                    postfix.Enqueue(next);
                }

                //if the data is an operator, check it's priority
                else
                {
                    checkPriority(next);
                }
            }

            //if there's any operator in the stack, pop it all and add to the postfix queue
            while (operator_signs.Count != 0)
            {
                postfix.Enqueue(operator_signs.Pop());
            }
            foreach (data d in postfix) System.Diagnostics.Debug.WriteLine(d.number + " " + d.operator_sign);
        }

        //execute calculation
        public string executeCalculation()
        {
            data temp;
            data result;
            Stack<data> numbers = new Stack<data>();

            toPostFix();

            while (postfix.Count != 0)
            {
                temp = postfix.Dequeue();
                if (!isOperatorData(temp))
                {
                    numbers.Push(temp);
                }
                else
                {
                    result = calculate(numbers.Pop(), numbers.Pop(), temp);
                    numbers.Push(result);
                }
            }

            result = numbers.Pop();
            return result.number.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class improveString
    {
        private string prosses = string.Empty;

        public string Endimprove 
        {
            get { return this.prosses; }
        }
        private bool isSimple(string str)
        {
            if(str.Contains("c") || str.Contains("s") || str.Contains("t") || str.Contains("e") || str.Contains("π"))
            {
                return false;
            }
            return true;
        }
        private string manageMaltip(string str)
        {
            Queue<char> builder = new Queue<char>();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] == 'e' || str[i] == 'π' || str[i] == 'c' || str[i] == 's' 
                    || str[i] == 't' || str[i] == 'l' || str[i] == '√' || str[i] == '∛') 
                    && i !=0){
                    if (char.IsDigit(str[i - 1]) || str[i - 1] == ')' || str[i - 1] ==']')
                    {
                        builder.Enqueue('*');
                    }    
                }
                builder.Enqueue(str[i]);
            }
            return new string(builder.ToArray());
        }
        public string removeSpacse(string str)
        {
            return str.Replace(" ", "");
        }
       public improveString(string str)
        {
            if (!isSimple(str))
            {
                prosses = manageMaltip(str);
                prosses = removeSpacse(prosses);
            }
            else 
            {
                prosses = removeSpacse(str);
            }
        }
    }

}

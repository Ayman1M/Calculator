using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class rewriteString
    {
        private string truns =string.Empty;
        public string pyCode
        {
            get {  return truns; }
        }
        private string BStrWrapper(string str)
        {
            str = str.Replace("(", "((" );
            str = str.Replace(")", "))");
            return  str;

        }
        private string traingleFunc(string str ,bool isdegree)
        {
            string toRadian ="(math.pi/180)*";
            if (isdegree)
            {
                str = str.Replace("sin(", "math.sin(" + toRadian);

                str = str.Replace("cos(", "math.cos(" + toRadian);

                str = str.Replace("tan(", "math.tan(" + toRadian);

            }
            else
            {
                str = str.Replace("sin(", "math.sin(");

                str = str.Replace("cos(", "math.cos(");

                str = str.Replace("tan(", "math.tan(");  
            }

            str = str.Replace("sin^-1(", "math.asin(");

            str = str.Replace("cos^-1(", "math.acos(");

            str = str.Replace("tan^-1(", "math.atan(");
            return str;
        }
        private string constExpress(string str)
        {
            str = str.Replace("π", "math.pi");

            str = str.Replace("e", "math.e");

            return str;
        }
        private string Logarithms(string str)
        {
            str = str.Replace("log[", "math.log10(");

            str = str.Replace("ln[", "math.log(");

            str = str.Replace("]", ")");

            return str;
        }
        private string powers(string str)
        {
            str = str.Replace("√(", "math.sqrt(");

            str = str.Replace("^", "**");

            return str;
        }
        public rewriteString(string str , bool isdegree)
        {
            truns = BStrWrapper(str);

            truns = traingleFunc(truns, isdegree);

            truns = constExpress(truns);

            truns = Logarithms(truns);

            truns = powers(truns);

        }
    }
}

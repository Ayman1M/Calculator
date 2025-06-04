using System;
using System.Collections.Generic;
using System.Linq; // للمزيد من المرونة مع LINQ إذا احتجت لاحقاً

namespace Calculator
{
    // اسم الكلاس أصبح أكثر وضوحاً
    internal class ExpressionValidator
    {
        private bool isValid; // اسم الحقل أوضح

        // تم تغيير اسم الدالة ليعكس وظيفتها بشكل أفضل
        // تعيد 'صحيح' إذا كان النص غير فارغ
        private bool IsNotEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        // اسم الدالة يشير إلى أنها تتحقق من الاستخدام الصحيح للثوابت
        // تعيد 'صحيح' إذا كان استخدام الثوابت (e, π) صحيحاً
        // (أي ليست متبوعة مباشرة برقم)
        private bool IsValidConstantUsage(string str)
        {
            // يمكن استخدام char.IsDigit() للتحقق من الأرقام
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == 'e' || str[i] == 'π' || str[i] == ')' || str[i] == ']')
                {
                    if (char.IsDigit(str[i + 1]))
                    {
                        return false; // استخدام غير صالح، مثل "e5" أو "π3" ")7" "]6"
                    }
                }
            }
            return true; // لم يتم العثور على استخدامات غير صالحة
        }

        // اسم الدالة يشير إلى التحقق من بداية ونهاية التعبير
        // تعيد 'صحيح' إذا كانت بداية ونهاية التعبير صالحة
        private bool HasValidStartAndEnd(string str)
        {
            // التأكد من أن السلسلة ليست فارغة قبل الوصول إلى str[0]
            if (string.IsNullOrEmpty(str)) return false; // أو حسب منطق isNotEmpty

            string[] invalidEndings = {
              "(", "()", "[", "[]", "." , " " // المسافة في النهاية تعتبر غير صالحة
            };

            foreach (string op in invalidEndings)
            {
                if (str.EndsWith(op))
                    return false;
            }

            char[] invalidBeginnings = {
               '^', ']', ')' , ' ' // المسافة في البداية تعتبر غير صالحة
            };

            // Array.Exists يمكن استبدالها بـ LINQ .Any() أو حلقة بسيطة
            if (Array.Exists(invalidBeginnings, c => str[0] == c))
                return false;

            return true;
        }

        // اسم الدالة يشير إلى التحقق من توازن الأقواس
        // تعيد 'صحيح' إذا كانت الأقواس متوازنة
        private bool AreBracketsBalanced(string str)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char ch in str)
            {
                if (ch == '(' || ch == '[')
                {
                    stack.Push(ch);
                }
                else if (ch == ')' || ch == ']')
                {
                    if (stack.Count == 0) return false; // قوس إغلاق بدون قوس فتح مطابق

                    char openBracket = stack.Pop();
                    if ((ch == ')' && openBracket != '(') || (ch == ']' && openBracket != '['))
                        return false; // عدم تطابق نوع القوس
                }
            }
            return stack.Count == 0; // يجب أن تكون المكدسة فارغة في النهاية
        }

        // اسم الدالة يشير إلى التحقق من عدم وجود أنماط شائعة خاطئة
        // تعيد 'صحيح' إذا لم يتم العثور على أي من الأنماط السيئة المحددة
        private bool ContainsNoForbiddenPatterns(string str)
        {
            // قائمة الأنماط السيئة كما هي لديك، مع مراعاة المسافات
            // ملاحظتك: "الفراغات دي مقصوده لان العمليات زي { , + , - , % , * ,}
            // ودي العمليات الوحيده اللي بيكون فيها فراغ"
            // هذا يعني أنك تتوقع أن تكون هذه العمليات متبوعة بمسافة واحدة (أو محاطة بمسافات)
            // وأن قائمة bad_cases تبحث عن أنماط خاطئة بناءً على هذا الافتراض.
            // مثال: "+  %" (زائد ثم مسافتين ثم %) يعتبر نمطاً خاطئاً.
            string[] badPatterns = {
              "+  %", "+  *", "+ )", "+  /", "+ ^",
              "+ ]" ,
              "-  %", "-  *", "- )", "- .", "-  /", "- ^",
              "- ]" ,
              "*  *", "* )", "*  %","( *", "*  /","* ]",
              "% )", "%  /", "%  %","%  *",
              ".(", ".)", ".[", ".]", ". ^", ".c", ".e", ".l", ".s", ".t", ".π", ".√", ".∛", "..",
              "e." ,"π." , ").",".^",
              "( +", "( -", "()", "( ^",
              ") /", "/ )", "/ %", "/ .", " /  ^", "[]", "]]", "^ )",
              "^  /","(]",
               "[ +", "[ -", "[ *", "[ /", "[ %", "[ ^" , ". +"
             };

            foreach (string bad in badPatterns)
            {
                if (str.Contains(bad))
                    return false; // تم العثور على نمط سيء
            }
            return true; // لم يتم العثور على أي أنماط سيئة
        }

        // اسم الخاصية أصبح أكثر وضوحاً
        public bool IsValid
        {
            get { return this.isValid; }
        }

        public ExpressionValidator(string expression)
        {
            // التحقق من السلسلة الفارغة أولاً لتجنب أخطاء في الدوال الأخرى
            if (!IsNotEmpty(expression))
            {
                this.isValid = false;
                return;
            }

            // ترتيب الاستدعاءات يمكن أن يكون مهماً إذا كانت هناك اعتمادات
            // ولكن هنا كل دالة تعمل بشكل مستقل نسبياً على السلسلة.
            this.isValid =
                IsValidConstantUsage(expression) &&
                HasValidStartAndEnd(expression) && // قد يكون من الأفضل وضعه بعد IsNotEmpty
                AreBracketsBalanced(expression) &&
                ContainsNoForbiddenPatterns(expression);
        }
    }
}
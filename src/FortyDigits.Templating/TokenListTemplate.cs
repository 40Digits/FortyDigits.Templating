using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FortyDigits.Templating
{
    public class TokenListTemplate : ITemplate<Dictionary<string, string>>
    {
        private delegate string ReplaceInvoker(Dictionary<string, string> tokenValues);
        private readonly ReplaceInvoker Replace;

        public TokenListTemplate(DynamicMethod method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            Replace = (ReplaceInvoker)method.CreateDelegate(typeof(ReplaceInvoker));
        }

        public string Render(Dictionary<string, string> values)
        {
            return Replace(values);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP2.Glif.Interpreter
{
    class GlifStatementList : GlifStatement
    {
        private readonly GlifStatement _current;
        private readonly GlifStatement _next;
        
        public GlifStatementList(GlifContext context, GlifStatement currentStatement, GlifStatement nextStatement) : base(context)
        {
            _current = currentStatement;
            _next = nextStatement;
        }

        public override void Execute()
        {
            _current.Execute();
            if(_next != null)
            {
                _next.Execute();
            }
        }
    }
}

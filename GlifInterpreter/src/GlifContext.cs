using System;
using SP2.Glif.Model;
using GoldParser;

namespace SP2.Glif.Interpreter
{
    class GlifContext
    {
        private readonly Parser _parser;
        public GlifWorkflow Workflow { get; private set; }


        public GlifContext(Parser parser)
        {
            _parser = parser;
            Workflow = new GlifWorkflow();
        }

        public GlifObject ExecuteReductionRule()
        {
            switch ((RuleConstants)_parser.ReductionRule.Index)
            {
                case RuleConstants.Start:             //<Start> ::= <Program>
                    return R<GlifStatement>(0);
                case RuleConstants.Program:           //<Program> ::= digraph ID '{' <Lines> '}'
                    return new GlifProgram(this, Token(0), Token(1), R<GlifStatement>(3));
                case RuleConstants.LinesMultiple:     //<Lines> ::= <Line> <Lines>
                    return new GlifStatementList(this, R<GlifStatement>(0), R<GlifStatement>(1));
                case RuleConstants.LinesSingle:       // <Lines> ::= <Line>
                    return R<GlifStatement>(0);
                case RuleConstants.LineNodeParams:    // <Line> ::= ID '[' <Params> ']' ';'
                    return new GlifNodeParamsStatement(this, Token(0), R<GlifParameter>(2));
                case RuleConstants.LineNextNode:      // <Line> ::= ID '->' ID ';'
                    return new GlifNextNodeStatement(this, Token(0), Token(2));
                case RuleConstants.LineVerticeParams: // <Line> ::= ID '->' ID '[' <Params> ']' ';'
                    return new GlifVertexParamsStatement(this, Token(0), Token(2), R<GlifParameter>(4));
                case RuleConstants.ParamsMultiple:    // <Params> ::= <Param> ',' <Params>
                    return new GlifParameterList(this, R<GlifParameter>(0), R<GlifParameter>(2));
                case RuleConstants.ParamsSingle:      // <Params> ::= <Param>
                    return R<GlifParameter>(0);
                case RuleConstants.Param:             // <Param> ::= <Attr> '=' <Value>
                    return new GlifParameter(this, R<GlifAttribute>(0), R<GlifValue>(2));
                case RuleConstants.AttrShape:
                    return new GlifAttribute(this, Token(0));
                case RuleConstants.AttrLabel:
                    return new GlifAttribute(this, Token(0));
                case RuleConstants.AttrColor:
                    return new GlifAttribute(this, Token(0));
                case RuleConstants.ValueString:
                    return new GlifValue(this, Token(0));
                case RuleConstants.ValueBox:
                    return new GlifValue(this, Token(0));
                case RuleConstants.ValueTrue:
                    return new GlifValue(this, Token(0));
                case RuleConstants.ValueFalse:
                    return new GlifValue(this, Token(0));
                case RuleConstants.ValueRed:
                    return new GlifValue(this, Token(0));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetTokenText()
        {
            switch((SymbolConstants)_parser.TokenSymbol.Index)
            {
                case SymbolConstants.BoxValue:
                case SymbolConstants.ColorAttr:
                case SymbolConstants.DigraphType:
                case SymbolConstants.FalseValue:
                case SymbolConstants.ID:
                case SymbolConstants.LabelAttr:
                case SymbolConstants.RedValue:
                case SymbolConstants.ShapeAttr:
                case SymbolConstants.StringLiteral:
                case SymbolConstants.TrueValue:
                    return _parser.TokenText;
            }
            return null;
        }

        private T R<T>(int index) where T : GlifObject
        {
            return (T) _parser.GetReductionSyntaxNode(index);
        }

        private string Token(int index)
        {
            return (string) _parser.GetReductionSyntaxNode(index);
        }
        
        private enum SymbolConstants
        {
            Eof = 0,              // (EOF)
            Error = 1,            // (Error)
            Whitespace = 2,       // (Whitespace)
            Comma = 3,            // ','
            Semicolon = 4,        // ';'
            LeftSqBracket = 5,    // '['
            RightSqBracket = 6,   // ']'
            LeftCurlBracket = 7,  // '{'
            RightCurlBracket = 8, // '}'
            Equals = 9,           // '='
            RightArrow = 10,      // '->'
            BoxValue = 11,        // box
            ColorAttr = 12,       // color
            DigraphType = 13,     // digraph
            FalseValue = 14,      // false
            ID = 15,              // ID
            LabelAttr = 16,       // label
            RedValue = 17,        // red
            ShapeAttr = 18,       // shape
            StringLiteral = 19,   // StringLiteral
            TrueValue = 20,       // true
            NTAttr = 21,          // <Attr>
            NTLine = 22,          // <Line>
            NTLines = 23,         // <Lines>
            NTParam = 24,         // <Param>
            NTParams = 25,        // <Params>
            NTProgram = 26,       // <Program>
            NTStart = 27,         // <Start>
            NTValue = 28          // <Value>
        }

        private enum RuleConstants
        {
            Start = 0, // <Start> ::= <Program>
            Program = 1, // <Program> ::= digraph ID '{' <Lines> '}'
            LinesMultiple = 2, // <Lines> ::= <Line> <Lines>
            LinesSingle = 3, // <Lines> ::= <Line>
            LineNodeParams = 4, // <Line> ::= ID '[' <Params> ']' ';'
            LineNextNode = 5, // <Line> ::= ID '->' ID ';'
            LineVerticeParams = 6, // <Line> ::= ID '->' ID '[' <Params> ']' ';'
            ParamsMultiple = 7, // <Params> ::= <Param> ',' <Params>
            ParamsSingle = 8, // <Params> ::= <Param>
            Param = 9, // <Param> ::= <Attr> '=' <Value>
            AttrShape = 10, // <Attr> ::= shape
            AttrLabel = 11, // <Attr> ::= label
            AttrColor = 12, // <Attr> ::= color
            ValueString = 13, // <Value> ::= StringLiteral
            ValueBox = 14, // <Value> ::= box
            ValueTrue = 15, // <Value> ::= true
            ValueFalse = 16, // <Value> ::= false
            ValueRed = 17, // <Value> ::= red
        }
    }
}

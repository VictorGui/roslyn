// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp.ExpressionEvaluator
{
    internal sealed class ExceptionLocalSymbol : PlaceholderLocalSymbol
    {
        private readonly string getExceptionMethodName;

        internal ExceptionLocalSymbol(MethodSymbol method, string name, TypeSymbol type, string getExceptionMethodName) :
            base(method, name, type)
        {
            this.getExceptionMethodName = getExceptionMethodName;
        }

        internal override bool IsWritable
        {
            get { return false; }
        }

        internal override BoundExpression RewriteLocal(CSharpCompilation compilation, EENamedTypeSymbol container, CSharpSyntaxNode syntax)
        {
            var method = GetIntrinsicMethod(compilation, this.getExceptionMethodName);
            var call = BoundCall.Synthesized(syntax, receiverOpt: null, method: method);
            return ConvertToLocalType(compilation, call, this.Type);
        }
    }
}

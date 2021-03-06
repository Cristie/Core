﻿// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Core.CommandLine
{
    using System;
    using System.Collections.Generic;
    using WixToolset.Data;
    using WixToolset.Extensibility.Data;

    internal class CompileCommand : ICommandLineCommand
    {
        public CompileCommand(IServiceProvider serviceProvider, IEnumerable<SourceFile> sources, IDictionary<string, string> preprocessorVariables)
        {
            this.PreprocessorVariables = preprocessorVariables;
            this.ServiceProvider = serviceProvider;
            this.SourceFiles = sources;
        }

        private IServiceProvider ServiceProvider { get; }

        public IEnumerable<string> IncludeSearchPaths { get; }

        private IEnumerable<SourceFile> SourceFiles { get; }

        private IDictionary<string, string> PreprocessorVariables { get; }

        public int Execute()
        {
            foreach (var sourceFile in this.SourceFiles)
            {
                var preprocessor = new Preprocessor(this.ServiceProvider);
                preprocessor.IncludeSearchPaths = this.IncludeSearchPaths;
                preprocessor.Platform = Platform.X86; // TODO: set this correctly
                preprocessor.SourcePath = sourceFile.SourcePath;
                preprocessor.Variables = new Dictionary<string, string>(this.PreprocessorVariables);
                var document = preprocessor.Execute();

                var compiler = new Compiler(this.ServiceProvider);
                compiler.OutputPath = sourceFile.OutputPath;
                compiler.Platform = Platform.X86; // TODO: set this correctly
                compiler.SourceDocument = document;
                var intermediate = compiler.Execute();

                intermediate.Save(sourceFile.OutputPath);
            }

            return 0;
        }
    }
}

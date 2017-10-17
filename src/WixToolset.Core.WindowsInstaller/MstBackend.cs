﻿// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Core.WindowsInstaller
{
    using System;
    using WixToolset.Core.WindowsInstaller.Databases;
    using WixToolset.Core.WindowsInstaller.Unbind;
    using WixToolset.Data;
    using WixToolset.Data.Bind;
    using WixToolset.Extensibility;

    internal class MstBackend : IBackend
    {
        public BindResult Bind(IBindContext context)
        {
            var command = new BindTransformCommand();
            command.Extensions = context.Extensions;
            command.TempFilesLocation = context.IntermediateFolder;
            command.Transform = context.IntermediateRepresentation;
            command.OutputPath = context.OutputPath;
            command.Execute();

            return new BindResult(Array.Empty<FileTransfer>(), Array.Empty<string>());
        }

        public bool Inscribe(IInscribeContext context)
        {
            throw new NotImplementedException();
        }

        public Output Unbind(IUnbindContext context)
        {
            var command = new UnbindMsiOrMsmCommand(context);
            return command.Execute();
        }
    }
}
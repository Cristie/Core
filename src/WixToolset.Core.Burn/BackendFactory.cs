﻿// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Core.Burn
{
    using System;
    using System.IO;
    using WixToolset.Extensibility;

    internal class BackendFactory : IBackendFactory
    {
        public bool TryCreateBackend(string outputType, string outputFile, IBindContext context, out IBackend backend)
        {
            if (String.IsNullOrEmpty(outputType))
            {
                outputType = Path.GetExtension(outputFile);
            }

            switch (outputType.ToLowerInvariant())
            {
                case "bundle":
                case ".exe":
                    backend = new BundleBackend();
                    return true;
            }

            backend = null;
            return false;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WIA;

namespace NAPS2.Scan.Wia
{
    public class ConsoleWiaTransfer : IWiaTransfer
    {
        public Stream Transfer(int pageNumber, WiaBackgroundEventLoop eventLoop, string format)
        {
            // The console shouldn't spawn new forms, so use the silent transfer method.
            // TODO: Test cancellation (via Ctrl+C or similar)
            ImageFile imageFile = null;
            eventLoop.Do(() =>
            {
                imageFile = (ImageFile)eventLoop.WiaItem.Transfer(format);
            });
            eventLoop.Sync();
            if (imageFile == null)
            {
                return null;
            }
            return new MemoryStream((byte[])imageFile.FileData.get_BinaryData());
        }
    }
}
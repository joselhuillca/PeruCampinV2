using Android.Content;
using Java.IO;
using MLearning.Core.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Practica.Droid.File
{
    class DroidAsyncStorageService : IAsyncStorageService
    {

        async public System.Threading.Tasks.Task<byte[]> TryReadBinaryFile(string filename)
        {

            string filePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal),filename);
            return System.IO.File.ReadAllBytes(filePath);

        }

        async public System.Threading.Tasks.Task<string> TryReadTextFile(string filename)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
            return System.IO.File.ReadAllText(filePath);
        }
    }
}

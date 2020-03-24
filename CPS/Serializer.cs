using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using CPS.Signal;
using Microsoft.Win32;

namespace CPS
{
    public class Serializer
    {
        public static void SaveToBinaryFile(BinaryWrapper bin, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, bin);
            stream.Close();
        }

        public static BinaryWrapper ReadFromBinaryFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryWrapper bin = (BinaryWrapper) formatter.Deserialize(stream);
            stream.Close();

            return bin;
        }

        public static string FilePath(bool load)
        {
            if (load)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Bin File(*.bin)| *.bin",
                    RestoreDirectory = true
                };

                openFileDialog.ShowDialog();

                if (openFileDialog.FileName.Length <= 0)
                {
                    MessageBox.Show("Please select a file");
                    return null;
                }

                return openFileDialog.FileName;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "Bin File(*.bin)| *.bin",
                    RestoreDirectory = true
                };

                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName.Length <= 0)
                {
                    MessageBox.Show("Please select a file");
                    return null;
                }

                return saveFileDialog.FileName;
            }

            
        }
    }

    [Serializable]
    public class BinaryWrapper
    {
        public Params SignalParams { get; set; }
        public SignalWrapper SelectedSignal { get; set; }
        public DiscreteSignal DiscreteSignal { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace RsLib.Common
{
    public static class Extensions
    {
        public static T DeepClone<T>(this T item)
        {
            if (item != null)
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, item);
                    stream.Seek(0, SeekOrigin.Begin);
                    var result = (T)formatter.Deserialize(stream);
                    return result;
                }
            }

            return default(T);
        }
        public static byte[] ConvertTobyteArr(this int intValue, uint arraySize)
        {
            byte[] array = new byte[arraySize];
            string text = intValue.ToString("X");
            if (text.Length % 2 != 0)
            {
                text = "0" + text;
            }
            int num = 0;
            for (int num2 = text.Length - 1; num2 >= 0; num2 -= 2)
            {
                string value = text.Substring(num2 - 1, 2);
                if (num < arraySize)
                {
                    array[num] = Convert.ToByte(value, 16);
                    num++;
                }
            }
            return array;
        }
        public static byte[] ConvertTobyteArr(this ushort intValue, uint arraySize)
        {
            byte[] array = new byte[arraySize];
            string text = intValue.ToString("X");
            if (text.Length % 2 != 0)
            {
                text = "0" + text;
            }
            int num = 0;
            for (int num2 = text.Length - 1; num2 >= 0; num2 -= 2)
            {
                string value = text.Substring(num2 - 1, 2);
                if (num < arraySize)
                {
                    array[num] = Convert.ToByte(value, 16);
                    num++;
                }
            }
            return array;
        }
        /// <summary>
        /// Convert string to word array. 
        /// ex: Test => 
        /// T : word 1 Low bit, e : word 1 High bit
        /// s : word 2 Low bit, t : word 2 High bit
        /// </summary>
        /// <param name="s"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static int[] ConvertToWordArray(this string s, uint dWordCount)
        {
            int[] iArr = new int[dWordCount];
            for (int i = 0; i < dWordCount; i++)
            {
                int lowBitStringIndex = i * 2;
                int highBitStringIndex = i * 2 + 1;
                int lowBitInt = 32; // ASCII space
                int highBitInt = 32; // ASCII space
                if (lowBitStringIndex < s.Length)
                {
                    char c = s[lowBitStringIndex];
                    lowBitInt = Convert.ToInt32(c);
                }

                if (highBitStringIndex < s.Length)
                {
                    char c = s[highBitStringIndex];
                    highBitInt = Convert.ToInt32(c);
                }
                iArr[i] = highBitInt << 8 | lowBitInt;
            }
            return iArr;
        }
        public static string ConvertToString(this int[] intArray)
        {
            string s = "";
            List<char> charList = new List<char>();
            for (int i = 0; i < intArray.Length; i++)
            {
                int intValue = intArray[i];
                int lowBit = intValue & 255;
                int highBit = intValue >> 8 & 255;
                charList.Add(Convert.ToChar(lowBit));
                charList.Add(Convert.ToChar(highBit));
            }
            s = string.Concat(charList.ToArray());
            return s;
        }
        public static ComboBox AddEnumItems(this ComboBox box,Type enumType)
        {
            box.Items.Clear();
            if (enumType.BaseType.FullName != "System.Enum") return box;
            
            string[] names = Enum.GetNames(enumType);
            for (int i = 0; i < names.Length; i++)
            {
                box.Items.Add(names[i]);
            }
            return box;
        }
    }

}

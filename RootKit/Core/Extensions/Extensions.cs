using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RootKit.Core
{
    //public partial class Extensions 
    public static partial class Extensions
    {
        /// <summary>
        /// Extend the LayerToSave base class
        /// </summary>
        /// <param name="layertosave">Source</param>
        /// <param name="val">value to check</param>
        /// <returns>a boolean indicating the existence of the Val parameter in the LayerToSave</returns>
        public static FileInfo[] GetFilesWithPattern(this DirectoryInfo dinfo, string ext)
        {
            string[] exts = ext.Split(',');
            FileInfo[][] finfo = new FileInfo[exts.Length][];
            int i = 0;

            foreach (string e in exts)
            {
                /* get files from the directory as a files collection */
                finfo[i++] = dinfo.GetFiles(e);
            }

            int tlength = 0;

            for (i = 0; i < finfo.Length; i++)
            {
                tlength += finfo[i].Length;
            }

            FileInfo[] res = new FileInfo[tlength];
            int j = 0;
            int rindex = 0;

            for (i = 0; i < finfo.Length; i++)
            {
                for (j = 0; j < finfo[i].Length; j++)
                {
                    res[rindex++] = finfo[i][j];
                }
            }

            return res;

        }

        public static string ElapsedTime(this Stopwatch myTime)
        {
            return myTime.ElapsedMilliseconds.ToString();
        }

        public static string ToRuntime(this int myTime)
        { 
            int Hour, Min;
            Hour = myTime / 60;
            Min = myTime - (Hour * 60);
            return Hour.ToString() + "H" + ((Min.ToString().Length == 1) ? "0" : "") + Min.ToString();
        }

        public static Graphics DrawText(this Graphics g, Image _bitmap, String _text, StringFormat _strFormat, Font _textFont, Brush _brush, RectangleF _clip, bool _dropShadow)
        {
            //Graphics g = Graphics.FromImage(_bitmap);
            RectangleF shadowClip = new RectangleF(_clip.X + 1, _clip.Y + 1, _clip.Width, _clip.Height);
            
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //_strFormat.Alignment = StringAlignment.Near;
            if (_dropShadow) 
                g.DrawString(_text, _textFont, Brushes.Black, shadowClip, _strFormat);

            //_strFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            //g.DrawString(_text, _textFont, _brush, _clip, _strFormat);
            g.DrawString(_text, _textFont, _brush, _clip, _strFormat);
            
            return g;
        }

        //public static long Seconds(this Stopwatch myTime)
        //{
        //    long seconds = myTime.ElapsedMilliseconds / 1000;
        //    return seconds;
        //}
        //public static long Milliseconds(this Stopwatch myTime)
        //{
        //    long seconds = myTime.ElapsedMilliseconds / 1000;
        //    return myTime.ElapsedMilliseconds - (seconds * 1000);
        //}
    }
}

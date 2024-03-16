using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Extensions
{
    public static partial class ExportExcelSupportExtension
    {
        #region RichText
        /// <summary>
        /// ใช้ฟอนต์หลายรูปแบบ เช่น ขนาดคนละขนาดภายในบริเวณเดียวกัน เป็นต้น
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ExcelRange richText(this ExcelRange excelRange, string text)
        {
            excelRange.IsRichText = true;
            excelRange.RichText.Add(text);

            return excelRange;
        }

        /// <summary>
        /// ใช้ฟอนต์หลายรูปแบบ เช่น ขนาดคนละขนาดภายในบริเวณเดียวกัน เป็นต้น เพิ่มเติมกำหนดสไตล์ได้
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="text"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ExcelRange richText(this ExcelRange excelRange, string text, Action<ExcelRichText> action)
        {
            excelRange.IsRichText = true;

            ExcelRichText _ert = excelRange.RichText.Add(text);
            action(_ert);

            return excelRange;
        }
        #endregion RichText

        #region Text
        /// <summary>
        /// กำหนดฟอนต์ตัวหนา
        /// </summary>
        /// <param name="excelRichText"></param>
        /// <returns></returns>
        public static ExcelRichText strong(this ExcelRichText excelRichText)
        {
            excelRichText.Bold = true;

            return excelRichText;
        }

        /// <summary>
        /// ขนาดฟอนต์ตัวปกติ
        /// </summary>
        /// <param name="excelRichText"></param>
        /// <returns></returns>
        public static ExcelRichText normal(this ExcelRichText excelRichText)
        {
            excelRichText.Bold = false;

            return excelRichText;
        }

        /// <summary>
        /// กำหนดขนาดฟอนต์
        /// </summary>
        /// <param name="excelRichText"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static ExcelRichText fontSize(this ExcelRichText excelRichText, float fontSize)
        {
            excelRichText.Size = fontSize;

            return excelRichText;
        }
        #endregion Text

        #region Text color
        /// <summary>
        /// กำหนดสีตัวหนังสือระดับแถว แบบ html color code เช่น #000000
        /// </summary>
        /// <param name="excelRichText"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelRichText textColor(this ExcelRichText excelRichText, string codeColor)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(codeColor);
            excelRichText.Color = color;

            return excelRichText;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับแถว จาก Color class
        /// </summary>
        /// <param name="excelRichText"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ExcelRichText textColor(this ExcelRichText excelRichText, Color color)
        {
            excelRichText.Color = color;

            return excelRichText;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับแถว จาก Rgb
        /// </summary>
        /// <param name="excelRichText"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExcelRichText textColor(this ExcelRichText excelRichText, byte r, byte g, byte b)
        {
            Color color = System.Drawing.Color.FromArgb(r, g, b);

            excelRichText.Color = color;

            return excelRichText;
        }
        #endregion Text color 
    }
}

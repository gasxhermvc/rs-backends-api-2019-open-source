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
        #region Row-Column
        /// <summary>
        /// กำหนดขนาดความสูงของแถวทั้งหมด
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ExcelWorksheet defaultRowHeight(this ExcelWorksheet excelSheet, double height)
        {
            if (height == 0)
            {
                throw new ArgumentException("{0}, Please input height more than 0", nameof(height));
            }

            excelSheet.DefaultRowHeight = height;

            return excelSheet;
        }

        /// <summary>
        /// กำหนดขนาดความกว้างของตารางทั้งหมด
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static ExcelWorksheet defaultColWidth(this ExcelWorksheet excelSheet, double width)
        {
            if (width == 0)
            {
                throw new ArgumentException("{0}, Please input height more than 0", nameof(width));
            }

            excelSheet.DefaultColWidth = width;

            return excelSheet;
        }

        /// <summary>
        /// กำหนดขนาดความสูงของแถว
        /// </summary>
        /// <param name="excelRow"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ExcelRow rowHeight(this ExcelRow excelRow, double height)
        {
            if (height == 0)
            {
                throw new ArgumentException("{0}, Please input height more than 0", nameof(height));
            }

            excelRow.Height = height;

            return excelRow;
        }

        /// <summary>
        /// กำหนดขนาดความกว้างให้กับคอลัมน์
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static ExcelColumn colWidth(this ExcelColumn excelColumn, double width)
        {
            if (width == 0)
            {
                throw new ArgumentException("{0}, Please input width more than 0", nameof(width));
            }

            excelColumn.Width = width;

            return excelColumn;
        }

        /// <summary>
        /// กำหนดความกว้างของคอลัมน์อันโนมัติ
        /// </summary>
        /// <param name="excelRange"></param>
        /// <returns></returns>
        public static ExcelRange autoWidth(this ExcelRange excelRange)
        {
            excelRange.AutoFitColumns();

            return excelRange;
        }
        #endregion Row-Column

        #region Text
        /// <summary>
        /// กำหนดขนาดตัวหนังสือภายในคอลัมน์แบบมีการรวมคอลัมน์
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static ExcelRange fontSize(this ExcelRange excelRange, float fontSize)
        {
            excelRange.Style.Font.Size = fontSize;

            return excelRange;
        }

        /// <summary>
        /// กำหนดขนาดตัวหนังสือภายในคอลัมน์เดียว
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static ExcelColumn fontSize(this ExcelColumn excelColumn, float fontSize)
        {
            excelColumn.Style.Font.Size = fontSize;

            return excelColumn;
        }

        /// <summary>
        /// กำหนดขนาดตัวหนังสือทั้งแถว
        /// </summary>
        /// <param name="excelRow"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static ExcelRow fontSize(this ExcelRow excelRow, float fontSize)
        {
            excelRow.Style.Font.Size = fontSize;

            return excelRow;
        }

        /// <summary>
        /// กำหนดขนาดตัวหนังสือทั้งหมดของชีท
        /// </summary>
        /// <param name="excelSheet"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static ExcelWorksheet defaultFontSize(this ExcelWorksheet excelSheet, object fontSize)
        {
            excelSheet.Cells.Style.Font.Size = float.Parse(fontSize.ToString());

            return excelSheet;
        }

        /// <summary>
        /// กำหนดฟอนท์ให้กับชีท
        /// </summary>
        /// <param name="excelSheet"></param>
        /// <param name="fontFamily"></param>
        /// <returns></returns>
        public static ExcelWorksheet defaultFontFamily(this ExcelWorksheet excelSheet, string fontFamily)
        {
            excelSheet.Cells.Style.Font.Name = fontFamily;

            return excelSheet;
        }

        /// <summary>
        /// ขึ้นบรรทัดเมื่อข้อความยาวเกินไป
        /// </summary>
        /// <param name="col"></param>
        /// <param name="wrap"></param>
        /// <returns></returns>
        public static ExcelColumn wrapText(this ExcelColumn excelColumn, bool wrap)
        {
            excelColumn.Style.WrapText = wrap;

            return excelColumn;
        }

        /// <summary>
        /// กำนหดตัวหนังสือของแถวเป็นตัวหนา
        /// </summary>
        /// <param name="excelRow"></param>
        /// <returns></returns>
        public static ExcelRow strong(this ExcelRow excelRow)
        {
            excelRow.Style.Font.Bold = true;

            return excelRow;
        }

        /// <summary>
        /// ข้อความตัวหนา
        /// </summary>
        /// <param name="excelRange"></param>
        /// <returns></returns>
        public static ExcelRange strong(this ExcelRange excelRange)
        {
            excelRange.Style.Font.Bold = true;

            return excelRange;
        }

        /// <summary>
        /// กำนหดตัวหนังสือของคอลัมน์
        /// </summary>
        /// <param name="excelRow"></param>
        /// <returns></returns>
        public static ExcelColumn strong(this ExcelColumn excelColumn)
        {
            excelColumn.Style.Font.Bold = true;

            return excelColumn;
        }
        #endregion

        #region Text align
        /// <summary>
        /// จัดให้ข้อความแนวนอนภายในแถวอยู่กึ่งกลาง
        /// </summary>
        /// <param name="excelRow"></param>
        /// <returns></returns>
        public static ExcelRow alignCenter(this ExcelRow excelRow)
        {
            excelRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            return excelRow;
        }

        /// <summary>
        /// จัดให้ข้อความแนวนอนภายในแถวอยู่ชิดซ้าย
        /// </summary>
        /// <param name="excelRow"></param>
        /// <returns></returns>
        public static ExcelRow alignLeft(this ExcelRow excelRow)
        {
            excelRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            return excelRow;
        }

        /// <summary>
        ///  จัดให้ข้อความแนวนอนภายในแถวอยู่ชิดขวา
        /// </summary>
        /// <param name="excelRow"></param>
        /// <returns></returns>
        public static ExcelRow alignRight(this ExcelRow excelRow)
        {
            excelRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            return excelRow;
        }

        /// <summary>
        /// จัดให้ข้อความแนวนอนภายในคอลัมน์อยู่กึ่งกลาง
        /// </summary>
        /// <param name="excelRange"></param>
        /// <returns></returns>
        public static ExcelRange alignCenter(this ExcelRange excelRange)
        {
            excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            return excelRange;
        }

        /// <summary>
        /// จัดให้ข้อความแนวนอนภายในคอลัมน์ชิดซ้าย
        /// </summary>
        /// <param name="excelRange"></param>
        /// <returns></returns>
        public static ExcelRange alignLeft(this ExcelRange excelRange)
        {
            excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            return excelRange;
        }

        /// <summary>
        /// จัดให้ข้อความแนวนอนภายในข้อลัมน์ชิดขวา
        /// </summary>
        /// <param name="excelRange"></param>
        /// <returns></returns>
        public static ExcelRange alignRight(this ExcelRange excelRange)
        {
            excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            return excelRange;
        }

        /// <summary>
        /// จัดให้ข้อความแนวนอนภายในคอลัมน์อยู่กึ่งกลาง
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <returns></returns>
        public static ExcelColumn alignCenter(this ExcelColumn excelColumn)
        {
            excelColumn.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            return excelColumn;
        }

        /// <summary>
        /// จัดให้ข้อความแนวนอนภายในคอลัมน์แถวอยู่ชิดซ้าย
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <returns></returns>
        public static ExcelColumn alignLeft(this ExcelColumn excelColumn)
        {
            excelColumn.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            return excelColumn;
        }

        /// <summary>
        ///  จัดให้ข้อความแนวนอนภายในคอลัมน์แถวอยู่ชิดขวา
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <returns></returns>
        public static ExcelColumn alignRight(this ExcelColumn excelColumn)
        {
            excelColumn.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            return excelColumn;
        }

        /// <summary>
        /// จัดให้ข้อความแนวตั้งภายในแถวอยู่กึ่งกลาง
        /// </summary>
        /// <param name="excelRow"></param>
        /// <returns></returns>
        public static ExcelRow valignCenter(this ExcelRow excelRow)
        {
            excelRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            return excelRow;
        }

        /// <summary>
        /// จัดให้ข้อความแนวตั้งภายในแถวอยู่บนสุด
        /// </summary>
        /// <param name="excelRow"></param>
        /// <returns></returns>
        public static ExcelRow valignTop(this ExcelRow excelRow)
        {
            excelRow.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            return excelRow;
        }

        /// <summary>
        /// จัดให้ข้อความแนวตั้งภายในแถวอยู่ล่างสุด
        /// </summary>
        /// <param name="excelRow"></param>
        /// <returns></returns>
        public static ExcelRow valignBottom(this ExcelRow excelRow)
        {
            excelRow.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;

            return excelRow;
        }

        /// <summary>
        /// จัดให้ข้อความบริเวณแนวตั้งอยู่กึ่งกลาง
        /// </summary>
        /// <param name="excelRange"></param>
        /// <returns></returns>
        public static ExcelRange valignCenter(this ExcelRange excelRange)
        {
            excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            return excelRange;
        }

        /// <summary>
        /// จัดให้ข้อความบริเวณแนวตั้งอยู่บนสุด
        /// </summary>
        /// <param name="excelRange"></param>
        /// <returns></returns>
        public static ExcelRange valignTop(this ExcelRange excelRange)
        {
            excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            return excelRange;
        }

        /// <summary>
        /// จัดให้ข้อความบริเวณแนวตั้งอยู่ล่างสุด
        /// </summary>
        /// <param name="excelRange"></param>
        /// <returns></returns>
        public static ExcelRange valignBottom(this ExcelRange excelRange)
        {
            excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;

            return excelRange;
        }

        /// <summary>
        /// จัดให้ข้อความคอลัมน์แนวตั้งอยู่กึ่งกลาง
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <returns></returns>
        public static ExcelColumn valignCenter(this ExcelColumn excelColumn)
        {
            excelColumn.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            return excelColumn;
        }

        /// <summary>
        /// จัดให้ข้อความคอลัมน์แนวตั้งอยู่บนสุด
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <returns></returns>
        public static ExcelColumn valignTop(this ExcelColumn excelColumn)
        {
            excelColumn.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            return excelColumn;
        }

        /// <summary>
        /// จัดให้ข้อความคอลัมน์แนวตั้งอยู่ล่างสุด
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <returns></returns>
        public static ExcelColumn valignBottom(this ExcelColumn excelColumn)
        {
            excelColumn.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;

            return excelColumn;
        }
        #endregion Text align

        #region Border
        /// <summary>
        /// กำหนดเส้นตาราง
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="borderStyle"></param>
        /// <returns></returns>
        public static ExcelRange border(this ExcelRange excelRange, ExcelBorderStyle borderStyle)
        {
            excelRange.Style.Border.Top.Style = borderStyle;
            excelRange.Style.Border.Left.Style = borderStyle;
            excelRange.Style.Border.Right.Style = borderStyle;
            excelRange.Style.Border.Bottom.Style = borderStyle;

            return excelRange;
        }
        #endregion Border

        #region Text color
        /// <summary>
        /// กำหนดสีตัวหนังสือระดับแถว แบบ html color code เช่น #000000
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelRow textColor(this ExcelRow excelRow, string codeColor)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(codeColor);
            excelRow.Style.Font.Color.SetColor(color);

            return excelRow;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับแถว จาก Color class
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ExcelRow textColor(this ExcelRow excelRow, Color color)
        {
            excelRow.Style.Font.Color.SetColor(color);

            return excelRow;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับแถว จาก Rgb
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExcelRow textColor(this ExcelRow excelRow, byte r, byte g, byte b)
        {
            Color color = System.Drawing.Color.FromArgb(r, g, b);
            excelRow.Style.Font.Color.SetColor(color);

            return excelRow;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับคอลัมน์ แบบ html color code เช่น #000000
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelRange textColor(this ExcelRange excelRange, string codeColor)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(codeColor);
            excelRange.Style.Font.Color.SetColor(color);

            return excelRange;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับคอลัมน์ จาก Color class
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ExcelRange textColor(this ExcelRange excelRange, Color color)
        {
            excelRange.Style.Font.Color.SetColor(color);

            return excelRange;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับคอลัมน์ จาก Rgb
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExcelRange textColor(this ExcelRange excelRange, byte r, byte g, byte b)
        {
            Color color = System.Drawing.Color.FromArgb(r, g, b);
            excelRange.Style.Font.Color.SetColor(color);

            return excelRange;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับคอลัมน์ แบบ html color code เช่น #000000
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelColumn textColor(this ExcelColumn excelColumn, string codeColor)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(codeColor);
            excelColumn.Style.Font.Color.SetColor(color);

            return excelColumn;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับคอลัมน์ จาก Color class
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ExcelColumn textColor(this ExcelColumn excelColumn, Color color)
        {
            excelColumn.Style.Font.Color.SetColor(color);

            return excelColumn;
        }

        /// <summary>
        /// กำหนดสีตัวหนังสือระดับคอลัมน์ จาก Rgb
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExcelColumn textColor(this ExcelColumn excelColumn, byte r, byte g, byte b)
        {
            Color color = System.Drawing.Color.FromArgb(r, g, b);
            excelColumn.Style.Font.Color.SetColor(color);

            return excelColumn;
        }
        #endregion Text color

        #region Background color
        /// <summary>
        /// กำหนดสีพื้นหลังระดับแถว แบบ html color code เช่น #000000
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelRow backgroundColor(this ExcelRow excelRow, string codeColor)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(codeColor);
            excelRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRow.Style.Fill.BackgroundColor.SetColor(color);

            return excelRow;
        }

        /// <summary>
        /// กำหนดสีพื้นหลังระดับแถว จาก Color class
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelRow backgroundColor(this ExcelRow excelRow, Color color)
        {
            excelRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRow.Style.Fill.BackgroundColor.SetColor(color);

            return excelRow;
        }

        /// <summary>
        /// กำหนดสีพื้นหลังระดับแถว จาก Rgb
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExcelRow backgroundColor(this ExcelRow excelRow, byte r, byte g, byte b)
        {
            Color color = System.Drawing.Color.FromArgb(r, g, b);

            excelRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRow.Style.Fill.BackgroundColor.SetColor(color);

            return excelRow;
        }

        /// <summary>
        /// กำหนดสีพื้นหลังระดับคอลัมน์ แบบ html color code เช่น #000000
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelRange backgroundColor(this ExcelRange excelRange, string codeColor)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(codeColor);
            excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(color);

            return excelRange;
        }

        /// <summary>
        /// กำหนดสีพื้นหลังระดับคอลัมน์ จาก Color class
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelRange backgroundColor(this ExcelRange excelRange, Color color)
        {
            excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(color);

            return excelRange;
        }

        /// <summary>
        /// กำหนดสีพื้นหลังระดับคอลัมน์ จาก Rgb
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExcelRange backgroundColor(this ExcelRange excelRange, byte r, byte g, byte b)
        {
            Color color = System.Drawing.Color.FromArgb(r, g, b);

            excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(color);

            return excelRange;
        }

        /// <summary>
        /// กำหนดสีพื้นหลังระดับคอลัมน์ แบบ html color code เช่น #000000
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelColumn backgroundColor(this ExcelColumn excelColumn, string codeColor)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml(codeColor);
            excelColumn.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelColumn.Style.Fill.BackgroundColor.SetColor(color);

            return excelColumn;
        }

        /// <summary>
        /// กำหนดสีพื้นหลังระดับคอลัมน์ จาก Color class
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <param name="codeColor"></param>
        /// <returns></returns>
        public static ExcelColumn backgroundColor(this ExcelColumn excelColumn, Color color)
        {
            excelColumn.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelColumn.Style.Fill.BackgroundColor.SetColor(color);

            return excelColumn;
        }

        /// <summary>
        /// กำหนดสีพื้นหลังระดับคอลัมน์ จาก Rgb
        /// </summary>
        /// <param name="excelColumn"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExcelColumn backgroundColor(this ExcelColumn excelColumn, byte r, byte g, byte b)
        {
            Color color = System.Drawing.Color.FromArgb(r, g, b);

            excelColumn.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelColumn.Style.Fill.BackgroundColor.SetColor(color);

            return excelColumn;
        }
        #endregion Background color

        #region Format
        /// <summary>
        /// รองรับการกำหนดฟอร์แมตคอลัมน์
        /// </summary>
        /// <param name="excelRange"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static ExcelRange format(this ExcelRange excelRange, string formatter)
        {
            excelRange.Style.Numberformat.Format = formatter;

            return excelRange;
        }
        #endregion Format
    }
}

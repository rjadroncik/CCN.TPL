using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using CCN.Office.Exceptions;
using Microsoft.Office.Interop.Excel;

namespace CCN.Office
{
    public class CCNExcel
    {
        #region Indexing

        public static string ColumnLetter(int column)
        {
            return (column > 26) ? Convert.ToChar(64 + ((column - 1) / 26)).ToString() + Convert.ToChar(65 + ((column - 1) % 26)).ToString()
                                 : Convert.ToChar(64 + column).ToString();
        }

        public static int ColumnIndex(string column)
        {
            if (column.Length > 1)
            {
                return ((Convert.ToInt32(column[0]) - 64) * 26) + (Convert.ToInt32(column[1]) - 64);
            }
            else
            {
                return Convert.ToInt32(column[0]) - 64;
            }
        }

        public static string ColumnRange(int column)
        {
            return ColumnLetter(column) + ":" + ColumnLetter(column);
        }

        public static string RowRange(int row)
        {
            return row + ":" + row;
        }   

        public static string CellRange(int column, int row)
        {
            return ColumnLetter(column) + row;
        }
        public static string CellRange(string column, int row)
        {
            return column + row;
        }

        public static string CellRange(int columnLeft, int columnRight, int row)
        {
            return ColumnLetter(columnLeft) + row + ":" + ColumnLetter(columnRight) + row;
        }
        public static string CellRange(string columnLeft, string columnRight, int row)
        {
            return columnLeft + row + ":" + columnRight + row;
        }

        public static string CellRange(int columnLeft, int columnRight, int rowTop, int rowBottom)
        {
            return ColumnLetter(columnLeft) + rowTop + ":" + ColumnLetter(columnRight) + rowBottom;
        }
        public static string CellRange(string columnLeft, string columnRight, int rowTop, int rowBottom)
        {
            return columnLeft + rowTop + ":" + columnRight + rowBottom;
        }

        public static string CellColumn(string range)
        {
            string column = string.Empty;

            for (int i = 0; i < range.Length; i++)
            {
                if ((range[i] > 64) && (range[i] < 91)) { column += range[i]; }
                else { break; }
            }

            return column;
        }
        public static int CellRow(string range)
        {
            return int.Parse(range.Substring(CellColumn(range).Length));
        }

        public static string BottomRightCell(Range range)
        {
            if ((bool)range.MergeCells)
            {
                return CellRange(range.Column + range.MergeArea.Columns.Count - 1, range.Row + range.MergeArea.Rows.Count - 1);
            }
            else
            {
                return CellRange(range.Column, range.Row);
            }
        }

        public static string RangeFromCenter(string column, int row, int width, int height)
        {
            int left = ColumnIndex(column) - (width / 2);
            int top = row - (height / 2);

            if (left < 1) { left = 1; }
            if (top < 1) { top = 1; }

            return ColumnLetter(left) + top + ":" + ColumnLetter(left + width) + (top + height);
        }

        #endregion

        #region Searching

        public static string FindRangeByValue(string value, string expectedColumn, int expectedRow, 
                                              int searchWidth, int searchHeight, Worksheet worksheet)
        {
            int center = ColumnIndex(expectedColumn);

            foreach (Range range in worksheet.Range[RangeFromCenter(expectedColumn, expectedRow, searchWidth, searchHeight)].Cells)
            {
                if (value.Equals(range.Value)) { return BottomRightCell(range); }

                if ((range.Value != null) && (range.Value is string) && ((string)range.Value).Contains(value))
                {
                    if (Regex.IsMatch((((string)range.Value).Replace(".", "").Replace(" ", "")), "^[0-9]*$"))
                    {
                        return BottomRightCell(range);
                    }
                }
            }

            throw new ValueNotFoundException("Chyba vo funkcii 'FindRangeByValue'.. Sheet: " + worksheet.Name);
        }

        #endregion

        #region Formatting

        public static int XlColor(Color color)
        {
            return ((256 * 256) * color.B) + (256 * color.G) + color.R;
        }

        public static string ZnakNKrat(char znak, int pocet)
        {
            if (pocet == 0) throw new ArgumentOutOfRangeException("Pocet musi byt vacsi ako 0!");

            string result = znak.ToString();

            for (int i = 1; i < pocet; i++)
            {
                result += znak.ToString();
            }

            return result;
        }

        public static void FormatGeneral(Range range)
        {
            range.NumberFormat = "General";
        }

        public static void FormatText(Range range)
        {
            range.NumberFormat = @"@";
        }

        public static void FormatDate(Range range)
        {
            range.NumberFormat = @"d/m/yyyy;@";
            range.HorizontalAlignment = Constants.xlRight;
        }

        public static void FormatCurreny(Range range)
        {
            range.NumberFormat = @"# ##0\\ [$€-1]";
        }

        public static void FormatCurrenyN(Range range, int decimals)
        {
            range.NumberFormat = @"# ##0." + ZnakNKrat('0', decimals) + "\\ [$€-1]";
        }

        public static void FormatPercentN(Range range, int decimals)
        {
            range.NumberFormat = @"0." + ZnakNKrat('0', decimals) + "%";
        }

        public static void FormatIntegerHideNulls(Range range)
        {
            range.NumberFormat = @"# ###";
        }

        public static void FormatInteger(Range range)
        {
            range.NumberFormat = @"# ##0";
        }

        public static void FormatDouble(Range range)
        {
            range.NumberFormat = @"# ##0";
        }

        public static void FormatDoubleN(Range range, int decimals)
        {
            range.NumberFormat = @"0." + ZnakNKrat('0', decimals) + "";
        }

        public static void FormatCustom(Range range, string format)
        {
            range.NumberFormat = format;
        }

        public static void FormatBackground(Range range, Color color)
        {
            FormatBackground(range.Interior, color);            
        }

        public static void FormatBackground(Interior interior, Color color)
        {
            interior.Pattern = Constants.xlSolid;
            interior.PatternColorIndex = Constants.xlAutomatic;
            interior.Color = XlColor(color);
            interior.TintAndShade = 0;
            interior.PatternTintAndShade = 0;
        }

        public static void FormatBorder(Range range, Color color, XlBordersIndex edge, 
                                        XlLineStyle style = XlLineStyle.xlContinuous,
                                        XlBorderWeight weight = XlBorderWeight.xlThin)
        {
            var borders = range.Borders[edge];

            borders.LineStyle = style;
            borders.Color = XlColor(color);
            borders.TintAndShade = 0;
            borders.Weight = weight;
        }

        public static void FormatBordersOuter(Range range, Color color,
                                              XlLineStyle style = XlLineStyle.xlContinuous,
                                              XlBorderWeight weight = XlBorderWeight.xlThin)
        {
            FormatBorder(range, color, XlBordersIndex.xlEdgeLeft, style, weight);
            FormatBorder(range, color, XlBordersIndex.xlEdgeTop, style, weight);
            FormatBorder(range, color, XlBordersIndex.xlEdgeRight, style, weight);
            FormatBorder(range, color, XlBordersIndex.xlEdgeBottom, style, weight);
        }

        public static void FormatBordersInner(Range range, Color color,
                                              XlLineStyle style = XlLineStyle.xlContinuous,
                                              XlBorderWeight weight = XlBorderWeight.xlThin)
        {
            FormatBorder(range, color, XlBordersIndex.xlInsideHorizontal, style, weight);
            FormatBorder(range, color, XlBordersIndex.xlInsideVertical, style, weight);
        }

        public static void FormatBordersAll(Range range, Color color,
                                            XlLineStyle style = XlLineStyle.xlContinuous,
                                            XlBorderWeight weight = XlBorderWeight.xlThin)
        {
            FormatBordersOuter(range, color, style, weight);
            FormatBordersInner(range, color, style, weight);
        }

        public static void FormatTextStyle(Range range, bool bold, bool italic = false, bool underline = false)
        {
            FormatTextStyle(range.Font, bold, italic, underline);
        }

        public static void FormatTextStyle(Microsoft.Office.Interop.Excel.Font font, bool bold, bool italic = false, bool underline = false)
        {
            font.Bold = bold;
            font.Italic = italic;
            font.Underline = underline;
        }

        public static void FormatTextColor(Range range, Color color)
        {
            FormatTextColor(range.Font, color);
        }

        public static void FormatTextColor(Microsoft.Office.Interop.Excel.Font font, Color color)
        {
            font.Color = XlColor(color);
            font.TintAndShade = 0;
        }

        #endregion

        #region Common tasks

        /// <summary>
        /// Nastavi headre ako fixne (nehybu sa pri skrolovani).
        /// </summary>
        /// <param name="window"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public static void FreezeHeaders(Window window, int rows, int cols)
        {
            window.SplitColumn = cols;
            window.SplitRow = rows;
            window.FreezePanes = true;
        }

        /// <summary>
        /// Na dany rozsah headrov aplikuje autofilter.
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="range"></param>
        public static void AutoFilter(Worksheet worksheet, string range)
        {
            worksheet.EnableAutoFilter = true;
            worksheet.AutoFilterMode = false;
            worksheet.Range[range].AutoFilter(1);
        }

        public static void PrintArea(Worksheet worksheet, string range)
        {
            worksheet.PageSetup.PrintArea = range;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using CCN.Services;
using CCN.Core.VB;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace CCN.Office
{
    public abstract class CCNExcelSession : Service
    {
        #region Common tasks

        private static Workbook Startup(string subor)
        {
            Application Application = new Application(); 

            try 
            {
                Application.DisplayAlerts = false;

                if (subor != null)
                {
                    return Application.Workbooks.Open(Path.GetFullPath(subor));
                }
                else
                {
                    return Application.Workbooks.Add();
                }
            }
            catch
            {
                Application.Quit();
                Marshal.FinalReleaseComObject(Application);
                throw;
            }
        }

        private static void Cleanup(Workbook workbook, bool save, bool close, string ulozAko)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (save) 
            {
                if (ulozAko != null)
                {
                    if (ulozAko.EndsWith(".xls"))
                    {
                        workbook.SaveAs(Filename: ulozAko, FileFormat: XlFileFormat.xlExcel8);
                    }
                    else if (ulozAko.EndsWith(".xlsx"))
                    {
                        workbook.SaveAs(Filename: ulozAko, FileFormat: XlFileFormat.xlOpenXMLWorkbook);
                    }
                }
                else
                {
                    workbook.Save(); 
                }
            }
            if (close) { workbook.Close(); }

            Marshal.FinalReleaseComObject(workbook);
        }

        #endregion

        #region Execution

        public static void Execute(string subor, bool save, bool close, string ulozAko, Worker1<Workbook> worker)
        {
            Workbook workbook = Startup(subor);

            try     { worker(workbook); }
            finally { Cleanup(workbook, save, close, ulozAko); }
        }

        public static void Execute<T>(string subor, bool save, bool close, string ulozAko, Worker2<Workbook, T> worker, T param)
        {
            Workbook workbook = Startup(subor);

            try     { worker(workbook, param); }
            finally { Cleanup(workbook, save, close, ulozAko); }
        }

        public static void Execute<T1, T2>(string subor, bool save, bool close, string ulozAko, Worker3<Workbook, T1, T2> worker, T1 param1, T2 param2)
        {
            Workbook workbook = Startup(subor);

            try     { worker(workbook, param1, param2); }
            finally { Cleanup(workbook, save, close, ulozAko); }
        }

        public static void Execute<T1, T2, T3>(string subor, bool save, bool close, string ulozAko, Worker4<Workbook, T1, T2, T3> worker, T1 param1, T2 param2, T3 param3)
        {
            Workbook workbook = Startup(subor);

            try { worker(workbook, param1, param2, param3); }
            finally { Cleanup(workbook, save, close, ulozAko); }
        }

        public static R ExecuteOperation<R>(string subor, bool save, bool close, string ulozAko, Operation1<R, Workbook> worker)
        {
            Workbook workbook = Startup(subor);

            try { return worker(workbook); }
            finally { Cleanup(workbook, save, close, ulozAko); }
        }

        public static R ExecuteOperation<R, T>(string subor, bool save, bool close, string ulozAko, Operation2<R, Workbook, T> worker, T param)
        {
            Workbook workbook = Startup(subor);

            try { return worker(workbook, param); }
            finally { Cleanup(workbook, save, close, ulozAko); }
        }

        public static R ExecuteOperation<R, T1, T2>(string subor, bool save, bool close, string ulozAko, Operation3<R, Workbook, T1, T2> worker, T1 param1, T2 param2)
        {
            Workbook workbook = Startup(subor);

            try { return worker(workbook, param1, param2); }
            finally { Cleanup(workbook, save, close, ulozAko); }
        }

        public static R ExecuteOperation<R, T1, T2, T3>(string subor, bool save, bool close, string ulozAko, Operation4<R, Workbook, T1, T2, T3> worker, T1 param1, T2 param2, T3 param3)
        {
            Workbook workbook = Startup(subor);

            try { return worker(workbook, param1, param2, param3); }
            finally { Cleanup(workbook, save, close, ulozAko); }
        }

        #endregion
    }
}

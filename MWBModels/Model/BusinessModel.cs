using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace MWBModels.Model
{
    public static class ProcessResult
    {
        public static List<DataSheet> GenerateResult(ObservableCollection model, string fileName)
        {
            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                List<MTM> ThornDatas = new List<MTM>();
                List<GR2M> GR2MDatas = new List<GR2M>();
                List<GR5M> GR5MDatas = new List<GR5M>();
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length == 4)
                    {
                        if (model.MTM)
                        {
                            MTM data = new MTM(double.Parse(fields[2]), GetPET(fields[0], fields[1], fields[3]), GetPrevSM(ThornDatas))
                            {
                                STC = model.STC,
                                RFactor = model.RFactor,
                                DRFactor = model.DRFactor,
                                Time = GetDate(fields[0], fields[1])
                            };
                            ThornDatas.Add(data);
                        }
                        if (model.GR2M)
                        {
                            GR2M data = new GR2M(double.Parse(fields[2]), GetPET(fields[0], fields[1], fields[3]), GetPrevSM(GR2MDatas), GetPrevR(GR2MDatas))
                            {
                                x1 = model.x1,
                                x2 = model.x2,
                                Time = GetDate(fields[0], fields[1])
                            };
                            GR2MDatas.Add(data);
                        }
                        if (model.GR5M)
                        {
                            GR5M data = new GR5M(double.Parse(fields[2]), GetPET(fields[0], fields[1], fields[3]), GetPrevSM(GR5MDatas), GetPrevR(GR5MDatas))
                            {
                                x1 = model.X1,
                                x2 = model.X2,
                                x3 = model.X3,
                                x4 = model.X4,
                                x5 = model.X5,
                                Time = GetDate(fields[0], fields[1])
                            };
                            GR5MDatas.Add(data);
                        }
                    }
                }
                return GetResultSheet(ThornDatas, GR2MDatas, GR5MDatas);
            }
        }

        private static string GetDate(string v1, string v2)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(int.Parse(v1)) + "-" + v2;
        }

        //private static double GetPrevAET(List<GR5M> datas)
        //{
        //    if (datas.Count > 0)
        //        return datas[datas.Count - 1].AET;
        //    else
        //        return 0;
        //}

        //private static double GetPrevPRE(List<GR5M> datas)
        //{
        //    if (datas.Count > 0)
        //        return datas[datas.Count - 1].PRE;
        //    else
        //        return 0;
        //}

        private static double GetPrevR(List<GR5M> datas)
        {
            if (datas.Count > 0)
                return datas[datas.Count - 1].R;
            else
                return 0;
        }

        private static double GetPrevR(List<GR2M> datas)
        {
            if (datas.Count > 0)
                return datas[datas.Count - 1].R;
            else
                return 0;
        }

        private static List<DataSheet> GetResultSheet(List<MTM> thornDatas, List<GR2M> GR2MDatas, List<GR5M> GR5MDatas)
        {
            List<DataSheet> resultSheet = new List<DataSheet>();
            if (thornDatas.Count > 0)
            {
                DataSheet sheet = new DataSheet
                {
                    Title = "MTM"
                };
                var datas = new List<object>();
                foreach (var data in thornDatas)
                {
                    datas.Add(new { Time = data.Time, PRE = data.PRE, PET = data.PET, AET = data.AET.ToString("0.000"), SoilMoisture = data.SoilMoisture.ToString("0.000"), Runoff = data.Runoff.ToString("0.000") });
                }
                sheet.Data = datas.ToArray();
                resultSheet.Add(sheet);
            }
            if (GR2MDatas.Count > 0)
            {
                DataSheet sheet = new DataSheet
                {
                    Title = "GR2M"
                };
                var datas = new List<object>();
                foreach (var data in GR2MDatas)
                {
                    datas.Add(new { Time = data.Time, PRE = data.PRE, PET = data.PET, AET = data.ET.ToString("0.000"), SoilMoisture = data.S.ToString("0.000"), Runoff = data.R.ToString("0.000") });
                }
                sheet.Data = datas.ToArray();
                resultSheet.Add(sheet);
            }
            if (GR5MDatas.Count > 0)
            {
                DataSheet sheet = new DataSheet
                {
                    Title = "GR5M"
                };
                var datas = new List<object>();
                foreach (var data in GR5MDatas)
                {
                    datas.Add(new { Time = data.Time, PRE = data.PRE, PET = data.PET, AET = data.ET.ToString("0.000"), SoilMoisture = data.S.ToString("0.000"), Runoff = data.R.ToString("0.000") });
                }
                sheet.Data = datas.ToArray();
                resultSheet.Add(sheet);
            }
            return resultSheet;
        }

        private static double GetPrevSM(List<GR5M> datas)
        {
            if (datas.Count > 0)
                return datas[datas.Count - 1].S;
            else
                return 0;
        }

        private static double GetPrevSM(List<GR2M> datas)
        {
            if (datas.Count > 0)
                return datas[datas.Count - 1].S;
            else
                return 0;
        }

        private static double GetPrevSM(List<MTM> datas)
        {
            if (datas.Count > 0)
                return datas[datas.Count - 1].SoilMoisture;
            else
                return 0;
        }

        private static double GetPET(string v1, string v2, string v3)
        {
            int month = int.Parse(v1);
            int year = int.Parse(v2);
            double dailyPET = double.Parse(v3);
            double monthlyPET = 0;
            bool isLeapYear = DateTime.IsLeapYear(year);
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    monthlyPET = dailyPET * 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    monthlyPET = dailyPET * 30;
                    break;
                case 2:
                    if(isLeapYear)
                        monthlyPET = dailyPET * 29;
                    else
                        monthlyPET = dailyPET * 28;
                    break;
            }
            return monthlyPET;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MWBModels.Model
{
    public class ObservableCollection : ViewModelBase
    {
        private string _FileName = "Select File";
        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                Set(ref _FileName, value);
            }
        }

        //Thorntwaite

        private double _STC = 260;
        public double STC
        {
            get
            {
                return _STC;
            }
            set
            {
                Set(ref _STC, value);
            }
        }
        private double _RFactor = 0.01;
        public double RFactor
        {
            get
            {
                return _RFactor;
            }
            set
            {
                Set(ref _RFactor, value);
            }
        }
        private double _DRFactor = 0.004;
        public double DRFactor
        {
            get
            {
                return _DRFactor;
            }
            set
            {
                Set(ref _DRFactor, value);
            }
        }


        //GR2M
        
        private double _x1 = 2663;
        public double x1
        {
            get
            {
                return _x1;
            }
            set
            {
                Set(ref _x1, value);
            }
        }
        private double _x2 = 0.92;
        public double x2
        {
            get
            {
                return _x2;
            }
            set
            {
                Set(ref _x2, value);
            }
        }


        //GR5M

        private double _X1 = 1132;
        public double X1
        {
            get
            {
                return _X1;
            }
            set
            {
                Set(ref _X1, value);
            }
        }
        private double _X2 = 3.8;
        public double X2
        {
            get
            {
                return _X2;
            }
            set
            {
                Set(ref _X2, value);
            }
        }
        private double _X3 = 1;
        public double X3
        {
            get
            {
                return _X3;
            }
            set
            {
                Set(ref _X3, value);
            }
        }
        private double _X4 = 59.5;
        public double X4
        {
            get
            {
                return _X4;
            }
            set
            {
                Set(ref _X4, value);
            }
        }
        private double _X5 = 0.03;
        public double X5
        {
            get
            {
                return _X5;
            }
            set
            {
                Set(ref _X5, value);
            }
        }


        //Output Plots

        private bool _PRE = true;
        public bool PRE
        {
            get
            {
                return _PRE;
            }
            set
            {
                Set(ref _PRE, value);
            }
        }
        private bool _PET = true;
        public bool PET
        {
            get
            {
                return _PET;
            }
            set
            {
                Set(ref _PET, value);
            }
        }
        private bool _AET = true;
        public bool AET
        {
            get
            {
                return _AET;
            }
            set
            {
                Set(ref _AET, value);
            }
        }
        private bool _SMC = true;
        public bool SMC
        {
            get
            {
                return _SMC;
            }
            set
            {
                Set(ref _SMC, value);
            }
        }
        private bool _Runoff = true;
        public bool Runoff
        {
            get
            {
                return _Runoff;
            }
            set
            {
                Set(ref _Runoff, value);
            }
        }


        //Models
        private bool _MTM = true;
        public bool MTM
        {
            get
            {
                return _MTM;
            }
            set
            {
                Set(ref _MTM, value);
            }
        }
        private bool _GR2M = false;
        public bool GR2M
        {
            get
            {
                return _GR2M;
            }
            set
            {
                Set(ref _GR2M, value);
            }
        }
        private bool _GR5M = false;
        public bool GR5M
        {
            get
            {
                return _GR5M;
            }
            set
            {
                Set(ref _GR5M, value);
            }
        }
    }
}

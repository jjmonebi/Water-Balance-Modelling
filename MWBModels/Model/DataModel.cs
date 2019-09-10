using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWBModels.Model
{
    public class MTM
    {
        //Constants
        public double STC { get; set; }
        public double RFactor { get; set; }
        public double DRFactor { get; set; }

        //Data
        public double PRE { get; set; }
        public double PET { get; set; }
        public string Time { get; set; }

        //Variables
        public double DRO
        {
            get
            {
                return DRFactor * PRE;
            }
        }
        public double PRemain
        {
            get
            {
                return PRE - DRO;
            }
        }
        public double P_PET
        {
            get
            {
                return PRemain - PET;
            }
        }
        public double SoilMoisture
        {
            get
            {
                if (P_PET < 0)
                {
                    var x = PreviousSoilMoisture * Math.Exp(-(PET - PRemain) / STC);
                    return x;
                }
                else
                {
                    return Math.Min(STC, (PreviousSoilMoisture + P_PET));
                }
            }
        }
        public double PreviousSoilMoisture { get; set; }
        public double AET
        {
            get
            {
                if (P_PET < 0)
                {
                    return PRemain + Math.Abs(SoilMoisture-PreviousSoilMoisture);
                }
                else
                {
                    return PET;
                }
            }
        }
        public double Surplus
        {
            get
            {
                return Math.Max((PRemain-AET+PreviousSoilMoisture-STC),0);
            }
        }
        public double Runoff
        {
            get
            {
                return (RFactor * Surplus) + DRO;
            }
        }

        public MTM(double Pre, double Pet, double PrevSM = 0.0)
        {
            PRE = Pre;
            PET = Pet;
            PrevSM = PreviousSoilMoisture;
        }
    }
    public class GR2M
    {
        //Constants
        //public double Smax { get; set; }
        public double x1 { get; set; }
        public double x2 { get; set; }
        //public double D { get; set; }

        //Data
        public double PRE { get; set; }
        public double PET { get; set; }
        public string Time { get; set; }

        //Variables
        public double Pi
        {
            get
            {
                return Math.Tanh(PRE/x1);
            }
        }
        public double Gamma
        {
            get
            {
                return Math.Tanh(PET / x1);
            }
        }
        public double S1
        {
            get
            {
                return ((PreviousSoilMoisture + x1 * Pi) / (1 + ((Pi * PreviousSoilMoisture) / x1)));
            }
        }
        public double S2
        {
            get
            {
                return (S1 * (1 - Gamma) / (1 + Gamma - ((Gamma * PreviousSoilMoisture) / x1)));
            }
        }
        public double S
        {
            get
            {
                return S2 / Math.Pow((1 + Math.Pow((S2 / x1), x2)), (1 / x2));
            }
        }
        public double PreviousSoilMoisture { get; set; }
        public double PreviousR { get; set; }
        public double ET
        {
            get
            {
                return S1 - S2;
            }
        }
        public double R1
        {
            get
            {
                return PreviousR + (1 - x2) * P3;
            }
        }
        public double R
        {
            get
            {
                return Math.Pow(R1, 2) / (R1 + 60);
            }
        }
        public double P1
        {
            get
            {
                return PRE + PreviousSoilMoisture - S1;
            }
        }
        public double P2
        {
            get
            {
                return S2 - S;
            }
        }
        public double P3
        {
            get
            {
                return P1 + P2;
            }
        }

        public GR2M(double Pre, double Pet, double PrevSM = 0.0, double PrevR = 0.0)
        {
            PRE = Pre;
            PET = Pet;
            PrevSM = PreviousSoilMoisture;
            PreviousR = PrevR;
        }
    }
    public class GR5M
    {
        //Constants
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double x3 { get; set; }
        public double x4 { get; set; }
        public double x5 { get; set; }

        //Data
        public double PRE { get; set; }
        public double PET { get; set; }
        public string Time { get; set; }

        //Variables
        public double Pi
        {
            get
            {
                return Math.Tanh(PRE / x1);
            }
        }
        public double Gamma
        {
            get
            {
                return Math.Tanh(PET / x1);
            }
        }
        public double S1
        {
            get
            {
                return ((PreviousSoilMoisture + x1 * Pi) / (1 + ((Pi * PreviousSoilMoisture) / x1)));
            }
        }
        public double S2
        {
            get
            {
                return (S1 * (1 - Gamma) / (1 + Gamma - ((Gamma * PreviousSoilMoisture) / x1))); 
            }
        }
        public double S
        {
            get
            {
                return S2 / Math.Pow((1 + Math.Pow((S2 / x1), x2)), (1 / x2));
            }
        }
        public double PreviousSoilMoisture { get; set; }
        public double PreviousR { get; set; }
        public double ET
        {
            get
            {
                return S1 - S2;
            }
        }
        public double Rs
        {
            get
            {
                return x3 * P3;
            }
        }
        public double Rg
        {
            get
            {
                return Math.Pow(R1, 2) / (R1 + x4);
            }
        }
        public double R1
        {
            get
            {
                return PreviousR + (1 - x3) * P3;
            }
        }
        public double R
        {
            get
            {
                return x5 * (Rs + Rg);
            }
        }
        public double P1
        {
            get
            {
                return PRE + PreviousSoilMoisture - S1;
            }
        }
        public double P2
        {
            get
            {
                return S2 - S;
            }
        }
        public double P3
        {
            get
            {
                return P1 + P2;
            }
        }

        public GR5M(double Pre, double Pet, double PrevSM = 0.0, double PrevR = 0.0)
        {
            PRE = Pre;
            PET = Pet;
            PrevSM = PreviousSoilMoisture;
            PreviousR = PrevR;
        }
    }
    public class DataSheet
    {
        public string Title { get; set; }
        public object[] Data { get; set; }
    }
}

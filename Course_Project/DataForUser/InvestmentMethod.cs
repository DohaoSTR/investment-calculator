using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Project.Models
{
    class InvestmentMethod
    {
        private double NPV(double moneyInvest, int dateValue, double[] valueArray, double rateInvest)
        {
            double sumNPV = 0;
            for (int i = 0; i <= dateValue - 1; i++)
            {
                sumNPV += valueArray[i] / (Math.Pow(1 + rateInvest, i));
            }
            return sumNPV - moneyInvest;
        }
        private double PI(double moneyInvest, int dateValue, double[] valueArray, double rateInvest)
        {
            return NPV(moneyInvest, dateValue, valueArray, rateInvest) / moneyInvest;
        }
        private double ARR(double moneyInvest, int dateValue, double[] valueArray)
        {
            double PN = 0;
            for (int i = 0; i <= dateValue - 1; i++)
            {
                PN += valueArray[i];
            }
            return ((PN / dateValue) / (0.5 * moneyInvest)) * 100;
        }
        private double PP(double moneyInvest, int dateValue, double[] valueArray)
        {
            double sumValue = 0;
            double sumDate = 0;
            for (int i = 0; i <= dateValue - 1; i++)
            {
                sumValue += valueArray[i];
                if (sumValue <= moneyInvest)
                    sumDate += 1;
            }
            return sumDate;
        }
        private double DPP(double moneyInvest, int dateValue, double[] valueArray, double rateInvest)
        {
            double sumValue = 0;
            double sumDate = 0;
            for (int i = 0; i <= dateValue - 1; i++)
            {
                sumValue += valueArray[i] / (Math.Pow(1 + rateInvest, i));
                if (sumValue <= moneyInvest)
                    sumDate += 1;
            }
            return sumDate;
        }
        private double IRR(double moneyInvest, int dateValue, double[] valueArray)
        {
            double rate1 = 0.01;
            double rate2 = 0.1;
            double fr1 = NPV(moneyInvest, dateValue, valueArray, rate1);
            double fr2 = NPV(moneyInvest, dateValue, valueArray, rate2);
            return rate1 + fr1 / (fr1 - fr2) * (rate2 - rate1) * 100;
        }
        public string InvestMethodsАnalytics(double moneyInvest, int dateValue, double[] valueArray, double rateInvest)
        {
            string resultNPV;
            if (NPV(moneyInvest, dateValue, valueArray, rateInvest) >= 0)
                resultNPV = "\nNPV: Проект следует принять";
            else if (NPV(moneyInvest, dateValue, valueArray, rateInvest) == 0)
                resultNPV = "\nNPV: Проект не является ни прибыльным ни убыточным";
            else
                resultNPV = "\nNPV: Проект следует отвергнуть";
            string resultPI;
            if (PI(moneyInvest, dateValue, valueArray, rateInvest) >= 1)
                resultPI = "\nPI: Проект следует принять";
            else if (PI(moneyInvest, dateValue, valueArray, rateInvest) == 1)
                resultPI = "\nPI: Проект не является ни прибыльным ни убыточным";
            else
                resultPI = "\nPI: Проект следует отвергнуть";
            string resultIRR;
            if (NPV(moneyInvest, dateValue, valueArray, rateInvest) == 0)
            {
                if (IRR(moneyInvest, dateValue, valueArray) > moneyInvest)
                    resultIRR = "\nIRR: Проект следует принять";
                else if (IRR(moneyInvest, dateValue, valueArray) == moneyInvest)
                    resultIRR = "\nIRR: Проект не является ни прибыльным ни убыточным";
                else
                    resultIRR = "\nIRR: Проект следует отвергнуть";
            }
            else if (NPV(moneyInvest, dateValue, valueArray, rateInvest) >= 0)
            {
                resultIRR = "\nМаксимально допустимый уровень расходов: " + Math.Round(IRR(moneyInvest, dateValue, valueArray)) + "%";
            }
            else
            {
                resultIRR = "\nIRR: Проект следует отвергнуть";
            }
            string resultPpDpp;
            if (PP(moneyInvest, dateValue, valueArray) == DPP(moneyInvest, dateValue, valueArray, rateInvest))
            {
                resultPpDpp = "\nСрок получения " + rateInvest * 100 + "% прибыли в днях: " + PP(moneyInvest, dateValue, valueArray);
            }
            else if (PP(moneyInvest, dateValue, valueArray) >= DPP(moneyInvest, dateValue, valueArray, rateInvest))
            {
                resultPpDpp = "\nСрок получения " + rateInvest * 100 + "% прибыли: от " + DPP(moneyInvest, dateValue, valueArray, rateInvest) + " до " + PP(moneyInvest, dateValue, valueArray) + " дней";
            }
            else
                resultPpDpp = "\nСрок получения " + rateInvest * 100 + "% прибыли: от " + PP(moneyInvest, dateValue, valueArray) + " до " + DPP(moneyInvest, dateValue, valueArray, rateInvest) + " дней";

            string resultARR = "\nБухгалтерская норма доходности: " + Math.Round(ARR(moneyInvest, dateValue, valueArray)) + "%";           
            return resultNPV + resultPI + resultIRR + resultPpDpp + resultARR;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViannaWebCrawler
{
    public class Discipline
    {
        public string Name { get; set; }
        //Nota do primeiro Bimestre
        public double FirstBimesterGrade { get; set; }
        //Nota do segundo Bimestre
        public double SecondBimesterGrade { get; set; }
        public double Media { get; set; }
        //Nota da recuperacao ou prova exame
        public double RetakeTestGrade { get; set; }
        //Nota final
        public double FinalMedia { get; set; }
        //Numero de faltas
        public int MissedClasses { get; set; }
        //Percentual de faltas
        public double MissedClassPercentage { get; set; }

    }
}

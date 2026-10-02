using System;
using System.Collections.Generic;
using System.Text;

namespace Gastos_registro_clase_9
{
    public class Gasto
    {
        public int Id { get; set; }
        public string descripcion { get; set; } = "";

        public string categoria { get; set; } = "";


        public decimal monto { get; set; }



        public override string ToString()
        {
            return $"{Id,-3} {descripcion,-20} Q {monto,8:N2} {categoria,10}";

        }


    }
}


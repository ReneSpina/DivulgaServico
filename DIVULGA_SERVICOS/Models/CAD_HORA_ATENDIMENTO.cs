﻿namespace DIVULGA_SERVICOS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class CAD_HORA_ATENDIMENTO
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CD_PES_JURIDICA { get; set; }

        [Key]
        [Column(Order = 0)]
        public int DIA_SEMANA { get; set; }
        
        public int HORA_INICIO { get; set; }
        
        public int HORA_FIM { get; set; }        

        public virtual CAD_PES_JURIDICA CAD_PES_JURIDICA { get; set; }
    }
}
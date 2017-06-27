﻿using HomeControl.Data.Dal.Dao.Custom.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControl.Data.Dal.Factory
{
    public abstract class DaoFactory
    {

        public abstract IComodoDao GetComodoDao(); 
        public abstract IInterruptorDao GetInterruptorDao();
        public abstract ISensorDao GetSensorDao();        
        public abstract IDispositivoDao GetDispositivoDao();
        public abstract IResidenciaDao GetResidenciaDao();
        public abstract IEmbarcadoDao GetEmbarcadoDao();
        public abstract IHistoricoUsoDispositivoDao GetHistoricoUsoDispositivoDao();

    }
}

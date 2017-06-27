using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeControl.Data.Dal.Dao.Custom.Interfaces;
using HomeControl.Data.Dal.Dao.Custom.Implementations;
using HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet;

namespace HomeControl.Data.Dal.Factory
{
    public class AdoNetRepositoryFactory : DaoFactory
    {

        public override IComodoDao GetComodoDao()
        {
            return new ComodoDaoAdoNet();
        }

        public override IDispositivoDao GetDispositivoDao()
        {
            return new DispositivoDaoAdoNet();
        }

        public override IEmbarcadoDao GetEmbarcadoDao()
        {
            return new EmbarcadoDatoAdoNet();
        }

        public override IHistoricoUsoDispositivoDao GetHistoricoUsoDispositivoDao()
        {
            return new HistoricoUsoDaoAdoNet();
        }

        public override IInterruptorDao GetInterruptorDao()
        {
            return new InterruptorDaoAdoNet();
        }

        public override IResidenciaDao GetResidenciaDao()
        {
            return new ResidenciaDaoAdoNet();
        }

        public override ISensorDao GetSensorDao()
        {
            return new SensorDaoAdoNet();
        }

    }
}

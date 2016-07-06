using Component.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OExam.Core.Data.UnitWork
{
    class RepositoryContext : UnitWorkBase
    {

        protected override DbContext Context
        {
            get; set;
        }

        public RepositoryContext()
        {
            Context = ContextFactory.GetContext();
        }
    }
}

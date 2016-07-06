using Component.Data;
using Component.Tools;
using OExam.Core.Data.UnitWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Core.Data.Repository
{
    public abstract class CoreRepositoryBase<TEntity> : RepositoryBase<TEntity, int> where TEntity : EntityBase<int>
    {
        private RepositoryContext _unit;
        protected override IUnitWork EFContext
        {
            get
            {
                if(_unit == null)
                    _unit = new RepositoryContext();

                return _unit;
            }

        }

    }
}

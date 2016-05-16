using GrantRequests.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantRequests.Common;

namespace GrantRequests.WEB.Services
{
    public class ServiceBase
    {
        public ServiceBase(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        protected readonly UnitOfWork unitOfWork;
    }
}
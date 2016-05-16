using System;
using GrantRequests.DAL.EFContext;


namespace GrantRequests.DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private GrantRequestsContext db;
        private bool disposed = false;
        private UserRepository userRepository;
        private RequestRepository requestRepository;
        private TherapeuticAreaRepository therapeuticAreaRepository;
        private HealthcareProfessionRepository healthcareProfessionRepository;
        private BenefitRepository benefitRepository;
        private TypeOfDisplayExhibitRepository typeOfDisplayExhibitRepository;
        private CountryRepository countryRepository;
        private StateRepository stateRepository;
       


        public UnitOfWork(string connectionString)
        {
            db = new GrantRequestsContext(connectionString);
        }

        private TRepository GetRepository<TRepository>(TRepository repository)
        {
            if (repository == null)
                repository = (TRepository)Activator.CreateInstance(typeof(TRepository), db); ;
            return repository;
        }      

        public CountryRepository Countries
        {
            get
            {
                return GetRepository(countryRepository);
            }
        }

        public StateRepository States
        {
            get
            {
                return GetRepository(stateRepository);
            }
        }

        public BenefitRepository Benefits
        {
            get
            {
                return GetRepository(benefitRepository);
            }
        }

        public HealthcareProfessionRepository HealthcareProfessions
        {
            get
            {
                return GetRepository(healthcareProfessionRepository);
            }
        }

        public RequestRepository Requests
        {
            get
            {
                return GetRepository(requestRepository);
            }
        }

        public TherapeuticAreaRepository TherapeuticAreas
        {
            get
            {
                return GetRepository(therapeuticAreaRepository);
            }
        }

        public TypeOfDisplayExhibitRepository TypeOfDisplayExhibits
        {
            get
            {
                return GetRepository(typeOfDisplayExhibitRepository);
            }
        }

        public UserRepository Users
        {
            get
            {
                return GetRepository(userRepository);
            }
        }
        

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
